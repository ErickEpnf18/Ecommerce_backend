using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WepApiCRUD.Data.Interfaces;
using WepApiCRUD.Models;

namespace WepApiCRUD.Data
{
    public class ApiRepository: IApiRepository
    {
        private readonly DataContext _context;
        public ApiRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        
        public async Task<IEnumerable<Producto>> GetProductosAsync()
        {
            var productos = await _context.Productos.ToListAsync();
            return productos;
        }
        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);
            return producto;
        }

        public async Task<Producto> GetProductoByNombreAsync(string nombre)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync(p => p.Nombre == nombre);
            return producto;
        }


        public async Task<IEnumerable<Usuario>> GetUsuariosAsync()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return usuarios;
        }
        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            return usuario;
        }

        public async Task<Usuario> GetUsuarioByNombreAsync(string nombre)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Nombre == nombre);
            return usuario;
        }

    }
}