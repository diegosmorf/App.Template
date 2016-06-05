
using App.Template.Core.Repositorio.Contrato;

namespace App.Template.Infraestrutura.Repositorio
{

    public class UnitOfWork<TContexto> : IUnitOfWork<TContexto> where TContexto : IBdContexto, new()
    {
        private readonly IBdContexto _contexto;        

        public UnitOfWork(TContexto contexto)
        {
            _contexto = contexto;
        }

        public void FinalizarTransacao()
        {
            _contexto.FinalizarTransacao();
        }

        public void IniciarTransacao()
        {
            _contexto.IniciarTransacao();
        }

        public void RollbackTransacao()
        {
            _contexto.RollbackTransacao();
        }

        public void SalvarAlteracoes()
        {
            _contexto.SalvarAlteracoes();
        }
    }
}
