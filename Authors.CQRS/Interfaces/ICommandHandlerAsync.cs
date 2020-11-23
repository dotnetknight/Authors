using System.Threading.Tasks;

namespace Authors.CQRS.Interfaces
{
    public interface ICommandHandlerAsync<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }

    public interface ICommandHandlerAsync<TCommand, TResult> where TCommand : ICommand
    {
        Task<TResult> HandleAsync(TCommand command);
    }
}
