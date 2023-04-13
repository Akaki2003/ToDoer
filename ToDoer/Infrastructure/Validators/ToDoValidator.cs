using FluentValidation;
using ToDoer.API.Infrastructure.Localizations;
using ToDoer.Application.ToDos.Requests;

namespace ToDoer.API.Infrastructure.Validators
{
    public class ToDoRequestValidator : AbstractValidator<ToDoRequestModel>
    {
        public ToDoRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(ErrorMessages.ToDoTitle)
                .MaximumLength(100).WithMessage(ErrorMessages.ToDoTitleLength);
        }
    }

    public class ToDoCreateValidator : AbstractValidator<ToDoCreateModel>
    {
        public ToDoCreateValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(ErrorMessages.ToDoTitle)
                .MaximumLength(100).WithMessage(ErrorMessages.ToDoTitleLength);

            RuleForEach(toDo => toDo.Subtasks)
                .Must((toDo, subtask) => subtask.Title.Length <= 100).
                WithMessage(ErrorMessages.SubtaskTitleLength);
        }
    }

    public class ToDoPutValidator : AbstractValidator<ToDoRequestPutModel>
    {
        public ToDoPutValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(ErrorMessages.ToDoTitle)
                .MaximumLength(100).WithMessage(ErrorMessages.ToDoTitleLength);


            RuleFor(x=>x.Status)
                .NotEmpty().WithMessage(ErrorMessages.ToDoStatus)
                .IsInEnum().WithMessage(ErrorMessages.ToDoStatusEnum);
        }
    }

}
