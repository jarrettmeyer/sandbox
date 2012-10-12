namespace Sandbox.MVC3.Lib.Commands
{
    public interface ICommand
    {
        void ExecuteCommand();
    }

    public interface ICommand<out TResult> : ICommand
    {
        TResult Result { get; }
    }
}