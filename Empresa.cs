using System;
using System.Collections.Generic;

namespace Sistema_Nomina
{
    public class Empresa
    {
        public string Nombre { get; set; }
        private List<Empleado> empleados;

        public Empresa(string nombre)
        {
            Nombre = nombre;
            empleados = new List<Empleado>();
        }

        //Metodos

        //Agregando empleado
        public bool AgregarEmpleado(Empleado emp)
        {
            if (empleados.Exists(e => e.Codigo == emp.Codigo))
                return false;

            empleados.Add(emp);
            return true;
        }

        //Obteniendo una lista de empleados
        public List<Empleado> ObtenerEmpleados()
        {
            return empleados;
        }

        //Generando la nomina con sus deduccionnes
        public void GenerarNomina()
        {
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

        //Generando reporte mensual por todo con sus deducciones
        public void ReporteMensual()
        {
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
