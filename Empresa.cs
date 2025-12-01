using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace Sistema_Nomina
{
    public class Empresa
    {
        private string connectionString = "Data Source=empresa.db";

        public Empresa()
        {
            // Crear tabla si no existe
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS empleados (
                    Codigo TEXT PRIMARY KEY,
                    Nombre TEXT,
                    Departamento TEXT,
                    SalarioBase REAL
                );";
            tableCmd.ExecuteNonQuery();
        }

        //Agregando al empleado a la base de datos
        public bool AgregarEmpleado(Empleado emp)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var insertCmd = connection.CreateCommand();
            insertCmd.CommandText = @"
                INSERT INTO empleados (Codigo, Nombre, Departamento, SalarioBase)
                VALUES ($codigo, $nombre, $departamento, $salario);";
            insertCmd.Parameters.AddWithValue("$codigo", emp.Codigo);
            insertCmd.Parameters.AddWithValue("$nombre", emp.Nombre);
            insertCmd.Parameters.AddWithValue("$departamento", emp.Departamento);
            insertCmd.Parameters.AddWithValue("$salario", emp.SalarioBase);

            try
            {
                insertCmd.ExecuteNonQuery();
                return true;
            }
            catch (SqliteException)
            {
                return false; // código duplicado
            }
        }

        //Obteniendo la lista de los empleados creados
        public List<Empleado> ObtenerEmpleados()
        {
            var lista = new List<Empleado>();

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = "SELECT Codigo, Nombre, Departamento, SalarioBase FROM empleados;";

            using var reader = selectCmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Empleado(
                    reader.GetString(1),  // Nombre
                    reader.GetString(0),  // Codigo
                    reader.GetString(2),  // Departamento
                    reader.GetDouble(3)   // SalarioBase
                ));
            }

            return lista;
        }

        //Editanr empleado existente

        public bool EditarEmpleado(string codigo, string nuevoNombre, string nuevoDepartamento, double nuevoSalarioBase)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var updateCmd = connection.CreateCommand();
            updateCmd.CommandText = @"
                UPDATE empleados 
                SET Nombre = $nombre, Departamento = $dep, SalarioBase = $salario
                WHERE Codigo = $codigo;";
            updateCmd.Parameters.AddWithValue("$nombre", nuevoNombre);
            updateCmd.Parameters.AddWithValue("$dep", nuevoDepartamento);
            updateCmd.Parameters.AddWithValue("$salario", nuevoSalarioBase);
            updateCmd.Parameters.AddWithValue("$codigo", codigo);

            int rows = updateCmd.ExecuteNonQuery();
            return rows > 0;
        }



        //Borrar empleado existente con confirmación
        public bool EliminarEmpleado(string codigo)
        {
            // Obtenemos el empleado para mostrar su nombre
            var empleados = ObtenerEmpleados();
            var empleado = empleados.Find(e => e.Codigo == codigo);

            if (empleado == null)
            {
                Console.WriteLine("Empleado no encontrado.");
                return false;
            }

            // Confirmación antes de eliminar
            Console.Write($"¿Está seguro que desea eliminar al empleado {empleado.Nombre}? (S/N): ");
            string respuesta = Console.ReadLine().Trim().ToUpper();

            if (respuesta != "S")
            {
                Console.WriteLine("Operación cancelada. No se eliminó al empleado.");
                return false;
            }

            // Conexión a la base de datos y eliminación
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var deleteCmd = connection.CreateCommand();
            deleteCmd.CommandText = "DELETE FROM empleados WHERE Codigo = $codigo;";
            deleteCmd.Parameters.AddWithValue("$codigo", codigo);

            int rows = deleteCmd.ExecuteNonQuery();

            if (rows > 0)
                Console.WriteLine($"Empleado {empleado.Nombre} eliminado exitosamente.");

            return rows > 0;
        }


        

        //Generar la nomina si existe algun empleado
        public void GenerarNomina()
        {
            var empleados = ObtenerEmpleados();

            if (empleados.Count == 0)
            {
                Console.WriteLine("No hay empleados registrados.");
                return;
            }

            Console.WriteLine("\n--- Nómina Mensual ---");
            foreach (var emp in empleados)
            {
                double afp = emp.SalarioBase * 0.0287;
                double ars = emp.SalarioBase * 0.0304;
                double neto = emp.SalarioBase - afp - ars;

                Console.WriteLine($"Empleado: {emp.Nombre} | Bruto: {emp.SalarioBase:C} | AFP: {afp:C} | ARS: {ars:C} | Neto: {neto:C}");
            }
        }

        //El reporte de los empleados con las deducciones 

        public void ReporteMensual()
        {
            var empleados = ObtenerEmpleados();

            if (empleados.Count == 0)
            {
                Console.WriteLine("No hay empleados registrados.");
                return;
            }

            double totalBruto = 0, totalAFP = 0, totalARS = 0, totalNeto = 0;

            Console.WriteLine("\n--- Reporte Mensual ---");
            foreach (var emp in empleados)
            {
                double afp = emp.SalarioBase * 0.0287;
                double ars = emp.SalarioBase * 0.0304;
                double neto = emp.SalarioBase - afp - ars;

                totalBruto += emp.SalarioBase;
                totalAFP += afp;
                totalARS += ars;
                totalNeto += neto;

                Console.WriteLine($"Empleado: {emp.Nombre} | Bruto: {emp.SalarioBase:C} | AFP: {afp:C} | ARS: {ars:C} | Neto: {neto:C}");
            }

            Console.WriteLine($"\nTotales: Bruto: {totalBruto:C} | AFP: {totalAFP:C} | ARS: {totalARS:C} | Neto: {totalNeto:C}");
        }
    }
}

