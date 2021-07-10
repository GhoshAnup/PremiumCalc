using AutoMapper;
using Premium.Models;
using Premium.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Premium
{
    public class ObjectMapper : Profile
    {
        public ObjectMapper()
        {
            CreateMap<PremiumViewModel, Premiums>();
        }
    }
}

