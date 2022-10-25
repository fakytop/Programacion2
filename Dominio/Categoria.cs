using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Categoria
    {
        private string nombre;
        private string descripcion;

        public string Nombre
        {
            get { return nombre; }
        }

        //CONSTRUCTOR:
        public Categoria(string pNombre, string pDescripcion)
        {
            this.nombre = pNombre;
            this.descripcion = pDescripcion;
        }

        public override bool Equals(object obj)
        {
            Categoria otroC = obj as Categoria;
            return otroC != null && otroC.Nombre == nombre;
        }
    }

    
}
