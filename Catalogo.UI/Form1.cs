using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Catalogo.Dominio;
using Catalogo.Negocio;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Catalogo.UI
{
    public partial class frmArticulos : Form
    {
        private List<Articulo> listaArticulo;
        public frmArticulos()
        {
            InitializeComponent();
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            cargar();
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Marca");
            cboCampo.Items.Add("Categoria");
            cboCampo.Items.Add("Precio");
        }
        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                CargarImagen(seleccionado.Imagen.ImagenUrl);
            }
        }
        private void CargarImagen(string Imagen)
        {
            try
            {
                pbxArticulo.Load(Imagen);
            }
            catch (Exception)
            {
                pbxArticulo.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcROGVlwDhbC-6RixbdgEwDrABJ6BD3hhM2eJA&usqp=CAU");
            }
        }
        private void cargar()
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            try
            {
                listaArticulo = articuloNegocio.Listar();
                dgvArticulos.DataSource = listaArticulo;
                ocultarColumnas();
                //Probar Ajuste de Tamaño de Form con tamaño de grilla
                dgvArticulos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                pbxArticulo.Height = dgvArticulos.Height - 105;
                //------------------
                CargarImagen(listaArticulo[0].Imagen.ImagenUrl);
                //Probar Ajuste de Tamaño de Form con tamaño de grilla
                ajustarAltoGrilla();
                //------------------
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void ocultarColumnas()
        {
            dgvArticulos.Columns["id"].Visible = false;
            dgvArticulos.Columns["Imagen"].Visible = false;
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            /*
            List<Articulo> listaFiltrada;
            string filtro = txtFiltro.Text;

            if(filtro != "")
            {
                listaFiltrada = listaArticulo.FindAll(x => x.Codigo.ToUpper().Contains(filtro.ToUpper()) || x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Descripcion.ToUpper().Contains(filtro.ToUpper()) || x.Marca.Descripcion.ToUpper().Contains(filtro.ToUpper()) || x.Categoria.Descripcion.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                listaFiltrada = listaArticulo;
            }

            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            ocultarColumnas();
            */

            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {

                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text;

                dgvArticulos.DataSource = negocio.Filtrar(campo, criterio, filtro);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }


        }
        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = txtFiltro.Text;

            if (filtro.Length >=2)
            {
                listaFiltrada = listaArticulo.FindAll(x => x.Codigo.ToUpper().Contains(filtro.ToUpper()) || x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Descripcion.ToUpper().Contains(filtro.ToUpper()) || x.Marca.Descripcion.ToUpper().Contains(filtro.ToUpper()) || x.Categoria.Descripcion.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                listaFiltrada = listaArticulo;
            }

            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            ocultarColumnas();
        }
        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cboCampo.SelectedItem.ToString();
            if (opcion == "Precio")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
                cboCriterio.Items.Add("Igual a");
            }
            else
            {
                cboCriterio.Items.Clear ();
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene");
            }
        }
        private void btnRestablecer_Click(object sender, EventArgs e)
        {
            cargar();
        }
        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAltaArticulo altaArticulo = new frmAltaArticulo();
            altaArticulo.ShowDialog();
            cargar();
        }
        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            if (dgvArticulos.CurrentCell is null)
            {
                MessageBox.Show("Debe Seleccionar un Artículo");
            }
            else
            {
                seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

                frmAltaArticulo modificar = new frmAltaArticulo(seleccionado);
                modificar.ShowDialog();
                cargar();
            }
        }
        private void eliminarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ArticuloNegocio articulo = new ArticuloNegocio();
            try
            {
                DialogResult respuesta = MessageBox.Show("Eliminar Articulo???", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    articulo.Delete(seleccionado);
                    MessageBox.Show("Eliminado exitosamente");
                    cargar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            cargar();
        }
        private void salirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss");
            lblFecha.Text = DateTime.Now.ToLongDateString();
        }
        //Probar Ajuste de Tamaño de Form con tamaño de grilla
        private void ajustarAltoGrilla()
        {
            //PROBAR
            //-----------------
            int AltoGridIni = dgvArticulos.Height;
            int AltoGrid = 0;
            int AltoForm = this.Height;
            int Diferencia = 0;
            AltoGrid = AltoGrid + dgvArticulos.ColumnHeadersHeight;

            for (int i = 0; i <= dgvArticulos.Rows.Count - 1; i++)
            {
                AltoGrid = AltoGrid + dgvArticulos.Rows[i].Height;
            }
            Diferencia = AltoGridIni - AltoGrid;

            if (Diferencia > 0)
            {
                AltoForm = AltoForm - Diferencia;
                this.Height = AltoForm;
            }
            else if (Diferencia < 0)
            {
                AltoForm = AltoForm + Diferencia;
                this.Height = AltoForm;
            }
            dgvArticulos.Height = AltoGrid;
            // -----------------
        }
        //------------------

    }
}
