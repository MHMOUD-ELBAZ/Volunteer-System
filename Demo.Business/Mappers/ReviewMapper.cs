using Demo.Business.DTOs.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.Mappers;

public class ReviewMapper
{
    public static ReviewDto MapToReviewDto(Review review)
    {
        return new ReviewDto
        {
            ApplicationId = review.ApplicationId,
            DateReviewed = review.DateReviewed,
            Comment = review.Comment, 
            Id = review.Id, 
            Rating = review.Rating
        };
    }

    public static ReviewWithAllDataDto MapToReviewWithAllDataDto(Review review)
    {
        return new ReviewWithAllDataDto
        {
            Id = review.Id,
            Application = ApplicationMapper.MapToApplicationDto(review.Application),
            Volunteer = VolunteerMapper.MapToVolunteerDto(review.Application.Volunteer),
            Organization = OrganizationMapper.MapToOrganizationDto(review.Application.Opportunity.Organization),
            Rating = review.Rating,
            DateReviewed = review.DateReviewed,
            Comment = review.Comment
        };
    }

    public static ReviewWithApplicationDto MapToReviewWithApplicationDto(Review review)
    {
        return new ReviewWithApplicationDto
        {
            Id = review.Id, 
            Application = ApplicationMapper.MapToApplicationWithOpportunityDto(review.Application),
            Rating = review.Rating,
            DateReviewed = review.DateReviewed,
            Comment = review.Comment,
        };
    }

    public static Review MapToReview(CreateReviewDto createReviewDto)
    {
        return new Review
        {
            ApplicationId = createReviewDto.ApplicationId,
            Rating = createReviewDto.Rating,
            Comment = createReviewDto.Comment
        };
    }
}
