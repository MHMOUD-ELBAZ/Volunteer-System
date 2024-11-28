using Demo.Business.DTOs.Opportunity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.DTOs.Application;

public class ApplicationWithOpportunityAndVolunteerDto : ApplicationDto
{
    public OpportunityDto Opportunity { get; set; }
    public VolunteerDto Volunteer { get; set; }
}
