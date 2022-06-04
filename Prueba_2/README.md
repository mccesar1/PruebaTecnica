## Directorio para la prueba 1 ##

create database prueba01

use prueba01 

CREATE TABLE usuarios (
    userId int primary key identity(1,1),
    Login  varchar(100),
    Nombre  varchar(100),
	Paterno   varchar(100),
    Materno   varchar(100)
   
);



CREATE TABLE empleados  (
    userId int,
    Sueldo double precision,
    FechaIngreso date,
	FOREIGN KEY (userId) REFERENCES usuarios(userId)
   
);


INSERT INTO usuarios
VALUES ( 'user01', 'bere' ,'NARANJO', 'GONZALEZ');
INSERT INTO usuarios
VALUES ( 'user02', 'ALEXIS' ,'CAMPOS
', 'NARANJO
');


INSERT INTO empleados
VALUES ( 1, 8837, '2000-01-11');


1---------------
Select userId from usuarios where userId Not in  (6,7,9,10)

2---------------
UPDATE empleados set Sueldo= (sueldo+(sueldo*.10)) where FechaIngreso BETWEEN '2000-01-01'and '2001-12-30'

3-----------------
select usuarios.Nombre, empleados.FechaIngreso from usuarios inner join empleados on usuarios.userId = empleados.userId where Sueldo > 10000  and usuarios.Paterno like 'T%'

4--------------
select count (empleados.userId) cantidad from empleados where empleados.Sueldo >1200 group by empleados.Sueldo




