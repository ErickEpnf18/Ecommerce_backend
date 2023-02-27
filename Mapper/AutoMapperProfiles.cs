using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WepApiCRUD.Dtos;
using WepApiCRUD.Models;

namespace WepApiCRUD.Mapper
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {   
            // POST OR CREATE 
            CreateMap<ProductoCreateDto, Producto>();
            
            // PUT OR UPDATE
            CreateMap<ProductoUpdateDto, Producto>();

            // GET OR LIST
            CreateMap<Producto, ProductoToListDto>();

            // Usuarios
            CreateMap<UsuarioRegisterDto, Usuario>();
            CreateMap<UsuarioLoginDto, Usuario>();
            CreateMap<Usuario, UsuarioListDto>(); // get db usuarios to push in usuariolistdto
        }
    }
}