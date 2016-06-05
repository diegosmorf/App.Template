
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using App.Template.DominioServico.DTO;
using App.Template.DominioServico.Contrato;
using App.Template.Core.Repositorio.Contrato;
using App.Template.Dominio.Modelo;

namespace App.Template.Application.Servico
{
	public class DominioServicoFuncionario : IDominioServicoFuncionario
    {
        private readonly IRepositorioGenerico<Funcionario> repositorioFuncionario;
        private readonly IRepositorioGenerico<Pessoa> repositorioPessoa;
        private readonly IRepositorioServicoLeitura<DTOFuncionario> repositorioDTOFuncionario;
        public DominioServicoFuncionario(
            IRepositorioGenerico<Funcionario> repositorioFuncionario,
            IRepositorioGenerico<Pessoa> repositorioPessoa,
            IRepositorioServicoLeitura<DTOFuncionario> repositorioDTOFuncionario
            )
        {
            this.repositorioFuncionario = repositorioFuncionario;
            this.repositorioPessoa = repositorioPessoa;
            this.repositorioDTOFuncionario = repositorioDTOFuncionario;
        }
		public IEnumerable<DTOFuncionario> BuscarLista(Expression<Func<DTOFuncionario, bool>> expressao)
        {
            return repositorioDTOFuncionario.BuscarLista(expressao);
        }
        public bool Salvar(DTOFuncionario instancia)
        {
            var pessoa = repositorioPessoa.Buscar(x => x.Id == instancia.Id);

            if(pessoa == null )
            {
                pessoa = new Pessoa() { Nome = instancia.Nome };
                repositorioPessoa.Inserir(pessoa);
                var funcionario = new Funcionario() {PessoaId = pessoa.Id,  Cargo = instancia.Cargo };
                repositorioFuncionario.Inserir(funcionario);                
            }
            else
            {
                pessoa = new Pessoa() { Nome = instancia.Nome };
                repositorioPessoa.Atualizar(pessoa);
                var funcionario = new Funcionario() { Cargo = instancia.Cargo };
                repositorioFuncionario.Atualizar(funcionario);
            }

            return true;
        }
        
        public bool Excluir(int id)
        {
            var funcionario = repositorioFuncionario.Buscar(x => x.PessoaId == id);
            repositorioFuncionario.Excluir(funcionario.Id);
            return repositorioFuncionario.Excluir(id);
        }

        public void Dispose()
        {
            repositorioPessoa.Dispose();
            repositorioFuncionario.Dispose();
            repositorioDTOFuncionario.Dispose();
        }
    }
}