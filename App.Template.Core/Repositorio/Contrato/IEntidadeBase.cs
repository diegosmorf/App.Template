using System;

namespace App.Template.Core.Repositorio.Contrato
{	
	public interface IEntidadeBase
	{
		int Id {get;set;}
		DateTime DtCriacao {get;set;}
	  	DateTime? DtAtualizacao {get;set;}
        eStatusEntidade StatusEntidade { get; set; }
    }
}
