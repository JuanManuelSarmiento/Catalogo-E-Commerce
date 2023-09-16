using Catalogo.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Negocio
{
    public class MarcaNegocio : IABML<Marca>
    {
        public void Add(Marca newEntity)
        {
            AccesoADatos datos = new AccesoADatos();
            try
            {
                datos.SetConsulta("INSERT INTO MARCAS(Id, Descripcion) \r\nVALUES (@id,@descripcion);");
                datos.SetParametro("@id", newEntity.Id);
                datos.SetParametro("@descripcion", newEntity.Descripcion);

                datos.EjecutarLectura();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Delete(Marca entity)
        {
            throw new NotImplementedException();
        }

        public List<Marca> Listar()
        {
            List<Marca> lista = new List<Marca>();
            AccesoADatos datos = new AccesoADatos();
            try
            {
                datos.SetConsulta("SELECT Id, Descripcion from MARCAS");
                datos.EjecutarLectura();
                while(datos.Lector.Read())
                {
                    Marca aux = new Marca
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

        public void Update(Marca entity)
        {
            throw new NotImplementedException();
        }
    }
}
