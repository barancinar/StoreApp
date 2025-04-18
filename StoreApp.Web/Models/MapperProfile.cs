using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using StoreApp.Data.Concrete;

namespace StoreApp.Web.Models;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // Create mappings between your entities and view models here
        CreateMap<Product, ProductViewModel>();
    }

}
