using App.Template.Core.Repositorio.SqlGenerator;
using System.Data;

namespace App.Template.Core.Repositorio.Contrato
{
    public interface IRepositorioGenerico<TEntidade> :
        IRepositorioServicoLeitura<TEntidade>,
        IRepositorioServicoPersistencia<TEntidade> where TEntidade : class
    {
    }
}