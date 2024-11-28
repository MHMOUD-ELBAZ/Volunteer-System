using Demo.Business.DTOs.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.DTOs.Application;

public class ApplicationWithReviewsDto : ApplicationDto
{
    public List<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
}
