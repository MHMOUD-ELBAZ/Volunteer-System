using Demo.Business.DTOs.Review;
using Demo.Business.Exceptions;
using Demo.Business.Mappers;


namespace Demo.Business.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;

    private readonly IApplicationRepository _applicationRepository;

    public ReviewService(IReviewRepository reviewRepository, IApplicationRepository applicationRepository)
    {
        _reviewRepository = reviewRepository;
        _applicationRepository = applicationRepository;
    }

    public ReviewDto? GetReviewById(int id)
    {
        var review = _reviewRepository.Get(id);
        return review != null ? ReviewMapper.MapToReviewDto(review) : null;
    }

    public ReviewWithAllDataDto? GetReviewWithAllData(int id)
    {
        var review = _reviewRepository.Get(id);

        if (review == null) return null;

        review.Application = _applicationRepository.GetWithVolunteerAndOrganization(id);
        return  ReviewMapper.MapToReviewWithAllDataDto(review) ;
    }


    public ReviewWithApplicationDto? GetReviewWithApplication(int id)
    {
        var review = _reviewRepository.GetWithApplication(id);
        return review != null ? ReviewMapper.MapToReviewWithApplicationDto(review) : null;
    }

    public IEnumerable<ReviewDto> GetAllReviews()
    {
        var reviews = _reviewRepository.GetAll().ToList();
        return reviews.Select(ReviewMapper.MapToReviewDto);
    }

    public ReviewDto? CreateReview(CreateReviewDto createReviewDto, string organizationId)
    {
        var application = _applicationRepository.GetWithOpportunity(createReviewDto.ApplicationId);

        if (application == null) 
            return null;

        if (application.Opportunity?.OrganizationId != organizationId) 
            throw new ForbiddenAccessException("You are not authorized to review this application.");


        var review = ReviewMapper.MapToReview(createReviewDto);
        _reviewRepository.Add(review);
        _reviewRepository.Save();

        return ReviewMapper.MapToReviewDto(review);
    }

    public ReviewDto? UpdateReview(int id, UpdateReviewDto updateReviewDto, string organizationId)
    {
        var review = _reviewRepository.GetWithApplication(id);

        if (review == null) return null;

        if (review.Application?.Opportunity?.OrganizationId != organizationId)
            throw new ForbiddenAccessException("You are not authorized to update this review.");

        review.Rating = updateReviewDto.Rating;
        review.Comment = updateReviewDto.Comment;

        _reviewRepository.Update(review);
        _reviewRepository.Save();

        return ReviewMapper.MapToReviewDto(review);
    }

    public bool DeleteReview(int id, string organizationId)
    {
        var review = _reviewRepository.GetWithApplication(id);

        if (review == null) 
            throw new NotFoundException($"Review with ID: {id} not found.");

        if (review.Application?.Opportunity?.OrganizationId != organizationId)
            throw new ForbiddenAccessException("You are not authorized to delete this review.");


        _reviewRepository.Delete(review);
        _reviewRepository.Save();

        return true;
    }
}
