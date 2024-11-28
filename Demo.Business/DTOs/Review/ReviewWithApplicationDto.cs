using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.DTOs.Review;

public class ReviewWithApplicationDto
{
    public int Id { get; set; }
    public string? VolunteerId { get; set; }
    public string? OrganizationId { get; set; }
    public int Rating { get; set; }
    public DateTime? DateReviewed { get; set; }
    public string? Comment { get; set; }

    public ApplicationDto? Application { get; set; }

}
