using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.DTOs.Application;
public class CreateApplicationDto
{
    public int? OpportunityId { get; set; }
    public string? VolunteerId { get; set; }
    public string? OrganizationId { get; set; }
}