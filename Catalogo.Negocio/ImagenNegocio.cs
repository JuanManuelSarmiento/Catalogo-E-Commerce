using Catalogo.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Negocio
{
    public class ImagenNegocio : IABML<Imagen>
    {
        private AccesoADatos datos;
        public void Add(Imagen newEntity)
        {
            datos = new AccesoADatos();
            try
            {
                datos.SetConsulta("insert into IMAGENES values(@idArticulo,@url)");
                datos.SetParametro("@idArticulo", newEntity.IdArticulo);
                datos.SetParametro("@url", newEntity.ImagenUrl);
                datos.EjecutarLectura();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Delete(Imagen entity)
        {
            throw new NotImplementedException();
        }

        public List<Imagen> Listar()
        {
            throw new NotImplementedException();
            //datos = new AccesoADatos();
            //try
            //{
            //    datos.SetConsulta("Select Id,ImagenUrl from IMAGENES where IdArticulo = @articulo");
            //    datos.SetParametro("@articulo", art.Id);
            //    List<Imagen> lista = new List<Imagen>();
            //    while(datos.Lector.Read())
            //    {
            //        Imagen aux = new Imagen
            //        {
            //            Id = (int)datos.Lector["Id"],
            //            IdArticulo = art.Id,
            //            ImagenUrl = (string)datos.Lector["ImagenUrl"]
            //        };

            //        lista.Add(aux);
            //    }
            //    return lista;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    datos.CerrarConexion();
            //}
           
        }

        public void Update(Imagen entity)
        {
            throw new NotImplementedException();
        }
    }
}
