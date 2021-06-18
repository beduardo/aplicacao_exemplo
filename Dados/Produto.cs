using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aplicacao_exemplo.Dados
{
    [Table("Produtos")]
    public class Produto
    {
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string Descricao { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Material { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string Preco { get; set; }
        
        [Required]
        [MaxLength(40)]
        public string Color { get; set; }
    }
}