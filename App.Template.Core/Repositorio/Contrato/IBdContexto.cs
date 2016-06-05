using System;
using System.Data;

namespace App.Template.Core.Repositorio.Contrato
{
    public interface IBdContexto : IDisposable
    {
        string ConnectionString { get; }
        IDbConnection Conexao { get; }
        void IniciarTransacao();
        void FinalizarTransacao();
        void RollbackTransacao();
        void SalvarAlteracoes();
    }
}
