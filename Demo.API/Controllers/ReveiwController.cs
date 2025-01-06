using Demo.Business.Exceptions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Demo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ExceptionFilter]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }


    [HttpGet("GetAll")]
    public ActionResult<IEnumerable<ReviewDto>> GetAll()
    {
        var reviews = _reviewService.GetAllReviews();
        return Ok(reviews);
    }


    [HttpGet("{id}")]
    public ActionResult<ReviewDto> GetById(int id)
    {
        var review = _reviewService.GetReviewById(id);
        if (review == null)
            return NotFound($"Review with ID {id} not found.");

        return Ok(review);
    }


    [HttpGet("{id}/details")]
    public ActionResult<ReviewWithAllDataDto> GetWithAllData(int id)
    {
        var review = _reviewService.GetReviewWithAllData(id);
        if (review == null)
            return NotFound($"Review with ID {id} not found.");

        return Ok(review);
    }


    [HttpGet("{id}/application")]
    public ActionResult<ReviewWithApplicationDto> GetWithApplication(int id)
    {
        var review = _reviewService.GetReviewWithApplication(id);
        if (review == null)
            return NotFound($"Review with ID {id} not found.");

        return Ok(review);
    }


    [HttpPost]
    [Authorize]
    [OrganizationFilter]
    public ActionResult<ReviewDto> Create([FromBody] CreateReviewDto createReviewDto)
    {
        if (createReviewDto == null)
            return BadRequest("Review data is null.");

        string organizationId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";

        var createdReview = _reviewService.CreateReview(createReviewDto, organizationId);

        if (createdReview == null)
            return NotFound($"Application with ID: {createReviewDto.ApplicationId} not found.");

        return CreatedAtAction(nameof(GetById), new { id = createdReview.Id }, createdReview);
    }


    [HttpPut("{id}")]
    [Authorize]
    [OrganizationFilter]
    public ActionResult<ReviewDto> Update(int id, [FromBody] UpdateReviewDto updateReviewDto)
    {
        if (updateReviewDto == null)
            return BadRequest("Review data is null.");

        string organizationId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";

        var updatedReview = _reviewService.UpdateReview(id, updateReviewDto, organizationId);
        if (updatedReview == null)
            return NotFound($"Review with ID {id} not found.");

        return Ok(updatedReview);
    }


    [HttpDelete("{id}")]
    [Authorize]
    [OrganizationFilter]
    public IActionResult Delete(int id)
    {
        string organizationId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";

        _reviewService.DeleteReview(id, organizationId);

        return NoContent();
    }
}
