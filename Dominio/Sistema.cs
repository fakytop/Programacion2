using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Sistema
    {
        //Definimos los atributos, son listas de objetos.
        private static Sistema instancia;
        private List<Lugar> lugares = new List<Lugar>();
        private List<Actividad> actividades = new List<Actividad>();
        private List<Compra> compras = new List<Compra>();
        private List<Usuario> usuarios = new List<Usuario>();
        private List<Categoria> categorias = new List<Categoria>();



        //PARÁMETROS:
        public static Sistema Instancia
        {
            get
            {
                if (instancia == null) instancia = new Sistema();
                return instancia;
            }
        }

        public List<Compra> Compras
        {
            get { return compras; }
        }
        public List<Lugar> Lugares
        {
            get { return lugares; }
        }

        public List<Actividad> Actividades
        {
            get { return actividades; }
        }

        public List<Usuario> Usuarios
        {
            get { return usuarios; }
        }

        public bool registrarUsuario(Usuario nuevoUsuario) ////HAY QUE HACER VALIDACIONES PARA EL REGISTRO - aca se agrega a la lista
        {
            bool valido = false;
            if (nuevoUsuario.ValidarNuevoUsuario() && !usuarios.Contains(nuevoUsuario))
            {
                Usuario nuevoU = new Usuario(nuevoUsuario.Nombre, nuevoUsuario.Apellido, nuevoUsuario.EMail, nuevoUsuario.FechaNacimiento, nuevoUsuario.Contrasena, nuevoUsuario.NombreUsuario);
                usuarios.Add(nuevoU);
                valido = true;
            }
            return valido;
        }



            //CONSTRUCTOR:
            private Sistema()
        {
            PrecargaDatos();
        }
      
        //MÉTODOS:

        //Agrega un lugar de clase Cerrado, haciendo controles necesarios.
        public void AgregarLugarCerrado(string pMantenimiento, string pCapacidadTotal, string pNombre, string pDimM2)
        {
            decimal mantenimiento = EsDecimal(pMantenimiento);
            decimal dimM2 = EsDecimal(pDimM2);
            int capacidad = EsEntero(pCapacidadTotal);

            if (mantenimiento > (decimal)0 && capacidad > 0 && NoVacio(pNombre) && dimM2 > (decimal)0)
            {
                Cerrado unLugarCerrado = new Cerrado(capacidad, pNombre, dimM2);
                lugares.Add(unLugarCerrado);
            }
        }

        //Agrega un lugar de clase Abierto, haciendo los controles necesarios.
        public void AgregarLugarAbierto(string pTexto, string pM2)
        {
            decimal m2 = EsDecimal(pM2);
            if (NoVacio(pTexto) && m2 > 0)
            {
                Abierto nuevoLugarAbierto = new Abierto(pTexto, m2);
                lugares.Add(nuevoLugarAbierto);
            }

        }

        public void AgregarActividad(string nombre, string nombreCat, DateTime fecha, string pIdLugar, string tipoPublico, string precioBase)
        {

            decimal precio = EsDecimal(precioBase);
            int id = EsEntero(pIdLugar);

            if (nombre != "" && nombreCat != "" && DevolverLugar(id) != null && ValidarTipoPublico(tipoPublico) && precio > 0)
            {
                Actividad nuevaActividad = new Actividad(nombre, DevolverCategoria(nombreCat), fecha, DevolverLugar(id), tipoPublico, precio);
                actividades.Add(nuevaActividad);
            }
        }

        public bool ValidarTipoPublico(string publico)
        {
            bool valido = false;
            if(publico.ToUpper() == "P" || publico.ToUpper() == "C13" || publico.ToUpper() == "C16" || publico.ToUpper() == "C18")
            {
                valido = true;
            }

            return valido;
        }

        public Categoria DevolverCategoria(string pNombre)
        {
            int i = 0;
            bool bandera = false;
            Categoria nuevaCategoria = categorias[i];

            do
            {
                i = 0;
                while (!bandera && i < categorias.Count)
                {
                    if (categorias[i].Nombre == pNombre)
                    {
                        nuevaCategoria = categorias[i];
                        bandera = true;
                    }
                    i++;
                }

                //if(!bandera)
                //{
                //    Console.WriteLine("La categoría no fue encontrada, vuelva a ingresarla por favor:");
                //    pNombre = Console.ReadLine();
                //}

            } while (!bandera);
            


            return nuevaCategoria;
        }
        public Lugar DevolverLugar(int pId)
        {
            int i = 0;
            bool bandera = false;
            Lugar nuevoLugar = Lugares[i];

            do
            {
                i = 0;
                while (!bandera && i < Lugares.Count)
                {
                    if (Lugares[i].Id == pId)
                    {
                        nuevoLugar = Lugares[i];
                        bandera = true;
                    }
                    i++;
                }

                //if (!bandera)
                //{
                //    Console.WriteLine("El lugar no fue encontrado, vuelva a ingresar su id por favor:");
                //    pId = EsEntero(Console.ReadLine());
                        
                //}
            } while (!bandera);

            return nuevoLugar;
        }

        public bool NoVacio(string pTexto)
        {
            bool valido = false;
            do
            {
                if (pTexto != "")
                {
                    valido = true;
                }
                //else
                //{
                //    Console.WriteLine("El campo no puede quedar vacío.");
                //    pTexto = Console.ReadLine();
                //}
            } while (!valido);
            

            return valido;
        }
        public decimal EsDecimal(string pNum)
        {
            decimal num = 0;
            try
            {
                num = decimal.Parse(pNum);

            }
            catch
            {

                num = 0;
            }
            return num;
        }

        public int EsEntero(string pNum)
        {
            int num = 0;
                try
                {
                    num = int.Parse(pNum);
            
                }
                catch
                {
                    num = 0;
                }
            
            return num;
        }

        public bool AdmCambiarAforo(string unNumero)
        {
            bool valido = false;
            int numeroEntero = EsEntero(unNumero);

            if (numeroEntero > 0 && numeroEntero <= 100)
            {
                Cerrado.CambiarAforo(numeroEntero);
                valido = true;
            }

            return valido;
        }

        public bool AdmCambiarPrecioButaca(string unDecimal)
        {
            bool valido = false;
            decimal numeroDecimal = EsDecimal(unDecimal); 
            if (numeroDecimal > 0)
            {
                Abierto.CambiarPrecioButaca(numeroDecimal);
                valido = true;
            }

            return valido;
        }

        public List<Categoria> ListaCategorias()
        {
            List<Categoria> aux = new List<Categoria>();
            foreach(Categoria i in categorias)
            {
                if(!aux.Contains(i))
                {
                    aux.Add(i);
                }
            }

            return aux;
        }

        public List<Actividad> ListarActividades()
        {
            List<Actividad> auxList = new List<Actividad>();
            foreach(Actividad i in actividades)
            {
                auxList.Add(i);
            }

            return auxList;
        }
        public bool EncontrarCategoria(string nombreCat)
        {
            bool valido = false;
            foreach(Categoria i in categorias)
            {
                if(i.Nombre.ToLower() == nombreCat.ToLower())
                {
                    valido = true;
                    break;
                }
            }

            return valido;
        }

        public DateTime GuardarFecha(string anio, string mes, string dia)
        {
            DateTime fecha = new DateTime();
            bool bandera = false;
            do
            {
                int anioFecha = EsEntero(anio);
                int mesFecha = EsEntero(mes);
                int diaFecha = EsEntero(dia);
                if (mesFecha > 0 && mesFecha <= 12 && diaFecha > 0 && diaFecha <= 31)
                {

                    DateTime fechaAux = new DateTime(anioFecha, mesFecha, diaFecha);
                    fecha = fechaAux;
                    bandera = true;
                }
                //else
                //{
                //    Console.WriteLine("La fecha ingresada no es correcta, vuelva a intentarlo por favor: Año / Mes / Día");
                //    anio = Console.ReadLine();
                //    mes = Console.ReadLine();
                //    dia = Console.ReadLine();
                //}
            } while (!bandera);

            return fecha;
        }
        public List<Actividad> ListarActividadesPorCategoria(string nombreCategoria, DateTime fechaDesde, DateTime fechaHasta)
        {
            List<Actividad> auxList = new List<Actividad>();
            bool bandera = false;

            do
            {
                if (EncontrarCategoria(nombreCategoria) && fechaDesde <= fechaHasta)
                {
                    foreach (Actividad i in actividades)
                    {
                        if (nombreCategoria.ToLower() == i.DevolverNombreCategoria().ToLower() && fechaDesde <= i.Fecha && fechaHasta >= i.Fecha)
                        {
                            auxList.Add(i);
                        }
                    }
                    bandera = true;
                }
                else
                {
                    auxList = null;
                }
            } while (!bandera);
            return auxList;

        }

        public List<Actividad> ListarActividadesParaTodoPublico()
        {
            List<Actividad> aux = new List<Actividad>();
            foreach(Actividad i in actividades)
            {
                if(i.TipoPublico.ToUpper() == "P")
                {
                    aux.Add(i);
                }
            }

            return aux;
        }
        
        
        public void AgregarCategoria(string nombre, string descripcion)
        {
            if(nombre != "" && descripcion != "")
            {
                Categoria nuevaCat = new Categoria(nombre, descripcion);
                categorias.Add(nuevaCat);
            }
        }
        public void PrecargaDatos()
        {
            AgregarCategoria("Deportes", "Partidos de fútbol y básquetbol.");
            AgregarCategoria("Paseos", "Paseos al aire libre");
            AgregarCategoria("Carnaval", "Fiesta nacional");
            AgregarCategoria("Compras", "Paseos de compras");
            AgregarCategoria("Concierto", "Conciertos musicales");
            AgregarCategoria("Jardineria", "Todo sobre espacios verdes.");
            AgregarCategoria("Cine", "Para los cineastas.");
            AgregarCategoria("Museo", "Dedicado a los historiadores.");
            AgregarCategoria("Juegos", "Dedicado a los mas pequeños.");

            AgregarLugarAbierto("Estadio Centenario", "7140");                          //1
            AgregarLugarAbierto("Teatro de Verano", "164");                             //2
            AgregarLugarAbierto("Parque Rodó", "10000");                                //3
            AgregarLugarAbierto("Jardín Botánico", "132500");                           //4
            AgregarLugarAbierto("Estadio Campeón del Siglo", "7000");                   //5
            AgregarLugarAbierto("La Colmena de Olavarría", "2000000");                  //6
            
            AgregarLugarCerrado("20000", "1145", "Teatro Solis", "200");                //7
            AgregarLugarCerrado("250", "10", "Juegos Mentales", "55");                  //8
            AgregarLugarCerrado("17000", "12000", "Antel Arena", "40000");              //9
            AgregarLugarCerrado("25000", "200", "Museo de Historia del Arte", "200");   //10
            AgregarLugarCerrado("11500", "2000", "Shopping Punta Carretas", "178");     //11


            DateTime fecha1 = new DateTime(2020, 12, 12);
            DateTime fecha2 = new DateTime(2021, 08, 05);
            DateTime fecha3 = new DateTime(2021, 10, 03);
            DateTime fecha4 = new DateTime(2021, 02, 03);
            DateTime fecha5 = new DateTime(2020, 08, 04);
            AgregarActividad("Despedida del T8ny", "Deportes", fecha1, "5", "P", "250");        //LUGAR ABIERTO
            AgregarActividad("Barco Pirata", "Paseos", fecha1, "3", "C16", "50");                 //LUGAR ABIERTO
            AgregarActividad("Divididos", "Concierto", fecha2, "2", "C16", "150");                //LUGAR ABIERTO
            AgregarActividad("Charla sobre plantas", "Jardineria", fecha3, "4", "C18", "250");    //LUGAR ABIERTO
            AgregarActividad("Uruguay vs Argentina", "Deportes", fecha4, "1", "P", "300");      //LUGAR ABIERTO
            AgregarActividad("Concierto PR y Los Redondos", "Concierto", fecha3, "6", "P", "1600");

            AgregarActividad("Escuela Filarmonica", "Concierto", fecha3, "7", "P", "400");            //LUGAR CERRADO
            AgregarActividad("Paw Patrol", "Cine", fecha4, "11", "C13", "200");                       //LUGAR CERRADO   
            AgregarActividad("Charla sobre pinturas de Picasso", "Museo", fecha5, "10", "P", "200");   //LUGAR CERRADO  
            AgregarActividad("Marama", "Concierto", fecha5, "9", "C18", "300");                       //LUGAR CERRADO  
            AgregarActividad("actividades para Niños", "Juegos", fecha4, "8", "P", "120");          //LUGAR CERRADO

            AgregarNuevoUsuarioHC("Gonzalo", "Pérez", "gonzaloP@gmail.com", "1994-02-28", "GonzaP1234", "GonzaP", "OPERADOR");
            AgregarNuevoUsuarioHC("Diego", "Gagliano", "diegoG@gmail.com", "1989-09-16", "DiegoG1234", "DiegoG", "OPERADOR");
            AgregarNuevoUsuarioHC("Juan Carlos", "Schelza", "holajuancarlos@comoestas.com", "1949-10-09", "AcaEstaElEspiritu123", "juancarlos", "CLIENTE");
            AgregarNuevoUsuarioHC("Luis", "Dentone", "LDentone@Ldentone.com", "1980-10-09", "Ldentone123", "Ldentone", "CLIENTE");

            AgregarCompra(1, 5, "juancarlos", fecha4, true);
            AgregarCompra(2, 2, "juancarlos", fecha3, true);
            AgregarCompra(3, 15, "juancarlos", fecha4, true);
            AgregarCompra(4, 10, "juancarlos", fecha5, true);
            AgregarCompra(5, 3, "Ldentone", fecha1, true);
            AgregarCompra(8, 7, "Ldentone", fecha2, true);
            AgregarCompra(9, 9, "Ldentone", fecha3, true);
            AgregarCompra(10, 12, "Ldentone", fecha2, true);
        }

        public bool AgregarNuevoUsuarioHC(string pNombre, string pApellido, string pEMail, string pFecha, string pContrasena, string pNombreUsuario, string pRol)
        {
            bool valido = false;
            Usuario unU = new Usuario(pNombre, pApellido, pEMail, DevolverFecha(pFecha), pContrasena, pNombreUsuario, pRol);

            if (unU.ValidarNuevoUsuario(pNombre, pApellido, pEMail, pContrasena) && DevolverFecha(pFecha) != null)
            {
                usuarios.Add(unU);
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

            DateTime fechaDev = new DateTime(year,mes,dia);

            return fechaDev;
        }
        

//        public void MenuPrincipal()
//        {
//            int opcion;
//            do
//            {
//                string bienvenida = "Bienvenido al sistema de La Dirección Nacional de Cultura. \nSeleccione una opción: \n1 - Listar actividades. \n2 - Cambiar valor del aforo máximo. ";
//                bienvenida += "\n3 - Cambiar precio de butacas de lugares abiertos. \n4 - Listar actividades según categoría, en un período dado.";
//                bienvenida += "\n5 - Listar espectáculos para todo público. \n0 - Salir.";
//                Console.WriteLine(bienvenida);
//                opcion = ConvertirNumero(Console.ReadLine());
//                switch (opcion)
//                {
//                    case 1:
//                        Console.Clear();
//                        MostrarListaActividadFiltrada(ListarActividades());
//                        MenuPrincipal();
//                        break;
//                    case 2:
//                        Console.Clear();
//                        Console.WriteLine("Ingrese el aforo máximo para lugares cerrados (porcentual):");
//                        string aforo = Console.ReadLine();
//                        AdmCambiarAforo(aforo);
//                        MenuPrincipal();
//                        break;
//                    case 3:
//                        Console.Clear();
//                        Console.WriteLine("Ingrese el nuevo precio por butaca:");
//                        string precio = Console.ReadLine();
//                        AdmCambiarPrecioButaca(precio);
//                        MenuPrincipal(); 
//                        break;
//                    case 4:
//                        Console.Clear();
//                        Console.WriteLine("Escriba la categoría deseada:");
//                        string categoria = Console.ReadLine();
//                        Console.WriteLine("Desde fecha: (AAAA (Enter) MM (Enter) DD (Enter)");
//                        string anioDesde = Console.ReadLine();
//                        string mesDesde = Console.ReadLine();
//                        string diaDesde = Console.ReadLine();
//                        Console.WriteLine("Hasta fecha: (AAAA (Enter) MM (Enter) DD (Enter)");
//                        string anioHasta = Console.ReadLine();
//                        string mesHasta = Console.ReadLine();
//                        string diaHasta = Console.ReadLine();
////                        MostrarListaActividadFiltrada(ListarActividadesPorCategoria(categoria, anioDesde, mesDesde, diaDesde, anioHasta, mesHasta, diaHasta));
//                        MenuPrincipal();
//                        break;
//                    case 5:
//                        Console.Clear();
//                        MostrarListaActividadFiltrada(ListarActividadesParaTodoPublico());
//                        MenuPrincipal();
//                        break;
//                    default:
//                        break;
//                }
//            } while (opcion != 0);
//        }

        public int ConvertirNumero(string pNum)
        {
            bool valido = false;
            int numero = -1;
            do
            {
                try
                {
                    numero = int.Parse(pNum);
                    valido = true;
                }
                catch
                {
                    //Console.WriteLine("El dato ingresado no es correcto, por favor vuelva a intentarlo");
                    //pNum = Console.ReadLine();
                }
            } while (!valido);

            return numero;
        }

        public void MostrarListaActividadFiltrada(List<Actividad> pLista)
        {
            string mostrar = "";
            foreach(Actividad i in pLista)
            {
                mostrar += i.ToString();
            }
            Console.WriteLine(mostrar);
        }

        public List<Compra> ComprasPorFecha(DateTime fechaDesde, DateTime fechaHasta)
        {
            List<Compra> aux = new List<Compra>();
            foreach (Compra item in compras)
            {
                if(item.FechayHoraCompra > fechaDesde && item.FechayHoraCompra < fechaHasta)
                {
                    aux.Add(item);
                }
            }

            return aux;
        }

        public decimal SumaTotalCompras(DateTime fechaDesde, DateTime fechaHasta)
        {
            decimal total = 0;
            foreach (Compra item in compras)
            {
                if(item.FechayHoraCompra > fechaDesde && item.FechayHoraCompra < fechaHasta)
                {
                    total += item.TotalCompra();
                }
            }
            return total;
        }
        public decimal SumaTotalCompras(List<Compra> listaCompras)
        {
            decimal total = 0;
            foreach (Compra item in listaCompras)
            {
                total += item.TotalCompra();
            }
            return total;
        }

        public List<Compra> ComprasPorUsuario(string nombreUsuarioLog)
        {
            List<Compra> aux = new List<Compra>();
            foreach (Compra item in compras)
            {
                if(nombreUsuarioLog == item.UsuarioCompra.NombreUsuario)
                {
                    aux.Add(item);
                }
            }
            return aux;
        }

        public List<Usuario> ClientesOrdenados()
        {
            List<Usuario> aux = new List<Usuario>();
            foreach (Usuario item in usuarios)
            {
                if (item.Rol == "CLIENTE" && !aux.Contains(item))
                {
                    aux.Add(item);
                }
                
            }
            aux.Sort();

            return aux;
        }

        public Usuario ObtenerUsuario(string pNombre, string pPassword)
        {
            Usuario unU = null;
            foreach (Usuario item in usuarios)
            {
                if(item.NombreUsuario == pNombre && item.Contrasena == pPassword)
                {
                    unU = item;
                    break;
                }
            }

            if(unU != null)
            {
                return unU;
            } else
            {
                return null;
            }
        }

        public Usuario ObtenerUsuario(string pNombre)
        {
            Usuario unU = null;
            foreach (Usuario item in usuarios)
            {
                if (item.NombreUsuario == pNombre)
                {
                    unU = item;
                    break;
                }
            }

            if (unU != null)
            {
                return unU;
            }
            else
            {
                return null;
            }
        }

        public Compra ObtenerCompra(int id)
        {
            Compra unaC = null; 
            foreach(Compra item in compras)
            {
                if(item.Id == id)
                {
                    unaC = item;
                    break;
                }
            }
            return unaC;
        }
        public Actividad ObtenerActividad(int id)
        {
            Actividad unA = null;
            foreach (Actividad item in actividades)
            {
                if(item.Id == id)
                {
                    unA = item;
                    break;
                }
            }

            return unA;


        }

        public List<Actividad> ActividadesSegunNombreLugar(string nombreLugar)
        {
            List<Actividad> aux = new List<Actividad>();
            foreach (Actividad item in actividades)
            {
                if (item.Lugar.Nombre == nombreLugar)
                {
                    aux.Add(item);
                }
            }

            return aux;
        }

        public bool DarMeGustaAlaActividad(int idActividad)
        {
            bool meGusta = false;


            foreach (Actividad unA in actividades)
            {
                if (unA.Id == idActividad)
                {
                    unA.AgregarMeGusta();
                    meGusta = true;
                }
            }

            return meGusta;

        } 

        public List<Compra> ComprasDeMayorCosto()
        {
            List<Compra> aux = new List<Compra>();
            decimal mayorCosto = decimal.MinValue;
            foreach (Compra item in compras)
            {
                if(item.TotalCompra() > mayorCosto)
                {
                    mayorCosto = item.TotalCompra();
                    aux.Clear();
                    aux.Add(item);
                } 
                else if(item.TotalCompra() == mayorCosto)
                {
                    aux.Add(item);
                }
            }

            return aux;
        }

        public void AgregarCompra(int pActividad, int pCantidadDeCompra, string pUsuarioCompra, DateTime pFechaYHoraCompra, bool pEstadoActivo)
        {

            Compra unC = new Compra(ObtenerActividad(pActividad), pCantidadDeCompra, ObtenerUsuario(pUsuarioCompra), pFechaYHoraCompra, pEstadoActivo);

            compras.Add(unC);


        }
    }
}

