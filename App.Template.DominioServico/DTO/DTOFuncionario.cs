using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Template.DominioServico.DTO
{
    [Table("VW_PesoaFuncionario")]
    public class DTOFuncionario
	{
        [Column("PessoaId")]
        public int Id { get; set;}
        [Column("Nome")]
        public string Nome { get; set; }
        [Column("Cargo")]
        public string Cargo { get; set;}
	}
}