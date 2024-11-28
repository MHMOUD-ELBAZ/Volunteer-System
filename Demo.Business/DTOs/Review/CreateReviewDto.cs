using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.DTOs.Review;

public class CreateReviewDto
{
    public int? ApplicationId { get; set; }
    public string? VolunteerId { get; set; }
    public string? OrganizationId { get; set; }

    [Range(0,5)]
    public int Rating { get; set; }
    public string? Comment { get; set; }
}
