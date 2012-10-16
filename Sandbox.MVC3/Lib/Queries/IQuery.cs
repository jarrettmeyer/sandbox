namespace Sandbox.MVC.Lib.Queries
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