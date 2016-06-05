using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.Template.Core.Repositorio.Contrato
{
    public interface IRepositorioServicoPersistenciaAssincrono<TEntidade> : IDisposable where TEntidade : class
    {
        Task<bool> InserirAssincrono(TEntidade instancia);
        Task<bool> ExcluirAssincrono(TEntidade instancia);
        Task<bool> AtualizarAssincrono(TEntidade instancia);
    }
}