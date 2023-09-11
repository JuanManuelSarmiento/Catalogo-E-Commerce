using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Dominio
{
    public class Articulo
    {
        public int Id { get; set; }
        [DisplayName("Código")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Este campo solo admite letras y numeros")]
        [Required]
        public string Codigo { get; set; }

        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Este campo solo admite letras y numeros")]
        [Required]
        public string Nombre { get; set; }
        [DisplayName("Descripción")]
        [StringLength(150, ErrorMessage = "Maximo 150 caracteres")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Este campo solo admite letras y numeros")]
        [Required]
        public string Descripcion { get; set; }
        public Marca Marca { get; set; }
        [DisplayName("Categoría")]
        public Categoria Categoria { get; set; }
        public Imagen Imagen { get; set; }
        public decimal Precio { get; set; }
        public override string ToString()
        {
            return Nombre;
        }
    }
}
