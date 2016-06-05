using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Template.Core.Repositorio.Contrato;
using App.Template.Core.Repositorio.Atributos;
using App.Template.Core.Repositorio.Atributos.LogicalDelete;

namespace App.Template.Dominio.Modelo
{
    [Table("TB_Funcionario")]
    public class Funcionario : IEntidadeBase
    {
		[Key]
		[Identity]
		public int Id { get; set; }
        [Column("PessoaId")]
        public int PessoaId { get; set; }
        [Column("Cargo")]
		public string Cargo { get; set; }
		[Column("DtCriacao")]
		public DateTime DtCriacao { get; set; }
		[Column("DtAtualizacao")]
		public DateTime? DtAtualizacao { get; set; }
		[Status]
		[Column("StatusEntidade")]
		public eStatusEntidade StatusEntidade { get; set; }
    }
}
