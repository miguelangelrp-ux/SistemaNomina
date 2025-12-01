using System;
using System.Collections.Generic;

namespace Sistema_Nomina
{
    class Program
    {
        // Lista de empleados
        static Empresa empresa = new Empresa("Servicios Corporativos Caribe SRL");

        static void Main(string[] args)
        {
            int opcion = -1;

            do
            {
                Console.Clear();
                Console.WriteLine("Servicios Corporativos Caribe SRL");
                Console.WriteLine("\n--- Sistema de Nómina ---");
                Console.WriteLine("1. Agregar empleado");
                Console.WriteLine("2. Consultar empleados");
                Console.WriteLine("3. Generar nómina");
                Console.WriteLine("4. Reporte mensual");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione opción: ");
                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1: AgregarEmpleadoMenu(); break;
                    case 2: ConsultarEmpleadosMenu(); break;
                    case 3: empresa.GenerarNomina(); break;
                    case 4: empresa.ReporteMensual(); break;
                    case 0: Console.WriteLine("Saliendo del sistema..."); break;
                    default:
                        Console.WriteLine("Opción no válida."); break;
                }

                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();

            } while (opcion != 0);
        }


        static void AgregarEmpleadoMenu()
        {
            Console.Write("Código: ");
            string codigo = Console.ReadLine();

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("El nombre no puede estar vacío.");
                return;
            }

            Console.Write("Departamento: ");
            string departamento = Console.ReadLine();

            Console.Write("Salario Base: ");
            if (!double.TryParse(Console.ReadLine(), out double salarioBase) || salarioBase <= 0)
            {
                Console.WriteLine("Salario inválido.");
                return;
            }

            var emp = new Empleado(nombre, codigo, departamento, salarioBase);
            if (empresa.AgregarEmpleado(emp))
                Console.WriteLine("Empleado agregado correctamente.");
            else
                Console.WriteLine("Ya existe un empleado con ese código.");
        }

        static void ConsultarEmpleadosMenu()
        {
            var empleados = empresa.ObtenerEmpleados();
            if (empleados.Count == 0)
            {
                Console.WriteLine("No hay empleados registrados.");
                return;
            }

            Console.WriteLine("\n--- Lista de Empleados ---");
            foreach (var emp in empleados)
            {
                Console.WriteLine($"{emp.Codigo} | {emp.Nombre} | {emp.Departamento} | Salario Base: {emp.SalarioBase:C}");
            }
        }
    }
}



