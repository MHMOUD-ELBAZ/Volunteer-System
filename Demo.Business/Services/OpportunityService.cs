using Demo.Business.DTOs;
using Demo.Business.DTOs.Opportunity;
using Demo.Business.Exceptions;
using Demo.Business.Mappers;



namespace Demo.Business.Services;

public class OpportunityService : IOpportunityService
{
    private readonly IOpportunityRepository _opportunityRepository;
    private readonly ISkillRepository _skillRepository;
    private readonly IOpportunitySkillRepository _opportunitySkillRepository;
    private readonly IOrganizationRepository _organizationRepository;

    public OpportunityService(IOpportunityRepository opportunityRepository, ISkillRepository skillRepository, 
        IOpportunitySkillRepository opportunitySkillRepository, IOrganizationRepository organizationRepository)
    {
        _opportunityRepository = opportunityRepository;
        _skillRepository = skillRepository;
        _opportunitySkillRepository = opportunitySkillRepository;
        this._organizationRepository = organizationRepository;
    }

    public OpportunityDto? GetById(int id)
    {
        var opportunity = _opportunityRepository.Get(id);
       
        return opportunity != null ? OpportunityMapper.MapToOpportunityDto(opportunity) : null;
    }

    public OpportunityWithOrganizationDto? GetOpportunityWithOrganization(int id)
    {
        var opportunity = _opportunityRepository.Get(id);

        if (opportunity == null) return null; 
        
        opportunity.Organization = _organizationRepository.Get(opportunity.OrganizationId);

        return OpportunityMapper.MapToOpportunityWithOrganizationDto(opportunity) ;
    }

    public OpportunityWithApplicationsDto? GetOpportunityWithApplications(int id)
    {
        var opportunity = _opportunityRepository.GetWithApplications(id);
        return opportunity != null ? OpportunityMapper.MapToOpportunityWithApplicationsDto(opportunity) : null;
    }

    public IEnumerable<OpportunityWithOrganizationDto> GetAllOpportunities()
    {
        var opportunities = _opportunityRepository.GetAll().ToList();
        return opportunities.Select(OpportunityMapper.MapToOpportunityWithOrganizationDto);
    }


    public OpportunityDto Create(CreateOpportunityDto opportunityDto, string organizationId)
    {
        //Check valid IDs
        if(opportunityDto.SkillIDs?.Any() ?? false)
        {
            List<Skill> skills = new List<Skill>(_skillRepository.GetAll()); 
            foreach(var id in opportunityDto.SkillIDs)
            {
                if (!skills.Where(s => s.Id == id).Any())
                    throw new Exception($"No skill with ID = {id}");
            }
        }
        
        //Add opp
        var opportunity = OpportunityMapper.MapToOpportunity(opportunityDto, organizationId);
        _opportunityRepository.Add(opportunity);
        _opportunityRepository.Save();


        //Add OppSkills
        if (opportunityDto.SkillIDs?.Any() ?? false)
        {
            foreach (var id in opportunityDto.SkillIDs)
            {
                _opportunitySkillRepository.Add(new OpportunitySkill { OpportunityId = opportunity.Id, SkillId = id });
            }
            _opportunitySkillRepository.Save();
        }

        

        return OpportunityMapper.MapToOpportunityDto(opportunity);
    }


    public OpportunityDto? Update(int id,CreateOpportunityDto updatedOpportunityDto, string organizationId)
    {
        var opportunity = _opportunityRepository.Get(id);

        if (opportunity == null)
            return null; 

        if(opportunity.OrganizationId != organizationId)
            throw new ForbiddenAccessException("You are not authorized to update this opportunity.");


        opportunity.Description = updatedOpportunityDto.Description;
        opportunity.IsOnline = updatedOpportunityDto.IsOnline;
        opportunity.Deadline = updatedOpportunityDto.Deadline;

        
        _opportunitySkillRepository.UpdateOpportunitySkills(opportunity, updatedOpportunityDto.SkillIDs); 
        
        _opportunityRepository.Update(opportunity);
        _opportunityRepository.Save();

        return GetById(opportunity.Id);
    }

    public bool Delete(int id, string organizationId)
    {
        var opportunity = _opportunityRepository.Get(id);
        if (opportunity == null)
            throw new NotFoundException($"No opportunity found with ID: {id}");

        if (opportunity.OrganizationId != organizationId)
            throw new ForbiddenAccessException("You are not authorized to delete this opportunity.");

        _opportunityRepository.Delete(opportunity);
        _opportunityRepository.Save();

        return true;
    }
}

