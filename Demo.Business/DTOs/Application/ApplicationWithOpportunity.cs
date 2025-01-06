using Demo.Business.DTOs.Opportunity;


namespace Demo.Business.DTOs.Application
{
    public class ApplicationWithOpportunity : ApplicationDto
    {
        public OpportunityDto Opportunity { get; set; }
    }
}
