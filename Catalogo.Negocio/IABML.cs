using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalogo.Dominio;

namespace Catalogo.Negocio
{
    interface IABML<T>
    {
        void Create(T newEntity);
        void Delete(T entity);
        void Update(T entity);
        List<T> Listar();
    }

    class ArticuloNegocio : IABML<Articulo>
    {
        public void Create(Articulo newEntity) { }

        public void Delete(Articulo Entity) { }

        public void Update(Articulo Entity) { }

        public List<Articulo> Listar()
        {
            List<Articulo> articulos = new List<Articulo>();
            AccesoADatos datos = new AccesoADatos();

            try
            {
                datos.SetConsulta("SELECT A.codigo, A.nombre, A.descripcion FROM ARTICULOS A;");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Descripcion = (string)datos.Lector["Descirpcion"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Precio = (double)datos.Lector["Precio"];
                    aux.Codigo = (int)datos.Lector["Codigo"];
                    aux.UrlImagen = (string)datos.Lector["UrlImagen"];
                    aux.Marca = (Marca)datos.Lector["Marca"];
                    aux.Categoria = (Categoria)datos.Lector["Categoria"];

                    articulos.Add(aux);

                }

                return articulos;
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
    }
}
