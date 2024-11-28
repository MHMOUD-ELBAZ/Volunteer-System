global using Demo.Business.DTOs.Application;
global using Demo.Business.DTOs.Volunteer;
global using Demo.Business.IServices;
global using Demo.Data.IRepositories;
using Demo.Business.Mappers;

namespace Demo.Business.Services;

public class VolunteerService : IVolunteerService
{
    private readonly IVolunteerRepository volunteerRepository;
    private readonly IVolunteerSkillRepository vsRepository;

    public VolunteerService(IVolunteerRepository volunteerRepository, IVolunteerSkillRepository vsRepository)
    {
        this.volunteerRepository = volunteerRepository;
        this.vsRepository = vsRepository;
    }

    public void AddVolunteer(RegisterVolunteerDto volunteer, string id)
    {
        volunteerRepository.Add(VolunteerMapper.MapToVolunteer(volunteer,id));
        volunteerRepository.Save(); 
    }

    public VolunteerDto? GetById(string volunteerId)
    {
        var volunteer = volunteerRepository.GetVolunteer(volunteerId);
        if(volunteer == null) return null;

        volunteer.VolunteerSkills = vsRepository.GetSkillsForVolunteer(volunteerId).ToList(); 
        
        return  VolunteerMapper.MapToVolunteerDto(volunteer);
    }

    public IEnumerable<VolunteerDto> GetAllVolunteers()
    {
        var volunteers = volunteerRepository.GetAll(); 

        return volunteers.Select(v => VolunteerMapper.MapToVolunteerDto(v));
    }

    public VolunteerDto? UpdateVolunteer(string volunteerId, UpdateVolunteerDto volunteerDto)
    {
        var volunteer = volunteerRepository.GetVolunteer(volunteerId);
        if (volunteer == null) return null;

        volunteer.Address = volunteerDto.Address;
        volunteer.Bio = volunteerDto.Bio;
        volunteer.Age = volunteerDto.Age;

        volunteerRepository.Update(volunteer);
        volunteerRepository.Save();

        return VolunteerMapper.MapToVolunteerDto(volunteer);
    }

    public VolunteerWithApplicationsDto? GetWithApplications(string volunteerId)
    {
        var volunteer = volunteerRepository.GetVolunteerWithApplications(volunteerId);

        return volunteer != null ? VolunteerMapper.MapToVolunteerWithAppDto(volunteer) : null;
    }
  
    public VolunteerDto? UpdateVolunteerSkills(UpdateVolunteerSkillsDto dto)
    {
        var old = volunteerRepository.GetVolunteerWithSkills(dto.volunteerId); 
        if(old == null) return null;
        
        old.VolunteerSkills.Clear();
        
        if (dto.SkillIds != null)
        {    
            foreach (var skillId in dto.SkillIds)
            {
                old.VolunteerSkills.Add(new VolunteerSkill
                {
                    VolunteerId = old.VolunteerId,
                    SkillId = skillId
                });
            }
        }

        volunteerRepository.Update(old);
        volunteerRepository.Save();

        return GetById(old.VolunteerId);
    }
}
