using FluentValidation;
using ToDoer.API.Infrastructure.Localizations;
using ToDoer.Application.Subtasks.Requests;

namespace ToDoer.API.Infrastructure.Validators
{
    public class SubtaskCreateValidator :  AbstractValidator<SubtaskCreateModel>
    {
        public SubtaskCreateValidator()
        {
            RuleFor(x => x.Title).
                NotEmpty().WithMessage(ErrorMessages.SubtaskTitle).
                MaximumLength(50).WithMessage(ErrorMessages.ToDoTitleLength);
        }
    }

    public class SubtaskUpdateValidator : AbstractValidator<SubtaskRequestPutModel>
    {
        public SubtaskUpdateValidator()
        {
            RuleFor(x => x.Title).
                NotEmpty().WithMessage(ErrorMessages.SubtaskTitle).
                MaximumLength(50).WithMessage(ErrorMessages.ToDoTitleLength);
        }
    }

    public class SubtaskRequestValidator : AbstractValidator<SubtaskRequestModel>
    {
        public SubtaskRequestValidator()
        {
            RuleFor(x => x.Title).
                NotEmpty().WithMessage(ErrorMessages.SubtaskTitle).
                MaximumLength(50).WithMessage(ErrorMessages.ToDoTitleLength);
        }
    }
}
