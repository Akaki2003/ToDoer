using Microsoft.AspNetCore.JsonPatch;
using Swashbuckle.AspNetCore.Filters;
using ToDoer.Application.PatchModel;
using ToDoer.Application.Subtasks.Requests;
using ToDoer.Application.Subtasks.Responses;
using ToDoer.Application.ToDos.Requests;
using ToDoer.Application.ToDos.Responses;
using Status = ToDoer.Application.ToDos.Responses.Status;

namespace ToDoer.API.ModelExamples
{
    public class SubtasksCreate : IMultipleExamplesProvider<SubtaskCreateModel>
    {
        public IEnumerable<SwaggerExample<SubtaskCreateModel>> GetExamples()
        {
            yield return SwaggerExample.Create("example 1", new SubtaskCreateModel
            {
                Title = "Buy Bread",
            });

            yield return SwaggerExample.Create("example 2", new SubtaskCreateModel
            {
                Title = "Visit dentist",
            });

        }
    }
    public class SubtaskById : IMultipleExamplesProvider<SubtaskResponseModel>
    {
        public IEnumerable<SwaggerExample<SubtaskResponseModel>> GetExamples()
        {
            yield return SwaggerExample.Create("example 1", new SubtaskResponseModel
            {
                Title = "Buy screws",
            });

            yield return SwaggerExample.Create("example 2", new SubtaskResponseModel
            {
                Title = "Visit homeowner",
            });

        }
    }
    public class SuptaskUpdate : IMultipleExamplesProvider<SubtaskRequestPutModel>
    {
        public IEnumerable<SwaggerExample<SubtaskRequestPutModel>> GetExamples()
        {
            yield return SwaggerExample.Create("example 1", new SubtaskRequestPutModel
            {
                Id = 10,
                TodoItemId = 4,
                Title = "Getting hair done",
            });

            yield return SwaggerExample.Create("example 2", new SubtaskRequestPutModel
            {
                Id = 11,
                TodoItemId = 4,
                Title = "Slice the bread",
            });

            yield return SwaggerExample.Create("example 3", new SubtaskRequestPutModel
            {
                Id = 12,
                TodoItemId = 4,
                Title = "Iron the clothes",
            });

        }
    }
    public class  TodosCreate : IMultipleExamplesProvider<ToDoCreateModel>
    {
        public IEnumerable<SwaggerExample<ToDoCreateModel>> GetExamples()
        {
            yield return SwaggerExample.Create("example 1", new ToDoCreateModel
            {
                Title = "Meet friends",
                TargetCompletionDate = DateTime.Now,
                Subtasks = new List<SubtaskCreateModel>
                {
                    new SubtaskCreateModel
                    {
                        Title = "Go  to school"
                    },
                     new SubtaskCreateModel
                    {
                        Title = "Go to University"
                    },
                }
            });

            yield return SwaggerExample.Create("example 2", new ToDoCreateModel
            {
                Title = "Run 5 miles",
                TargetCompletionDate = DateTime.Now,
                Subtasks = new List<SubtaskCreateModel>
                {
                    new SubtaskCreateModel
                    {
                        Title = "Get Sport shoes"
                    },
                     new SubtaskCreateModel
                    {
                        Title = "Stand up early"
                    },
                }
            });

        }
    }

    public class ToDoGet : IMultipleExamplesProvider<ToDoResponseModel>
    {
        public IEnumerable<SwaggerExample<ToDoResponseModel>> GetExamples()
        {
            yield return SwaggerExample.Create("example 1", new ToDoResponseModel
            {
                Id = 4,
                Title = "Studying Architecture",
                UserId = 2,
                Status = Status.Active,
                TargetCompletionDate = DateTime.Now,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Subtasks = new List<SubtaskResponseModel>
                {
                    new SubtaskResponseModel
                    {
                        TodoItemId = 4,
                        Title = "Buying stuff",
                    },
                     new SubtaskResponseModel
                    {
                        TodoItemId = 4,
                        Title = "Buying more stuff",
                    },

                }
            });

            yield return SwaggerExample.Create("example 2", new ToDoResponseModel
            {
                Id = 5,
                Title = "Studying Business",
                UserId = 6,
                Status = Status.Active,
                TargetCompletionDate = DateTime.Now,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Subtasks = new List<SubtaskResponseModel>
                {
                    new SubtaskResponseModel
                    {
                        TodoItemId = 5,
                        Title = "Meeting bussinessmans stuff",
                    },
                     new SubtaskResponseModel
                    {
                        TodoItemId = 5,
                        Title = "Getting more experience",
                    },
                       new SubtaskResponseModel
                    {
                        TodoItemId = 5,
                        Title = "Reading books about business",
                    }

                }
            });

        }
    }
    public class ToDosGet : IMultipleExamplesProvider<List<ToDoResponseModel>>
    {
        public IEnumerable<SwaggerExample<List<ToDoResponseModel>>> GetExamples()
        {
            yield return SwaggerExample.Create("example 1", new List<ToDoResponseModel>
            {
               new ToDoResponseModel
            {
                Id = 10,
                Title = "Reading books",
                UserId = 6,
                Status = Status.Active,
                TargetCompletionDate = DateTime.Now,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Subtasks = new List<SubtaskResponseModel>
                {
                    new SubtaskResponseModel
                    {
                        TodoItemId = 10,
                        Title = "buying books related to philosophy",
                    },
                     new SubtaskResponseModel
                    {
                        TodoItemId = 10,
                        Title = "buying books related to religion",
                    },
                }
            },
               new ToDoResponseModel
            {
                Id = 5,
                Title = "Having nice time",
                UserId = 6,
                Status = Status.Active,
                TargetCompletionDate = DateTime.Now,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Subtasks = new List<SubtaskResponseModel>
                {
                    new SubtaskResponseModel
                    {
                        TodoItemId = 5,
                        Title = "Taking dog for a walk",
                    },
                     new SubtaskResponseModel
                    {
                        TodoItemId = 5,
                        Title = "Playing games",
                    },
                       new SubtaskResponseModel
                    {
                        TodoItemId = 5,
                        Title = "Having fun with friends",
                    }
                }
            }

            }
              );

        }
    }
    public class TodosUpdate : IMultipleExamplesProvider<ToDoRequestPutModel>
    {
        public IEnumerable<SwaggerExample<ToDoRequestPutModel>> GetExamples()
        {
            yield return SwaggerExample.Create("example 1", new ToDoRequestPutModel
            {
                Id = 10,
                Title = "Visiting family",
                Status = (Application.ToDos.Requests.Status)Status.Done,
                TargetCompletionDate = DateTime.Now
            }); 

            yield return SwaggerExample.Create("example 2", new ToDoRequestPutModel
            {
                Id = 11,
                Title = "Visit Chiropractor",
                Status = (Application.ToDos.Requests.Status)Status.Done,
                TargetCompletionDate = DateTime.Now
            });

        }
    }

    public class ToDosPatch : IMultipleExamplesProvider<List<RequestPatchModel>>
    {
        public IEnumerable<SwaggerExample<List<RequestPatchModel>>> GetExamples()
        {
            yield return SwaggerExample.Create("example 1", new List<RequestPatchModel>
            {
                new RequestPatchModel
                {
                    path = "title",
                    op = "replace",
                    value = "do some dishes"
                }
            });

            yield return SwaggerExample.Create("example 2", new List<RequestPatchModel>
            {
                new RequestPatchModel
                {
                    path = "title",
                    op = "replace",
                    value = "get some veggies"
                }
            });

            yield return SwaggerExample.Create("example 3", new List<RequestPatchModel>
            {
                new RequestPatchModel
                {
                    path = "title",
                    op = "replace",
                    value = "take out trash"
                }
            });



        }
    }
}
