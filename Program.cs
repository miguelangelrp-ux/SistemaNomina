using System;
using System.Collections.Generic;

namespace Sistema_Nomina
{
    class Program
    {
        static Empresa empresa = new Empresa();

        static void Main(string[] args)
        {
            //Creando logica de menu principal
            int opcion = -1;

            do
            {
                Console.Clear();
                Console.WriteLine("Servicios Corporativos Caribe SRL");
                Console.WriteLine("\n--- Sistema de Nómina ---");
                Console.WriteLine("1. Agregar empleado");
                Console.WriteLine("2. Editar empleado");
                Console.WriteLine("3. Consultar empleados");
                Console.WriteLine("4. Eliminar empleado");
                Console.WriteLine("5. Generar nómina");
                Console.WriteLine("6. Reporte mensual");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione opción: ");
                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1: AgregarEmpleadoMenu(); break;
                    case 2: EditarEmpleadoMenu(); break;
                    case 3: ConsultarEmpleadosMenu(); break;  
                    case 4: EliminarEmpleadoMenu(); break;  
                    case 5: empresa.GenerarNomina(); break;
                    case 6: empresa.ReporteMensual(); break;
                    case 0: Console.WriteLine("Saliendo del sistema..."); break;
                    default:
                        Console.WriteLine("Opción no válida."); break;
                }

                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();

            } while (opcion != 0);
        }

        //Pidiendo los datos del empleado para agregarlo a la lista
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

        //Consultando lista de empleados
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

        //Editando empleado
        static void EditarEmpleadoMenu()
        {
            Console.Write("Ingrese el código del empleado a editar: ");
            string codigo = Console.ReadLine();

            var emp = empresa.ObtenerEmpleados().Find(e => e.Codigo == codigo);
            if (emp == null)
            {
                Console.WriteLine("Empleado no encontrado.");
                return;
            }

            Console.Write($"Nuevo nombre (actual: {emp.Nombre}): ");
            string nombre = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nombre)) nombre = emp.Nombre;

            Console.Write($"Nuevo departamento (actual: {emp.Departamento}): ");
            string departamento = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(departamento)) departamento = emp.Departamento;

            Console.Write($"Nuevo salario base (actual: {emp.SalarioBase}): ");
            string salarioInput = Console.ReadLine();
            double salarioBase;
            if (!double.TryParse(salarioInput, out salarioBase) || salarioBase <= 0)
                salarioBase = emp.SalarioBase;

            if (empresa.EditarEmpleado(codigo, nombre, departamento, salarioBase))
                Console.WriteLine("Empleado actualizado correctamente.");
            else
                Console.WriteLine("No se pudo actualizar el empleado.");
        }

        //Eliminar empleado que ya existe
        static void EliminarEmpleadoMenu()
        {
            Console.Write("Ingrese el código del empleado a eliminar: ");
            string codigo = Console.ReadLine();

            if (empresa.EliminarEmpleado(codigo))
                Console.WriteLine("Empleado eliminado correctamente.");
            else
                Console.WriteLine("Empleado no encontrado.");
        }
    }
}



