using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Compra
    {
        private int id;
        private Actividad actividad;
        private int cantidadDeCompra;
        private Usuario usuarioCompra;
        private DateTime fechaYHoraCompra;
        private bool estadoActivo;
        private static int ultId = 0;
        //private Usuario usuario;
        //private Actividad unaActividad;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public Actividad Actividad
        {
            get { return actividad; }
            set { actividad = value; }
        }

        public int CantidadDeCompra
        {
            get { return cantidadDeCompra; }
            set { cantidadDeCompra = value; }
        }

        public Usuario UsuarioCompra
        {
            get { return usuarioCompra; }
            set { usuarioCompra = value; }
        }

        public DateTime FechayHoraCompra
        {
            get { return fechaYHoraCompra; }
            set { fechaYHoraCompra = value; }
        }

        public bool EstadoActivo
        {
            get { return estadoActivo; }
            set { estadoActivo = value; }
        }





        //CONSTRUCTOR de la clase Compra
        public Compra(Actividad pActividad, int pCantidadDeCompra, Usuario pUsuarioCompra, DateTime pFechaYHoraCompra, bool pEstadoActivo)
        {
            this.id = ++ultId;
            this.actividad = pActividad;
            this.cantidadDeCompra = pCantidadDeCompra;
            this.usuarioCompra = pUsuarioCompra;
            this.fechaYHoraCompra = pFechaYHoraCompra;
            this.estadoActivo = pEstadoActivo;

        }

        public Compra()
        {

        }

        public decimal TotalCompra()
        {
            decimal total = actividad.CalcularCostoFinal() * (decimal)cantidadDeCompra;
            return total;
        }
    }
}