using System.Threading.Tasks;

namespace Authors.CQRS.Interfaces
{
    public interface ICommandBusAsync
    {
        Task ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand;
        Task<TResult> ExecuteAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand;
    }
}
