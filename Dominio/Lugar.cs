using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public abstract class Lugar
    {
        private int id;
        private string nombre;
        private decimal dimM2;
        private static int ultId = 0;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public decimal DimM2
        {
            get { return dimM2; }
            set { dimM2 = value; }
        }

        //CONSTRUCTOR de la clase Lugar
        public Lugar(string pNombre, decimal pDimM2)
        {
            this.Id = ++ultId;
            this.Nombre = pNombre;
            this.DimM2 = pDimM2;
        }

        public virtual decimal CostoAdicional(decimal precio)
        {
            return precio;
        }        
    }
}
