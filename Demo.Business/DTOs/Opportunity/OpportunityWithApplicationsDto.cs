using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.DTOs.Opportunity
{
    public class OpportunityWithApplicationsDto : OpportunityDto
    {
        public List<ApplicationDto> Applications { get; set; }
    }
}
