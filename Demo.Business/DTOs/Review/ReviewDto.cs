using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.DTOs.Review;

public class ReviewDto
{
    public int Id { get; set; }

    public int? ApplicationId { get; set; }

    [Range(0, 5)]
    public int Rating { get; set; } = 0; 

    public DateTime? DateReviewed { get; set; }

    [StringLength(256)]
    public string? Comment { get; set; }
}
