using AutoMapper;
using Entities.DTO.OutputDTO;
using Entities.DTO.CreateDTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrapeCityBlog
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Blog, BlogOutputDTO>();

            CreateMap<BlogForCreationDTO, Blog>()
                  .ForMember(c => c.CreationDate,
                            opt => opt.MapFrom(x => DateTime.Now)
                            );
        }
    }
}
