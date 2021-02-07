using System.ComponentModel.DataAnnotations;

namespace teste.net.Models
{
    public class Estados
    {
        [Key]
        public int IdEstado { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [MinLength(4, ErrorMessage = "Este campo deve conter no mínimo 4 caracteres.")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter no máximo 60 caracteres.")]
        public string NomeEstado {get; set;}

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [MinLength(2, ErrorMessage = "Este campo deve conter no mínimo 2 caracteres.")]
        [MaxLength(2, ErrorMessage = "Este campo deve conter no máximo 2 caracteres.")]
        public string UF {get; set;}

    }
}