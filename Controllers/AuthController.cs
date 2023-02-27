using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WepApiCRUD.Data.Interfaces;
using WepApiCRUD.Dtos;
using WepApiCRUD.Models;
using WepApiCRUD.Services.interfaces;

namespace WepApiCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AuthController(IAuthRepository repo, IMapper mapper, ITokenService tokenService)
        {
            _repo = repo;
            _mapper = mapper; 
            _tokenService = tokenService;
        }
        [AllowAnonymous]

        [HttpPost("register")]
        public async Task<IActionResult> Register(UsuarioRegisterDto usuarioDto)
        {
            usuarioDto.Correo = usuarioDto.Correo.ToLower(); //all to lowercase
            if(await _repo.ExisteUsuario(usuarioDto.Correo))
            {
                return BadRequest("Usuario con este correo ya existe");
            }
            var usuarioNuevo = _mapper.Map<Usuario>(usuarioDto);
            var usuarioCreado = await _repo.Registrar(usuarioNuevo, usuarioDto.Password);
            var usuarioCreadoDto = _mapper.Map<UsuarioListDto>(usuarioCreado); 

            return Ok(usuarioCreadoDto); 
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UsuarioLoginDto usuarioLoginDto)
        {
            var usuarioFromRepo = await _repo.Login(usuarioLoginDto.Correo, usuarioLoginDto.Password);
            if(usuarioFromRepo == null)
                return Unauthorized();
            
            var usuario = _mapper.Map<UsuarioListDto>(usuarioFromRepo);

            var token = _tokenService.CreateToken(usuarioFromRepo);

            return Ok(new {
                token= token,
                usuario= usuario
            });


        }



        
    }
}