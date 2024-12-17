using Demo.Business.DTOs.Review;


namespace Demo.Business.DTOs.Volunteer;

public class VolunteerWithReviewsDto
{
    public required string  VolunteerId { get; set; } 

    public string Address { get; set; } = null!;

    public string? Bio { get; set; }

    public int Age { get; set; }

    public double? Rating { get; set; } = 0;

    public IEnumerable<ReviewDto>? Reviews { get; set; }
}
