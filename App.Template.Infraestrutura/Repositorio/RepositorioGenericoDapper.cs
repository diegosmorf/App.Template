using Dapper;
using App.Template.Infraestrutura.Repositorio.DbContext.SqlGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Template.Core.Repositorio.Contrato;
using App.Template.Core.Repositorio.SqlGenerator;
using App.Template.Core.Repositorio.Extensao;

namespace App.Template.Infraestrutura.Repositorio
{
    public class RepositorioGenericoDapper<TEntity> : IRepositorioGenerico<TEntity> where TEntity : class, IEntidadeBase
    {
        public RepositorioGenericoDapper(IDbConnection conexao)
        {
            Conexao = conexao;
            GeradorSQL = new SqlGenerator<TEntity>(ESqlConnector.MSSQL);
        }

        public RepositorioGenericoDapper(IDbConnection conexao, ESqlConnector sqlConnector)
        {
            Conexao = conexao;
            GeradorSQL = new SqlGenerator<TEntity>(sqlConnector);
        }

        public RepositorioGenericoDapper(IDbConnection conexao, ISqlGenerator<TEntity> sqlGenerator)
        {
            Conexao = conexao;
            GeradorSQL = sqlGenerator;
        }

        public IDbConnection Conexao { get; }

        public ISqlGenerator<TEntity> GeradorSQL { get; }

        public virtual TEntity Buscar(Expression<Func<TEntity, bool>> expressao)
        {
            var resultadoQuery = GeradorSQL.GetSelectFirst(expressao);
            return BuscarLista(resultadoQuery).FirstOrDefault();
        }
        
        public virtual IEnumerable<TEntity> BuscarLista()
        {
            var resultadoQuery = GeradorSQL.GetSelectAll(null);
            return BuscarLista(resultadoQuery);
        }

        public virtual IEnumerable<TEntity> BuscarLista(Expression<Func<TEntity, bool>> expressao)
        {
            var resultadoQuery = GeradorSQL.GetSelectAll(expressao);
            return Conexao.Query<TEntity>(resultadoQuery.Sql, resultadoQuery.Param);
        }

        public virtual IEnumerable<TEntity> BuscarLista(SqlQuery sqlQuery)
        {
            return Conexao.Query<TEntity>(sqlQuery.Sql, sqlQuery.Param);
        }
                
        public virtual bool Inserir(TEntity instancia)
        {
            bool adicionado;
            instancia.DtCriacao = DateTime.UtcNow;

            var resultadoQuery = GeradorSQL.GetInsert(instancia);

            if (GeradorSQL.IsIdentity)
            {
                var novoId = Conexao.Query<long>(resultadoQuery.Sql, resultadoQuery.Param).FirstOrDefault();
                adicionado = novoId > 0;

                if (adicionado)
                {
                    var newParsedId = Convert.ChangeType(novoId, GeradorSQL.IdentityProperty.PropertyInfo.PropertyType);
                    GeradorSQL.IdentityProperty.PropertyInfo.SetValue(instancia, newParsedId);
                }
            }
            else
            {
                adicionado = Conexao.Execute(resultadoQuery.Sql, instancia) > 0;
            }

            return adicionado;
        }
        
        public virtual bool Excluir(TEntity instancia)
        {
            var resultadoQuery = GeradorSQL.GetDelete(instancia);
            var deleted = Conexao.Execute(resultadoQuery.Sql, resultadoQuery.Param) > 0;
            return deleted;
        }
        
        public virtual bool Atualizar(TEntity instancia)
        {
            instancia.DtAtualizacao = DateTime.UtcNow;
            var query = GeradorSQL.GetUpdate(instancia);
            var updated = Conexao.Execute(query.Sql, instancia) > 0;
            return updated;
        }
        
        public bool Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            this.Conexao.Dispose();            
        }
    }
}