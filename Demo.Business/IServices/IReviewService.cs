using Demo.Business.DTOs.Review;


namespace Demo.Business.IServices;

public interface IReviewService
{
    ReviewDto? GetReviewById(int id);
    ReviewWithAllDataDto? GetReviewWithAllData(int id);
    ReviewWithApplicationDto? GetReviewWithApplication(int id);
    IEnumerable<ReviewDto> GetAllReviews();
    ReviewDto CreateReview(CreateReviewDto createReviewDto, string organizationId);
    ReviewDto? UpdateReview(int id, UpdateReviewDto updateReviewDto, string organizationId);
    bool DeleteReview(int id, string organizationId);
}



