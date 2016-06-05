using App.Template.Core.Repositorio.Contrato;
using System.Data;

namespace App.Template.Infraestrutura.Repositorio.DbContext
{
    public class BdContextoDapper : IBdContexto
    {
        protected BdContextoDapper(IDbConnection connection)
        {
            _conexao = connection;
        }

        protected readonly IDbConnection _conexao;
        protected IDbTransaction _transacao;

        public virtual IDbConnection Conexao
        {
            get
            {
                if (_conexao.State != ConnectionState.Open && _conexao.State != ConnectionState.Connecting)
                    _conexao.Open();

                return _conexao;
            }
        }

        public string ConnectionString
        {
            get
            {
                return _conexao.ConnectionString;
            }
        }

        public void Dispose()
        {
            if (_conexao != null && _conexao.State != ConnectionState.Closed)
                _conexao.Close();
        }

        public void IniciarTransacao()
        {
            _transacao = _conexao.BeginTransaction();
        }

        public void FinalizarTransacao()
        {
            if (_transacao != null)
                _transacao.Commit();
        }

        public void RollbackTransacao()
        {
            if (_transacao != null)
                _transacao.Rollback();
        }
        public void SalvarAlteracoes()
        {
            this.FinalizarTransacao();
        }
    }
}