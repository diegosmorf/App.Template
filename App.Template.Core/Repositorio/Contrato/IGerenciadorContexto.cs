
namespace App.Template.Core.Repositorio.Contrato
{
    public interface IGerenciadorContexto<TContexto> where TContexto : IBdContexto, new()
    {
        IBdContexto Contexto();
    }
}
