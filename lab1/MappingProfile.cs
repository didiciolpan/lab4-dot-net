using System;
using AutoMapper;
using lab1.Models;
using lab2.Models;
using lab2.ViewModels;
using lab2.ViewModels.AssignedTasks;

namespace lab2
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tasks, TasksViewModel>().ReverseMap();
            CreateMap<Comment, CommentViewModel>();
            CreateMap<ApplicationUser, ApplicationUserViewModel>().ReverseMap();
            CreateMap<Tasks, TasksWithCommentsViewModel>();
            CreateMap<AssignedTasks, AssignedTasksForUserResponse>().ReverseMap();
            //CreateMap<AssignedTasks, NewAssignedTask>().ReverseMap();


        }
    }
}
