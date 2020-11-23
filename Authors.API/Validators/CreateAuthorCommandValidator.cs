using Authors.Models.Commands;
using FluentValidation;

namespace Authors.API.Validators
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty();
            RuleFor(c => c.LastName).NotEmpty();
            RuleFor(c => c.MainCategory).NotEmpty();
        }
    }
}
