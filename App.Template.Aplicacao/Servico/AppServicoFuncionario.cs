
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using App.Template.DominioServico.DTO;
using App.Template.Application.Contrato;
using App.Template.DominioServico.Contrato;

namespace App.Template.Application.Servico
{
	public class AppServicoFuncionario : IAppServicoFuncionario 
    {
        private readonly IDominioServicoFuncionario servicoDominioFuncionario;
        public AppServicoFuncionario(IDominioServicoFuncionario servicoDominioFuncionario)
        {
            this.servicoDominioFuncionario = servicoDominioFuncionario;
        }
		public IEnumerable<DTOFuncionario> Buscar(Expression<Func<DTOFuncionario, bool>> expressao)
        {
            return servicoDominioFuncionario.BuscarLista(expressao);
        }
        public bool Salvar(DTOFuncionario instancia)
        {
            return servicoDominioFuncionario.Salvar(instancia);
        }        
        public bool Excluir(int id)
        {
            return servicoDominioFuncionario.Excluir(id);
        }

        public void Dispose()
        {
            servicoDominioFuncionario.Dispose();
        }
    }
}