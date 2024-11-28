using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.DTOs.Organization;

public class RegisterOrganizationDto : RegisterUserDto
{
    public string? Mission { get; set; }

    [Unicode(false)]
    public string? Website { get; set; }

    public string? MainBranch { get; set; }

    [StringLength(128)]
    public string? BankName { get; set; }

    [Unicode(false)]
    public string? BankAccount { get; set; }
}
