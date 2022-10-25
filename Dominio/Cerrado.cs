using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Cerrado : Lugar
    {
        private int capacidadTotal;
        private static int aforo;
        
        //PROPIEDADES:

        public int Aforo
        {
            get { return aforo; }
            
        }

       
        //CONSTRUCTOR del objeto:
        public Cerrado(int pCapacidadTotal, string pNombre, decimal pDimM2) : base(pNombre, pDimM2)
        {
            this.capacidadTotal = pCapacidadTotal;   
        }

        //MÉTODOS:
        public static void CambiarAforo(int unNumero)
        {
            aforo = unNumero;
        }
        public override string ToString()
        {
            return $"{Nombre}";
        }
        public override decimal CostoAdicional(decimal precio)
        {
            decimal precioFinal = base.CostoAdicional(precio);

            if(aforo < 50)
            {
                precioFinal *= (decimal)1.30;
            } else if(aforo >= 50 && aforo <= 70)
            {
                precioFinal *= (decimal)1.15;
            }

            return precioFinal;
        }
    }
}
