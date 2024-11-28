using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.DTOs.Review;

public class ReviewWithAllDataDto
{
    public int Id { get; set; }
    public ApplicationDto Application { get; set; }
    public VolunteerDto Volunteer { get; set; }
    public OrganizationDto Organization { get; set; }
    public int Rating { get; set; }
    public DateTime? DateReviewed { get; set; }
    public string? Comment { get; set; }
}
