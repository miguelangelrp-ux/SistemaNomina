using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_Nomina
{
    public class Empleado
    {
        public string nombre {get; set;}
        public string codigo {get; set;}
        public string departamento {get; set;}
        public double salarioBase {get; set;}
        

        //Constructor
        public Empleado (string Nombre, string Codigo, string Departamento, double SalarioBase)
        {
            nombre = Nombre;
            codigo = Codigo;
            departamento = Departamento;
            salarioBase = SalarioBase;
        }

    }
}