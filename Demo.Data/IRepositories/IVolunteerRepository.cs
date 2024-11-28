using Demo.Data.Models;


namespace Demo.Data.IRepositories;

public interface IVolunteerRepository : IRepository<Volunteer>
{
    public Volunteer? GetVolunteer(string id); 
    public Volunteer? GetVolunteerWithSkills(string id); 
    public Volunteer? GetVolunteerWithApplications(string id); 
}
