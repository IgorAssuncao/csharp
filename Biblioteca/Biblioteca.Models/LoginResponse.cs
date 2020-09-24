using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Models
{
    public class LoginResponse
    {
        public bool Status { get; set; }
        public string Token { get; set; }
        public Guid Id { get; set; }
    }
}
