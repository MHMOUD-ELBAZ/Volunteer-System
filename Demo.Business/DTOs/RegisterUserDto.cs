using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.DTOs;

public class RegisterUserDto
{
    public string Email { get; set; }

    public string Name { get; set; }

    public string Password { get; set; }

    public string Phone { get; set; }
    public IFormFile? Photo { get; set; }
}
