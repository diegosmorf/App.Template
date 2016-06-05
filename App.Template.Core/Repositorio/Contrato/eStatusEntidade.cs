using App.Template.Core.Repositorio.Atributos.LogicalDelete;

namespace App.Template.Core.Repositorio.Contrato
{
    public enum eStatusEntidade
    {
        Ativo = 0,

        Inativo = 1,

        [Deleted]
        Deletado = 2
    }
}