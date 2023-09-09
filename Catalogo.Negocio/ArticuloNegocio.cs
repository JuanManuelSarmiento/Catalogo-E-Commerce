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
                datos.SetConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) \r\nVALUES (@codigo,@nombre,@descripcion,@idMarca,@idCategoria,@precio);");
                datos.SetParametro("@codigo", newEntity.Codigo);
                datos.SetParametro("@nombre", newEntity.Nombre);
                datos.SetParametro("@descripcion", newEntity.Descripcion);
                datos.SetParametro("@idMarca", newEntity.Marca.Id);
                datos.SetParametro("@idCategoria", newEntity.Categoria.Id);
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

        public void Delete(Articulo newEntity)
        {
            AccesoADatos datos = new AccesoADatos();
            try
            {
                datos.SetParametro("@codigo", newEntity.Codigo);
                datos.SetConsulta("DELETE FROM ARTICULOS WHERE Codigo = @codigo");
                
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
        public void Update(Articulo Entity) { }

        public List<Articulo> Listar()
        {
            List<Articulo> articulos = new List<Articulo>();
            AccesoADatos datos = new AccesoADatos();

            try
            {
                datos.SetConsulta("SELECT A.Codigo, A.Nombre, A.Descripcion , M.Descripcion as Marca, C.Descripcion as Categoria, I.ImagenUrl, A.Precio FROM ARTICULOS A LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id LEFT JOIN MARCAS M ON A.IdMarca = M.Id LEFT JOIN IMAGENES I ON A.Id = I.IdArticulo");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Marca = new Marca();
                    aux.Categoria = new Categoria();
                    aux.Imagen = new Imagen();

                    if (!(datos.Lector["Codigo"] is DBNull))
                    aux.Codigo = (string)datos.Lector["Codigo"];

                    if (!(datos.Lector["Nombre"] is DBNull))
                    aux.Nombre = (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Descripcion"] is DBNull))
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    if (!(datos.Lector["Marca"] is DBNull))
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    if (!(datos.Lector["Categoria"] is DBNull))
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    aux.Imagen.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    if (!(datos.Lector["Precio"] is DBNull))
                    aux.Precio = (decimal)datos.Lector["Precio"];

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
