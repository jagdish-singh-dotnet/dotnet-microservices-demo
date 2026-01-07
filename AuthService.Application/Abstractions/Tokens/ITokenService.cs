using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Abstractions.Tokens
{
    public interface ITokenService
    {
        string GenerateToken(Guid userId, string email);
    }
}
