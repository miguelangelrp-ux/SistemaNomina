using System;
using System.Collections.Generic;

namespace Sistema_Nomina
{
    class Program
    {
        // Lista de empleados
        static List<Empleado> empleados = new List<Empleado>();

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
                    case 1: AgregarEmpleado(); break;
                    case 2: ConsultarEmpleados(); break;
                    case 3: GenerarNomina(); break;
                    case 4: ReporteMensual(); break;
                    case 0: Console.WriteLine("Saliendo del sistema..."); break;
                    default:
                        Console.WriteLine("Opción no válida."); break;
                }

                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();

            } while (opcion != 0);
        }


    }
}

