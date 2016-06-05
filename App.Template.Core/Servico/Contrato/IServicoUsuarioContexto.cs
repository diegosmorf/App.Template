using App.Template.Dominio.Modelo;

namespace App.Template.Dominio.Contrato.Servico
{
    public interface IServicoUsuarioContexto : IServicoBase<UsuarioContexto>
    {
        UsuarioContexto Autenticar(string login, string senha);
    }
}
