using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Conexion.Conectar();
            MessageBox.Show("conexion exitosa");

            dataGridView1.DataSource = llenar_grid();
        }
        public DataTable llenar_grid() {
            Conexion.Conectar();
            DataTable dt = new DataTable();
            string consulta = "SELECT top 10 usuarios.Login, usuarios.Nombre, usuarios.Paterno, usuarios.Materno, empleados.Sueldo, empleados.FechaIngreso from usuarios inner join  empleados ON empleados.userId= usuarios.userId";
            SqlCommand cmd = new SqlCommand(consulta, Conexion.Conectar());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;

        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string agregar = "INSERT INTO " +
                "usuarios (Login, Nombre, Paterno, Materno) " +
                "VALUES(@Login,@Nombre,@Paterno,@Materno)";

            string agregarsueldo = "INSERT INTO empleados (userId, Sueldo, FechaIngreso) VALUES (,@Sueldo, GETDATE()) ";

            SqlCommand cmd1 = new SqlCommand(agregar,  Conexion.Conectar());
            SqlCommand cmd2 = new SqlCommand(agregarsueldo, Conexion.Conectar());
            cmd1.Parameters.AddWithValue("@Login", txtlogin.Text);
            cmd1.Parameters.AddWithValue("@Nombre", txtnombre.Text);
            cmd1.Parameters.AddWithValue("@Paterno", txtpaterno.Text);
            cmd1.Parameters.AddWithValue("@Materno", txtmaterno.Text);
            cmd2.Parameters.AddWithValue("@Sueldo", txtsueldo.Text);


            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();

            MessageBox.Show("DATOS AGREGADOS");
            dataGridView1.DataSource = llenar_grid();



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtlogin.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtnombre.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtpaterno.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtmaterno.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtsueldo.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            }
            catch { 
            
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string actualizasueldo = "UPDATE empleados SET Sueldo = @Sueldo WHERE Sueldo=@Sueldo";
            SqlCommand cmd2 = new SqlCommand(actualizasueldo, Conexion.Conectar());

            cmd2.Parameters.AddWithValue("@Sueldo", txtsueldo.Text);
            
            cmd2.ExecuteNonQuery();
            MessageBox.Show("Sueldo Modificado");
            dataGridView1.DataSource = llenar_grid();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV (*.csv)|*.csv";
                sfd.FileName = "Output.csv";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("Error" + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            int columnCount = dataGridView1.Columns.Count;
                            string columnNames = "";
                            string[] outputCsv = new string[dataGridView1.Rows.Count + 1];
                            for (int i = 0; i < columnCount; i++)
                            {
                                columnNames += dataGridView1.Columns[i].HeaderText.ToString() + ",";

                            }
                            outputCsv[0] += columnNames;

                            for (int i = 1; (i - 1) < dataGridView1.Rows.Count; i++)
                            {
                                for (int j = 0; j < columnCount; j++)
                                {
                                    
                                    outputCsv[i] += Convert.ToString(dataGridView1.Rows[i - 1].Cells[j].Value) + ",";
                                }
                            }

                            File.WriteAllLines(sfd.FileName, outputCsv, Encoding.UTF8);
                            MessageBox.Show("Importado con exito");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("nada para exportar");
            }
        }
    }
}
