using AutoMapper;
using Birder2.Models;
using Birder2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.Data
{
    public class Birder2MappingProfile : Profile
    {
        public Birder2MappingProfile()
        {
            CreateMap<Observation, ObservationsIndexViewModel>();
        }
    }
}
