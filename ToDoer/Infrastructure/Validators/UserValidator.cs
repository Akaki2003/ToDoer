using FluentValidation;
using ToDoer.API.Infrastructure.Localizations;
using ToDoer.Application.Users.Requests;

namespace ToDoer.API.Infrastructure.Validators
{
    public class UserRegisterValidator : AbstractValidator<UserCreateModel>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.Username).
                NotEmpty().WithMessage(ErrorMessages.Username)
                .MaximumLength(50).WithMessage(ErrorMessages.UsernameLength);

            RuleFor(x => x.Password).
                NotEmpty().WithMessage(ErrorMessages.Password);
        }
    }
    public class UserLoginValidator : AbstractValidator<UserLoginReqest>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.Username).
                NotEmpty().WithMessage(ErrorMessages.Username)
                .MaximumLength(50).WithMessage(ErrorMessages.UsernameLength);


            RuleFor(x => x.Password).
                NotEmpty().WithMessage(ErrorMessages.Password);
        }
    }
}
