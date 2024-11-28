using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.DTOs.Organization;

public class UpdateOrganizationDto
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string? Mission { get; set; }
    public string? Website { get; set; }
    public string? MainBranch { get; set; }
    public string? BankName { get; set; }
    public string? BankAccount { get; set; }
    public IFormFile? Photo { get; set; }
}

