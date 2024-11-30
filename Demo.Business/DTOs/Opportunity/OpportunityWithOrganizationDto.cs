using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.DTOs.Opportunity
{
    public class OpportunityWithOrganizationDto : OpportunityDto
    {
        public OrganizationDetailsDto? Organization { get; set; }
    }
}
