using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WepApiCRUD.Data.Interfaces;
using WepApiCRUD.Dtos;
using WepApiCRUD.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace WepApiCRUD.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IApiRepository _repo;
        private readonly IMapper _mapper;


        public ProductosController(IApiRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ProductosController"/>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var productos = await _repo.GetProductosAsync();
            var productoDto = _mapper.Map<IEnumerable<ProductoToListDto>>(productos);
            return Ok(productoDto);
        }
        /// <summary>
        /// Obtener un producto mediante el id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var producto = await _repo.GetProductoByIdAsync(id);
            // var productoDto = new ProductoToListDto();
            var productoDto = _mapper.Map<ProductoToListDto>(producto);

            // productoDto.Id = producto.Id;
            // productoDto.Nombre = producto.Nombre;
            // productoDto.Descripcion = producto.Descripcion;

            if (producto == null)
                return NotFound("Producto no encontrado con el id");

            return Ok(productoDto);
        }

        [HttpGet("nombre/{nombre}")]
        public async Task<IActionResult> Get(string nombre)
        {
            var producto = await _repo.GetProductoByNombreAsync(nombre);
            if (producto == null)
                return NotFound("Producto no encontrado con el nombre");

            var productoDto = _mapper.Map<ProductoToListDto>(producto);
            return Ok(productoDto);
        }
        /// <summary> Ingresar un nuevo producto </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(ProductoCreateDto productoDto)
        {
            // var productoToCreate = new Producto();
            // productoToCreate.Nombre = productoDto.Nombre;
            // productoToCreate.Descripcion = productoDto.Descripcion;
            // productoToCreate.Precio = productoDto.Precio;
            var productoToCreate = _mapper.Map<Producto>(productoDto);

            _repo.Add(productoToCreate);
            if (await _repo.SaveAll())
                return Ok(productoToCreate);
            return BadRequest();

        }

        /// <summary>
        /// Actualizar un producto mediante el ingresado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productoDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProductoUpdateDto productoDto)
        {
            if(id != productoDto.Id)
                return BadRequest("Los ids no coinciden");
            
            var productoToUpdate = await _repo.GetProductoByIdAsync(productoDto.Id);
            if (productoToUpdate == null)
                return BadRequest("No existe tal producto en la base de datos");

            // productoToUpdate.Descripcion = productoDto.Descripcion;
            // productoToUpdate.Precio = productoDto.Precio;
            _mapper.Map(productoDto, productoToUpdate);

            if (!await _repo.SaveAll())
                return BadRequest("No se pudo actualizar el producto mala insercion");
            return Ok(productoToUpdate);
        }

        /// <summary>
        /// Eliminar un producto mediante el id
        /// </summary>
        /// <remarks>
        /// remark goes here.
        /// </remarks>
        /// <param name="id">Este es el id</param>
        /// <returns></returns>
        /// <response code="200">Producto borrado exitosamentes</response>
        /// <response code="400">No existe producto en la base de datos</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [ProducesResponseType(type: typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        // [SwaggerResponse(statusCode: StatusCodes.Status400BadRequest, type: null, description: "wrong email or password")]



        // [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Producto))]

        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _repo.GetProductoByIdAsync(id);
            if (producto == null)

                return BadRequest(new
                {
                    message = "No existe tal producto en la base de datos",
                    errors = producto
                });

            _repo.Delete(producto);
            if (!await _repo.SaveAll())
                // <summary> Cunado existe un fallo en el producto a eliminar </summary>
                return BadRequest("No se pudo eliminar el producto");
            return Ok("Producto Borrado");
        }








    }
}