namespace Sandbox.MVC3.Lib.Queries
{
    public abstract class AbstractQuery<TOut> : IQuery<TOut>
    {
        public abstract TOut ExecuteQuery();        

        object IQuery.ExecuteQuery()
        {
            return ExecuteQuery();
        }
    }
}