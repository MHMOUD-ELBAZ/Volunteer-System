using Demo.Business.DTOs.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.DTOs.Volunteer;

public class VolunteerWithApplicationsDto
{
    public string VolunteerId { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Bio { get; set; }

    public int Age { get; set; }

    public double? Rating { get; set; } = 0;

    public ICollection<ApplicationDto> Applications { get; set; }
}
