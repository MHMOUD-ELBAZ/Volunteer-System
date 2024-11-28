﻿using Demo.Business.DTOs.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.IServices;

public interface IReviewService
{
    ReviewDto? GetReviewById(int id);
    ReviewWithAllDataDto? GetReviewWithAllData(int id);
    ReviewWithApplicationDto? GetReviewWithApplication(int id);
    IEnumerable<ReviewDto> GetAllReviews();
    ReviewDto CreateReview(CreateReviewDto createReviewDto);
    ReviewDto? UpdateReview(int id, UpdateReviewDto updateReviewDto);
    bool DeleteReview(int id);
}


