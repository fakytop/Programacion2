using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Actividad
    {
        private int id;
        private string nombre;
        private Categoria categoria;
        private DateTime fechaYHora;
        private Lugar lugar;
        private string tipoPublico;
        private decimal precioBase;
        private int qMeGusta = 0;
        private static int ultId = 0;

        public int Id
        {
            get { return id; }
        }

        public string Nombre
        {
            get { return nombre; }
        }

        public Categoria Categoria
        {
            get { return categoria; }
        }
        public DateTime Fecha
        {
            get { return fechaYHora; }
        }

        public Lugar Lugar
        {
            get { return lugar; }
        }
        public string TipoPublico
        {
            get { return tipoPublico; }
        }

        public decimal PrecioBase
        {
            get { return precioBase; }
        }

        public int QMeGusta
        {
            get { return qMeGusta; }
        }

        //CONSTRUCTOR de la clase:
        public Actividad(string pNombre, Categoria pCategoria, DateTime pFechaYHora, Lugar pLugar, string pTipoPublico, decimal pPrecioBase)
        {
            this.nombre = pNombre;
            this.categoria = pCategoria;
            this.fechaYHora = pFechaYHora;
            this.lugar = pLugar;
            this.tipoPublico = pTipoPublico;
            this.precioBase = pPrecioBase;
            this.qMeGusta = 0;
            this.id = ++ultId;
        }

        public Actividad()
        {

        }

        //Mostrar actividad:
        public override string ToString()
        {
            string mostrar = $"{id} - {nombre} - {lugar} - {tipoPublico} \n";
            return mostrar;
        }

        public string DevolverNombreCategoria()
        {
            string nombre = "";
            nombre = categoria.Nombre;

            return nombre;
        }

        public decimal CalcularCostoFinal()
        {
            decimal costoFinal = Lugar.CostoAdicional(PrecioBase);
            return costoFinal;
        }

        public void AgregarMeGusta()
        {
            qMeGusta++;
        }

        
    }
}
