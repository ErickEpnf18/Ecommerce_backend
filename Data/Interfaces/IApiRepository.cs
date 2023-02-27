using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WepApiCRUD.Models;

namespace WepApiCRUD.Data.Interfaces
{
    public interface IApiRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<IEnumerable<Usuario>> GetUsuariosAsync();
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task<Usuario> GetUsuarioByNombreAsync(string nombre);
        Task<IEnumerable<Producto>> GetProductosAsync();
        Task<Producto> GetProductoByIdAsync(int id);
        Task<Producto> GetProductoByNombreAsync(string nombre);
    }
}