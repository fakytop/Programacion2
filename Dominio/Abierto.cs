using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Abierto : Lugar
    {
        private static decimal precioPorButaca;

        //PROPERTIES:

        public decimal PrecioPorButaca
        {
            get { return precioPorButaca; }
        }

        //CONSTRUCTOR de la clase:
        public Abierto(string pNombre, decimal pDimM2) : base(pNombre, pDimM2)
        {  
        }

        public static void CambiarPrecioButaca(decimal unDecimal)
        {
            precioPorButaca = unDecimal;
        }

        public override string ToString()
        {
            return $"{Nombre}";
        }

        public override decimal CostoAdicional(decimal precio)
        {
            decimal costoMant= 0;
            decimal precioFinal = base.CostoAdicional(precio);
            if (DimM2 > 1000000)
            {
                costoMant = precioFinal * (decimal)0.10;
                if (costoMant > 10)
                {
                    costoMant *= (decimal)1.05;
                }
            }

            precioFinal += costoMant;

            return precioFinal += PrecioPorButaca;

        }
    }
}
