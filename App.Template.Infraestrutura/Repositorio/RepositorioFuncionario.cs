using System.Data;
using App.Template.Dominio.Modelo;

namespace App.Template.Infraestrutura.Repositorio
{
	public class RepositorioFuncionario : RepositorioGenericoDapper<Funcionario>
    {
		public RepositorioFuncionario(IDbConnection conexao) : base(conexao)
        {
        }
    }
}