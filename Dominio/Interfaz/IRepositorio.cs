using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaz
{
    public interface IRepositorio<T> where T : class
    {
        bool Add(T obj);

        T FindById(object clave);

        IEnumerable<T> FindAll();

        bool Update(T obj);

        bool Remove(object clave);
    }
}
