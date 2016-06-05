
using App.Template.Core.Repositorio.Contrato;

namespace App.Template.Infraestrutura.Repositorio
{

	public class GerenciadorContexto<TContexto> : IGerenciadorContexto<TContexto> where TContexto : IBdContexto, new()
	{
		private readonly IBdContexto _dbContexto;

		public GerenciadorContexto(IBdContexto dbContexto)
		{
            _dbContexto = dbContexto;
		}

        public IBdContexto Contexto()
        {
            return _dbContexto;
        }        
	}
}
