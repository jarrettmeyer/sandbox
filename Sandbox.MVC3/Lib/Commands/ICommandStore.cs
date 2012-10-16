namespace Sandbox.MVC.Lib.Commands
{
    public interface ICommandStore
    {
        TCommand GetCommand<TCommand>() where TCommand : ICommand;
    }
}