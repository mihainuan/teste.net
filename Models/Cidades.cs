using System.ComponentModel.DataAnnotations;

namespace teste.net.Models
{
    public class Cidades
    {
        [Key]
        public int IdCidade { get; set; }

        [Required(ErrorMessage = "Nome  Obrigatório!")]
        [MinLength(4, ErrorMessage = "Este campo deve conter no mínimo 4 caracteres.")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter no máximo 60 caracteres.")]
        public string NomeCidade {get; set;}
      
        [Required(ErrorMessage = "Estado é Obrigatório!")]
        [Range(1, int.MaxValue, ErrorMessage = "Estado inválido")]
        public int IdEstado {get; set; }
        public Estados Estado {get; set;}

    }
}