using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.DTOs.Volunteer;

public class RegisterVolunteerDto : RegisterUserDto
{
    public string Address { get; set; }

    public string? Bio { get; set; }

    public int Age { get; set; } 
}

