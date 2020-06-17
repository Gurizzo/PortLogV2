using Dominio.Clases;
using Dominio.Interfaz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class RepositorioUsuario : IRepositorio<Usuario>
    {
        private PortLogContext db = new PortLogContext();

        public bool Add(Usuario obj)
        {
            var validar = db.Usuarios.Where(u => u.CI == obj.CI).FirstOrDefault<Usuario>();
            if (validar == null)
            {
                if (obj.Validar())
                {
                    db.Usuarios.Add(obj);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            else
            {
                return false;

            }
        }

        public IEnumerable<Usuario> FindAll()
        {
            throw new NotImplementedException();
        }

        public Usuario FindById(object clave)
        {
            throw new NotImplementedException();
        }

        public bool Remove(object clave)
        {
            throw new NotImplementedException();
        }

        public bool Update(Usuario obj)
        {
            throw new NotImplementedException();
        }

        public string Login(string ci, string password)
        {
            try
            {
                
                if (db.Usuarios.Any(p => p.CI == ci && p.Password == password))// Pregunto si tiene almacenado una cedula y contraseña iguales.
                {
                    var usr = db.Usuarios.Find(ci);//Traigo el objeto usuario.
                    return usr.Rol;//Retorno el nombre del rol.
                }
                else
                {
                    return "";
                }

            }
            catch (Exception)
            {
                return "";
            }
            finally
            {

            }
            


            
        }

        public bool PreCarga()
        {
            try
            {
                string dirBase = AppDomain.CurrentDomain.BaseDirectory;
                string directorio = dirBase + "\\Carga";
                string usuario = directorio + "\\Usuario.txt";
                string producto = directorio + "\\Productos.txt";
                string cliente = directorio + "\\Cliente.txt";
                string importacion = directorio + "\\Importaciones.txt";
                if (!Directory.Exists(directorio))//Si no existe el directorio lo crea.
                {
                    Directory.CreateDirectory(directorio);
                }
               if (File.Exists(usuario))//Controla que exista el archivo de los usuarios a precargar
                {
                    StreamReader sr = new StreamReader(usuario);
                    string linea = sr.ReadLine();
                    while (linea != null)
                    {
                        string separador = "#";
                        string[] datos = linea.Split(separador.ToCharArray());
                        Usuario u = new Usuario()
                        {
                            CI = datos[0],
                            Password = datos[1],
                            Rol = datos[2]
                        };
                        this.Add(u);
                        linea = sr.ReadLine();
                        
                        
                    }
                    
                }
                if (File.Exists(cliente))
                {
                    RepositorioCliente repocli = new RepositorioCliente();
                    StreamReader sr = new StreamReader(cliente);
                    string linea = sr.ReadLine();
                    while (linea != null)
                    {
                        string separador = "#";
                        string[] datos = linea.Split(separador.ToCharArray());
                        Cliente c = new Cliente
                        {
                            Rut= datos[0],
                            Nombre=datos[1]
                        };
                        linea = sr.ReadLine();

                        repocli.Add(c);
                        
                    }
                }
                if (File.Exists(producto))
                {
                    RepositorioProducto repositorio = new RepositorioProducto();
                    StreamReader sr = new StreamReader(producto);
                    string linea = sr.ReadLine();
                    while (linea != null)
                    {
                        string separador = "#";
                        string[] datos = linea.Split(separador.ToCharArray());
                        Producto p = new Producto
                        {
                            Codigo = datos[0],
                            Nombre = datos[1],
                            Peso = decimal.Parse(datos[3]),
                            Cliente = db.Clientes.Find(datos[4])
                        };
                       
                        linea = sr.ReadLine();
                        repositorio.Add(p);
                        
                    }
                }
                if (File.Exists(importacion)){
                    StreamReader sr = new StreamReader(importacion);
                    string linea = sr.ReadLine();
                    RepositorioImportacion repositorioImportacion = new RepositorioImportacion();
                    while (linea != null)
                    {
                        string separador = "#";
                        string[] datos = linea.Split(separador.ToCharArray());
                        
                        Importacion i = new Importacion
                        {
                            Id=0,
                            FchIngreso= Convert.ToDateTime(datos[0]),
                            FchSalida= Convert.ToDateTime(datos[1]),
                            Producto= db.Productos.Find(datos[2]),
                        Precio = Convert.ToDecimal(datos[4]),
                            Cantidad= Convert.ToInt32(datos[5]),
                            Almacenado= Convert.ToBoolean(datos[6]),
                            
                            
                            
                        };
                        repositorioImportacion.Add(i);
                        linea = sr.ReadLine();
                        //db.Importaciones.Add(i);
                        //db.SaveChanges();
                    }
                }
                
                


            }
            catch (Exception)
            {
                
                return false;
            }
            finally
            {
                
                
            }
            return true;
        }
    }
}
