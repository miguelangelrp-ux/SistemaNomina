using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_Nomina
{
    public class Empleado
    {
        public string Nombre {get; set;}
        public string Codigo {get; set;}
        public string Departamento {get; set;}
        public double SalarioBase {get; set;}
        

        //Constructor
        public Empleado (string nombre, string codigo, string departamento, double salarioBase)
        {
            Nombre = nombre;
            Codigo = codigo;
            Departamento = departamento;
            SalarioBase = salarioBase;
        }

    }
}