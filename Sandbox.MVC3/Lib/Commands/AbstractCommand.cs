namespace Sandbox.MVC3.Lib.Commands
{
    public abstract class AbstractCommand : ICommand
    {
        public abstract void ExecuteCommand();    
    }

    public abstract class AbstractCommand<TResult> : AbstractCommand, ICommand<TResult>
    {
        public TResult Result { get; protected set; }
    }
}