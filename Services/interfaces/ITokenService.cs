using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WepApiCRUD.Models;

namespace WepApiCRUD.Services.interfaces
{
    public interface ITokenService
    {
        string CreateToken(Usuario usuario);
    }
}