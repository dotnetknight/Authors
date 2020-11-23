using Authors.CQRS.Interfaces;
using Authors.Models.Commands;
using System.Threading.Tasks;

namespace Authors.API.Handlers.Command
{
    public class AddGambitCommandHandler : ICommandHandlerAsync<AddGambitCommand, Resp>
    {
        public Task<Resp> HandleAsync(AddGambitCommand command)
        {
            return Task.FromResult(new Resp
            {
                Desc = "GAMBIT",
                Id = 22
            });
        }
    }

    public class Resp
    {
        public int Id { get; set; }
        public string Desc { get; set; }
    }
}
