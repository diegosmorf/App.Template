namespace App.Template.Core.Repositorio.Atributos.Joins
{
    public class InnerJoinAttribute : JoinAttributeBase
    {       
        public InnerJoinAttribute(string tableName, string key, string externalKey)
            : base(tableName, key, externalKey)
        {
        }
    }
}
