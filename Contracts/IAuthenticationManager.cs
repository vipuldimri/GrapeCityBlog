using Entities.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAuthenticationManager
    {
        string GetMessage();
        Task<bool> ValidateUser(LoginDTO userForAuth);
        Task<string> CreateToken();
        User getUser();
    }
}
