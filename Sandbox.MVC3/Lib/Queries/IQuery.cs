namespace Sandbox.MVC3.Lib.Queries
{
    public interface IQuery
    {
        object ExecuteQuery();
    }

    public interface IQuery<out TOut> : IQuery
    {
        new TOut ExecuteQuery();
    }
}