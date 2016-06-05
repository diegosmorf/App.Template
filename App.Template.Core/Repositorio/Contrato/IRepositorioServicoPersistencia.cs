using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.Template.Core.Repositorio.Contrato
{
    public interface IRepositorioServicoPersistencia<TEntidade> : IDisposable where TEntidade : class
    {
        bool Inserir(TEntidade instancia);
        bool Excluir(int id);
        bool Atualizar(TEntidade instancia);
    }
}