using System;
using AutoMapper;
using lab1.Models;
using lab2.Models;
using lab2.ViewModels;

namespace lab2
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tasks, TasksViewModel>().ReverseMap();
            CreateMap<Comment, CommentViewModel>();
            CreateMap<Tasks, TasksWithCommentsViewModel>();

        }
    }
}
