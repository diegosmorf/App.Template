using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.Template.Core.Repositorio.Contrato
{
    public interface IRepositorioServicoLeitura<TEntidade>: IDisposable where TEntidade : class
    {
        TEntidade Buscar(Expression<Func<TEntidade, bool>> expressao);        
        IEnumerable<TEntidade> BuscarLista();
        IEnumerable<TEntidade> BuscarLista(Expression<Func<TEntidade, bool>> expressao);
        
    }
}