using Catalogo.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Negocio
{
    public class ArticuloNegocio : IABML<Articulo>
    {
        public void Add(Articulo newEntity) {
            AccesoADatos datos = new AccesoADatos();
            try
            {
                datos.SetConsulta("INSERT INTO ARTICULOS (Codigo,Nombre ,Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) VALUES (@codigo,@nombre,@descripcion,@idMarca,@idCategoria,@urlImagen,@precio);");
                datos.SetParametro("@codigo", newEntity.Codigo);
                datos.SetParametro("@nombre", newEntity.Nombre);
                datos.SetParametro("@descripcion", newEntity.Descripcion);
                datos.SetParametro("@idMarca", newEntity.Marca.Id);
                datos.SetParametro("@idCategoria", newEntity.Categoria.Id);
                datos.SetParametro("@urlImagen", newEntity.ImagenURL);
                datos.SetParametro("@precio", newEntity.Precio);

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

        public void Delete(Articulo Entity)
        {

        }

        public void Update(Articulo Entity) { }

        public List<Articulo> Listar()
        {
            List<Articulo> articulos = new List<Articulo>();
            AccesoADatos datos = new AccesoADatos();

            try
            {
                datos.SetConsulta("SELECT A.codigo, A.nombre, A.descripcion, A.ImagenURL, A.precio, C.descripcion categoria, M.descripcion marca FROM ARTICULOS A, Categorias C, Marcas M\r\nWHERE A.IdMarca = M.Id AND C.Id = A.IdCategoria;");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Marca = new Marca();
                    aux.Categoria = new Categoria();

                    aux.Codigo = (string)datos.Lector["codigo"];
                    aux.Nombre = (string)datos.Lector["nombre"];
                    aux.Descripcion = (string)datos.Lector["descripcion"];
                    aux.Marca.Descripcion = (string)datos.Lector["marca"];
                    aux.Categoria.Descripcion = (string)datos.Lector["categoria"];
                    aux.ImagenURL = (string)datos.Lector["ImagenURL"];
                    aux.Precio = (decimal)datos.Lector["precio"];

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
