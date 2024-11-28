using Demo.Business.DTOs.Opportunity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.DTOs.Organization;


public class OrganizationDto
{
    public string OrganizationId { get; set; }
    public string? Mission { get; set; }
    public string? Website { get; set; }
    public string? MainBranch { get; set; }
    public string? BankName { get; set; }
    public string? BankAccount { get; set; }
}

