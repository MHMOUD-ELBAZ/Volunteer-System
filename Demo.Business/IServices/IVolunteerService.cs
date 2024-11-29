global using Demo.Data.Models;


namespace Demo.Business.IServices;

public interface IVolunteerService
{
    void AddVolunteer(RegisterVolunteerDto volunteer, string id);

    IEnumerable<VolunteerDto> GetAllVolunteers();
    VolunteerDto? GetById(string volunteerId);
    VolunteerDto? UpdateVolunteer(string volunteerId, UpdateVolunteerDto volunteerDto);

    VolunteerDto? UpdateVolunteerSkills(UpdateVolunteerSkillsDto dto);

    VolunteerWithApplicationsDto? GetWithApplications(string volunteerId);

    //IEnumerable<OpportunityDto> GetVolunteerOpportunities(string volunteerId);
    //IEnumerable<ReviewDto> GetVolunteerReviews(string volunteerId);
}
