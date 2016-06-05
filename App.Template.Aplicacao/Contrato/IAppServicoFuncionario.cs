
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using App.Template.DominioServico.DTO;

namespace App.Template.Application.Contrato
{
	public interface IAppServicoFuncionario : IDisposable
    {
		IEnumerable<DTOFuncionario> Buscar(Expression<Func<DTOFuncionario, bool>> expressao);        
		bool Salvar(DTOFuncionario instancia);        
        bool Excluir(int id);
    }
}