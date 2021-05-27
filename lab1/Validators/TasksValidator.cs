using System;
using System.Linq;
using FluentValidation;
using lab1.Data;
using lab2.ViewModels;

namespace lab2.Validators
{
    public class TasksValidator : AbstractValidator<TasksViewModel> 
    {
            private readonly ApplicationDbContext _context;

            public TasksValidator(ApplicationDbContext context)
            {
                _context = context;
                RuleFor(x => x.Title).MinimumLength(10);
                RuleFor(x => x.Status).NotEmpty();

                RuleFor(x => x.Title).Custom((prop, validationContext) =>
                {
                    var instance = validationContext.InstanceToValidate;
                    int commentsForTypeCount = _context.Comments.Where(e => e.Tasks.Title.Equals(instance.Title)).Count();
                    if (commentsForTypeCount > 5)
                    {
                        validationContext.AddFailure($"Cannot add a task with title {instance.Title} " +
                            $"because that Title has {commentsForTypeCount} comments");
                    }
                });

                RuleFor(x => x.DateTimeAdded).GreaterThanOrEqualTo(DateTime.Now);
                RuleFor(x => x.DateTimeClosedAt).LessThanOrEqualTo(DateTime.Now);
             // RuleFor(x => x.Status).InclusiveBetween(??);
        }

    }








    }

