using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Dominio
{
    public class Imagen
    {
        [Required]
        public int Id { get; set; } 
        [Required]
        public int IdArticulo { get; set; }

        [RegularExpression(@"\bhttps?://\S+\.(png|jpe?g|gif|bmp|webp)\b", ErrorMessage = "No es una URL de imagen válida.")]
        [StringLength(1000, ErrorMessage = "Ingreso invalido")]
        [Required]
        public string ImagenUrl { get; set; }
        public override string ToString()
        {
            return ImagenUrl;
        }
    }
}
