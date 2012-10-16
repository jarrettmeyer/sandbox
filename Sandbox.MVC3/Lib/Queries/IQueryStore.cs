namespace Sandbox.MVC.Lib.Queries
{
    public interface IQueryStore
    {
        TQuery GetQuery<TQuery>() where TQuery : IQuery;
    }
}