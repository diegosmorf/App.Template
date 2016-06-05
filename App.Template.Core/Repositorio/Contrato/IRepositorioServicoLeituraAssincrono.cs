using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Template.Core.Repositorio.SqlGenerator;

namespace App.Template.Core.Repositorio.Contrato
{    public interface IRepositorioServicoLeituraAssincrono<TEntidade> : IDisposable where TEntidade : class
    {
        Task<IEnumerable<TEntidade>> BuscarListaAssincrono();
        Task<IEnumerable<TEntidade>> BuscarListaAssincrono(Expression<Func<TEntidade, bool>> expressao);
        Task<IEnumerable<TEntidade>> BuscarListaAssincrono(SqlQuery sqlQuery);
        Task<TEntidade> BuscarAssincrono(Expression<Func<TEntidade, bool>> expressao);             
    }
}