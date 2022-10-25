using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Usuario : IComparable<Usuario>
    {
        private int id;
        private string nombre;
        private string apellido;
        private string eMail;
        private DateTime fechaDeNacimiento;
        private static int ultId = 0;
        private string contrasena;
        private string nombreUsuario;
        private string rol;
        
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

        public string Apellido
        {
            get { return apellido; } 
            set { apellido = value; }
        }

        public string EMail
        {
            get { return eMail; } 
            set { eMail = value; }
        }

        public DateTime FechaNacimiento
        {
            get { return fechaDeNacimiento; } 
            set { fechaDeNacimiento = value; }
        }

        public string Contrasena
        {
            get { return contrasena; }
            set { contrasena = value; }
        }

        public string NombreUsuario
        {
            get { return nombreUsuario; }
            set { nombreUsuario = value; }
        }
        
        public string Rol
        {
            get { return rol; }
            set { rol = value; }
        }
        
        
        public Usuario(string pNombre, string pApellido, string pEMail, DateTime pFechaDeNacimiento, string pContrasena, string nombreUsuario, string pRol)
        {
            this.id = ++ultId;
            this.nombre = pNombre;
            this.apellido = pApellido;
            this.eMail = pEMail;
            this.fechaDeNacimiento = pFechaDeNacimiento;
            this.contrasena = pContrasena;
            this.nombreUsuario = nombreUsuario;
            this.rol = pRol; 
        }

        public Usuario(string pNombre, string pApellido, string pEMail, DateTime pFechaDeNacimiento, string pContrasena, string nombreUsuario)
        {
            this.id = ++ultId;
            this.nombre = pNombre;
            this.apellido = pApellido;
            this.eMail = pEMail;
            this.fechaDeNacimiento = pFechaDeNacimiento;
            this.contrasena = pContrasena;
            this.nombreUsuario = nombreUsuario;
            this.rol = "CLIENTE";
        }

        public Usuario() //para 
        {

        }
        public bool ValidarNuevoUsuario(string pNombre, string pApellido, string pEMail, string password)
        {
            bool valido = false;
            if (NoVacio(pNombre) && NoVacio(pApellido) && NoVacio(pEMail) && ValidarArroba(pEMail) && ControlarPassword(password))
            {
                valido = true;
            }
            return valido;
        }

        public bool ValidarNuevoUsuario()
        {
            return ValidarNuevoUsuario(this.nombre, this.apellido,this.eMail,this.contrasena);
        }

        public bool NoVacio(string pTexto)
        {
            bool valido = false;
            if(pTexto.Length >= 2)
            {
                valido = true;
            }
            return valido;
        }

        public bool ValidarArroba(string pMail)
        {
            bool valido = false;
            if(pMail.Contains("@"))
            {
                valido = true;
            }

            return valido;
        }


        public DateTime DevolverFecha(string fecha)
        {
            int year = 0;
            int mes = 0;
            int dia = 0;

            string guardar = "";
            for (int i = 0; i < 4; i++)
            {
                guardar += fecha[i];
            }

            year = int.Parse(guardar);
            guardar = "";

            for (int i = 5; i < 7; i++)
            {
                guardar += fecha[i];
            }

            mes = int.Parse(guardar);
            guardar = "";

            for (int i = 8; i < 10; i++)
            {
                guardar += fecha[i];
            }

            dia = int.Parse(guardar);

            DateTime fechaDev = new DateTime(year, mes, dia);

            return fechaDev;
        }

        public bool Mayuscula(char i)
        {
            string aMay = i.ToString().ToUpper();
            string aOrig = i.ToString();

            bool valido = false;
            if(aOrig == aMay)
            {
                valido = true;
            }
            return valido;
        }

        public bool Minuscula(char i)
        {
            string aMin = i.ToString().ToLower();
            string aOrig = i.ToString();

            bool valido = false;
            
            if(aMin == aOrig)
            {
                valido = true;
            }
            return valido;
        }
        
        public bool Numero(char i)
        {
            string aNum = i.ToString();
            bool valido = false;
            int num = 0;
            try
            {
                num = int.Parse(aNum);
                valido = true;
            }
            catch
            {
                valido = false;
            }
            return valido;
        }

        public bool ControlarPassword(string pPass)
        {
            bool resultadoValido = false;
            bool minuscula = false;
            bool mayuscula = false;
            bool numero = false;

            for (int i = 0; i < pPass.Length; i++)
            {
                char a = pPass[i];

                if (Numero(a))
                {//codigo ASCII numeros.
                    numero = true;
                }
                else if (Mayuscula(a))
                {//codigo ASCII letras mayusculas
                    mayuscula = true;
                }
                else if (Minuscula(a))
                {//codigo ASCII letras minusculas
                    minuscula = true;
                }
            }

            if (numero && mayuscula && minuscula && pPass.Length >= 6)
            {
                resultadoValido = true;
            }
            return resultadoValido;
        }
        public override bool Equals(object obj)
        {
            Usuario unU = obj as Usuario;
            return unU != null && (nombreUsuario == unU.NombreUsuario || eMail == unU.EMail);
        }

        public int CompareTo(Usuario other)
        {
            int orden = nombre.CompareTo(other.Apellido);
            if(orden == 0)
            {
                orden = apellido.CompareTo(other.Nombre);
            }

            return orden;
        }

    }
}
