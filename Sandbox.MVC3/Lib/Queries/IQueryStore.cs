namespace Sandbox.MVC3.Lib.Queries
{
    public interface IQueryStore
    {
        TQuery GetQuery<TQuery>() where TQuery : IQuery;
    }
}