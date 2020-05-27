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
            throw new NotImplementedException();
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
                string Usuario = directorio + "\\Usuario.txt";
                string Producto = directorio + "\\Producto.txt";
                if (!Directory.Exists(directorio))//Si no existe el directorio lo crea.
                {
                    Directory.CreateDirectory(directorio);
                }
               if (File.Exists(Usuario))//Controla que exista el archivo de los usuarios a precargar
                {
                    StreamReader sr = new StreamReader(Usuario);
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
                        
                        linea = sr.ReadLine();
                        if (db.Usuarios.Any(p => p.CI == u.CI))
                        {
                            //todo
                        }
                        else
                        {
                            
                            db.Usuarios.Add(u);
                            db.SaveChanges();

                        }
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
