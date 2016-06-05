
namespace App.Template.Core.Repositorio.Contrato
{
    public interface IUnitOfWork<TContext> where TContext : IBdContexto, new()
    {
        void IniciarTransacao();
        void FinalizarTransacao();
        void RollbackTransacao();
        void SalvarAlteracoes();
    }
}