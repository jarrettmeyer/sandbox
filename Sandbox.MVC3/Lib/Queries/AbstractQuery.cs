namespace Sandbox.MVC.Lib.Queries
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