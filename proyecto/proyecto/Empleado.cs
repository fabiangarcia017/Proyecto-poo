using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto
{
    class Empleado
    {
        private int _intID;

        public int Id
        {
            get { return _intID; }
            set { _intID = value; }
        }
        private string _strNombre;

        public string Nombre
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }
        private string _strApellido;

        public string Apellido
        {
            get { return _strApellido; }
            set { _strApellido = value; }
        }
        private string _strPuesto;

        public string Puesto
        {
            get { return _strPuesto; }
            set { _strPuesto = value; }
        }
        private int _intNumeroEmpleado;

        public int NumeroEmpleado
        {
            get { return _intNumeroEmpleado; }
            set { _intNumeroEmpleado = value; }
        }
        private double _dblSueldo;

        public double Sueldo
        {
            get { return _dblSueldo; }
            set { _dblSueldo = value; }
        }
        public delegate double Operacion();
        public double CalcularTotal(Operacion miOperacion)
        {
            return miOperacion();
        }
    }
}
