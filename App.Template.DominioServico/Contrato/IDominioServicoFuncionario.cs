
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using App.Template.DominioServico.DTO;

namespace App.Template.DominioServico.Contrato
{
	public interface IDominioServicoFuncionario : IDisposable
    {
		IEnumerable<DTOFuncionario> BuscarLista(Expression<Func<DTOFuncionario, bool>> expressao);        
		bool Salvar(DTOFuncionario instancia);        
        bool Excluir(int id);
    }
}