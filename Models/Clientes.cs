using System;
using System.ComponentModel.DataAnnotations;

namespace teste.net.Models
{
    public class Clientes
    {
        [Key]
        public int IdCliente { get; set; }
        
        [Required(ErrorMessage = "Nome Completo Obrigatório!")]
        [MinLength(4, ErrorMessage = "Este campo deve conter no mínimo 4 caracteres.")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter no máximo 60 caracteres.")]
        public string NomeCompleto { get; set; }
        public string Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        
        [Required(ErrorMessage = "Idade é Obrigatória!")]
        [Range(13, int.MaxValue, ErrorMessage = "A idade deve ser maior que 13 anos")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "Cidade é Obrigatória!")]
        [Range(1, int.MaxValue, ErrorMessage = "Cidade inválida")]
        public int IdCidade {get; set; }
        public Cidades Cidade {get; set;}

        [Required(ErrorMessage = "Estado é Obrigatóri!")]
        [Range(1, int.MaxValue, ErrorMessage = "Cidade inválida")]
        public int IdEstado {get; set; }
        public Estados Estado {get; set;}
    }
}