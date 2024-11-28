using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.DTOs.Application;

public class ApplicationDto
{
    public int Id { get; set; }

    public int? OpportunityId { get; set; }

    [StringLength(450)]
    public string? VolunteerId { get; set; }

    [StringLength(450)]
    public string? OrganizationId { get; set; }

    public string Status { get; set; }

    public DateTime? DateSent { get; set; }
}
