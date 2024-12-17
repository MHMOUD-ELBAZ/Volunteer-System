global using Demo.Business.DTOs.Application;
global using Demo.Business.DTOs.Volunteer;
global using Demo.Business.IServices;
global using Demo.Data.IRepositories;
using Demo.Business.Mappers;

namespace Demo.Business.Services;

public class VolunteerService : IVolunteerService
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IVolunteerSkillRepository _vsRepository;
    private readonly IReviewRepository _reviewRepository;

    public VolunteerService(IVolunteerRepository volunteerRepository, IVolunteerSkillRepository vsRepository, IReviewRepository reviewRepository)
    {
        _volunteerRepository = volunteerRepository;
        _vsRepository = vsRepository;
        _reviewRepository = reviewRepository;
    }

    public void AddVolunteer(RegisterVolunteerDto volunteer, string id)
    {
        _volunteerRepository.Add(VolunteerMapper.MapToVolunteer(volunteer,id));
        _volunteerRepository.Save(); 
    }

    public VolunteerDto? GetById(string volunteerId)
    {
        var volunteer = _volunteerRepository.GetVolunteer(volunteerId);
        if(volunteer == null) return null;

        volunteer.VolunteerSkills = _vsRepository.GetSkillsForVolunteer(volunteerId).ToList(); 
        
        return  VolunteerMapper.MapToVolunteerDto(volunteer);
    }

    public IEnumerable<VolunteerDto> GetAllVolunteers()
    {
        var volunteers = _volunteerRepository.GetAll(); 

        return volunteers.Select(v => VolunteerMapper.MapToVolunteerDto(v));
    }

    public VolunteerDto? UpdateVolunteer(string volunteerId, UpdateVolunteerDto volunteerDto)
    {
        var volunteer = _volunteerRepository.GetVolunteer(volunteerId);
        if (volunteer == null) return null;

        volunteer.Address = volunteerDto.Address;
        volunteer.Bio = volunteerDto.Bio;
        volunteer.Age = volunteerDto.Age;

        _volunteerRepository.Update(volunteer);
        _volunteerRepository.Save();

        return VolunteerMapper.MapToVolunteerDto(volunteer);
    }

    public VolunteerWithApplicationsDto? GetWithApplications(string volunteerId)
    {
        var volunteer = _volunteerRepository.GetVolunteerWithApplications(volunteerId);

        return volunteer != null ? VolunteerMapper.MapToVolunteerWithAppDto(volunteer) : null;
    }
  
    public VolunteerDto? UpdateVolunteerSkills(UpdateVolunteerSkillsDto dto)
    {
        var old = _volunteerRepository.GetVolunteerWithSkills(dto.volunteerId); 
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

        _volunteerRepository.Update(old);
        _volunteerRepository.Save();

        return GetById(old.VolunteerId);
    }

    public VolunteerWithReviewsDto? GetWithReviews(string volunteerId)
    {
        var volunteer = _volunteerRepository.GetVolunteer(volunteerId);

        if (volunteer == null) return null;

        var reviews = _reviewRepository.GetForVolunteer(volunteerId); 
        return VolunteerMapper.MapToVolunteerWithReviewsDto(volunteer,reviews);
    }
}
