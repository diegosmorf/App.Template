
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Template.Core.Repositorio.Contrato;
using App.Template.Core.Repositorio.Atributos;
using App.Template.Core.Repositorio.Atributos.LogicalDelete;

namespace App.Template.Dominio.Modelo
{
	[Table("TB_Pessoa")]
    public class Pessoa : IEntidadeBase
    {
        [Key]
        [Identity]
        public int Id { get; set; }
		[Column("Nome")]
		public string Nome { get; set; }
        [Column("DtCriacao")]
        public DateTime DtCriacao { get; set; }
        [Column("DtAtualizacao")]
        public DateTime? DtAtualizacao { get; set; }
        [Status]
        [Column("StatusEntidade")]
        public eStatusEntidade StatusEntidade { get; set; }
        
    }
    
}