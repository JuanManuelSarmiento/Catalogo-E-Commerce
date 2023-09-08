using Catalogo.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Negocio
{
    public class CategoriaNegocio : IABML<Categoria>
    {
        public void Add(Categoria newEntity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Categoria entity)
        {
            throw new NotImplementedException();
        }

        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoADatos datos = new AccesoADatos();
            try
            {
                datos.SetConsulta("SELECT Id, Descripcion from CATEGORIAS");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria
                    {
                        Id = (int)datos.Lector["Id"],
                        Descripcion = (string)datos.Lector["Descripcion"]
                    };

                    lista.Add(aux);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
            return lista;
        }

        public void Update(Categoria entity)
        {
            throw new NotImplementedException();
        }
    }
}
