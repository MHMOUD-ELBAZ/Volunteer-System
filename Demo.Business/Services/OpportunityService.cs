using Demo.Business.DTOs;
using Demo.Business.DTOs.Opportunity;
using Demo.Business.Mappers;
using Demo.Business.Services.Interfaces;


namespace Demo.Business.Services;

public class OpportunityService : IOpportunityService
{
    private readonly IOpportunityRepository _opportunityRepository;
    private readonly ISkillRepository _skillRepository;
    private readonly IOpportunitySkillRepository _opportunitySkillRepository;

    public OpportunityService(IOpportunityRepository opportunityRepository, ISkillRepository skillRepository, 
        IOpportunitySkillRepository opportunitySkillRepository)
    {
        _opportunityRepository = opportunityRepository;
        _skillRepository = skillRepository;
        _opportunitySkillRepository = opportunitySkillRepository;
    }

    public OpportunityDto? GetById(int id)
    {
        var opportunity = _opportunityRepository.Get(id);
        
        if(opportunity != null)
        {
            opportunity.OpportunitySkills = new List<OpportunitySkill>(_opportunitySkillRepository.GetSkillsForOpportunity(id));

        }

        return opportunity != null ? OpportunityMapper.MapToOpportunityDto(opportunity) : null;
    }

    public OpportunityWithOrganizationDto? GetOpportunityWithOrganization(int id)
    {
        var opportunity = _opportunityRepository.GetWithOrganization(id);
        return opportunity != null ? OpportunityMapper.MapToOpportunityWithOrganizationDto(opportunity) : null;
    }

    public OpportunityWithApplicationsDto? GetOpportunityWithApplications(int id)
    {
        var opportunity = _opportunityRepository.GetWithApplications(id);
        return opportunity != null ? OpportunityMapper.MapToOpportunityWithApplicationsDto(opportunity) : null;
    }

    public IEnumerable<OpportunityDto> GetAllOpportunities()
    {
        var opportunities = _opportunityRepository.GetAll().ToList();
        return opportunities.Select(OpportunityMapper.MapToOpportunityDto);
    }

    public OpportunityDto Create(CreateOpportunityDto opportunityDto)
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
        var opportunity = OpportunityMapper.MapToOpportunity(opportunityDto);
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

    public OpportunityDto? Update(int id, CreateOpportunityDto opportunityDto)
    {
        var opportunity = _opportunityRepository.Get(id);
        if (opportunity == null) return null;

        opportunity.Description = opportunityDto.Description;
        opportunity.IsOnline = opportunityDto.IsOnline;
        opportunity.Deadline = opportunityDto.Deadline;

        _opportunityRepository.Update(opportunity);

        _opportunitySkillRepository.UpdateOpportunitySkills(opportunity, opportunityDto.SkillIDs); 

        _opportunityRepository.Save();

        return GetById(id);
    }

    public bool Delete(int id)
    {
        var opportunity = _opportunityRepository.Get(id);
        if (opportunity == null) return false;

        _opportunityRepository.Delete(opportunity);
        _opportunityRepository.Save();
        return true;
    }
}

