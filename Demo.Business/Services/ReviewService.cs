using Demo.Business.DTOs.Review;
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

    public ReviewDto CreateReview(CreateReviewDto createReviewDto)
    {
        var review = ReviewMapper.MapToReview(createReviewDto);
        _reviewRepository.Add(review);
        _reviewRepository.Save();

        return ReviewMapper.MapToReviewDto(review);
    }

    public ReviewDto? UpdateReview(int id, UpdateReviewDto updateReviewDto)
    {
        var review = _reviewRepository.Get(id);
        if (review == null) return null;

        review.Rating = updateReviewDto.Rating;
        review.Comment = updateReviewDto.Comment;

        _reviewRepository.Update(review);
        _reviewRepository.Save();

        return ReviewMapper.MapToReviewDto(review);
    }

    public bool DeleteReview(int id)
    {
        var review = _reviewRepository.Get(id);
        if (review == null) return false;

        _reviewRepository.Delete(review);
        _reviewRepository.Save();

        return true;
    }
}
