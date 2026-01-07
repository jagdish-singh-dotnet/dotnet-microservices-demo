using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Contracts.Responses
{
    public class AuthResult   
    {
        public bool IsSuccess { get; set; }
        public string? Token { get; set; }
        public string? Error { get; set; }
    }
}
