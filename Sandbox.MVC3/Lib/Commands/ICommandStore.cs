namespace Sandbox.MVC3.Lib.Commands
{
    public interface ICommandStore
    {
        TCommand GetCommand<TCommand>() where TCommand : ICommand;
    }
}