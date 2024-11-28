using Demo.Business.DTOs.Opportunity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class OrganizationWithOpportunitiesDto : OrganizationDto
{
    public List<OpportunityDto>? Opportunities { get; set; }
}

