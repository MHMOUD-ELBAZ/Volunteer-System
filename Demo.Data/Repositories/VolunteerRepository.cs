using Demo.Data.IRepositories;
using Demo.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace Demo.Data.Repositories;

public class  VolunteerRepository: Repository<Volunteer>, IVolunteerRepository
{
    public VolunteerRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public new IEnumerable<Volunteer> GetAll()
    {
        return _dbContext.Volunteers.Include(v => v.User).AsNoTracking(); 
    }

    public Volunteer? GetVolunteer(string id)
    {
        return _dbContext.Volunteers.Include(v => v.User).FirstOrDefault(v => v.VolunteerId == id); 
    }

    public Volunteer? GetVolunteerWithApplications(string id)
    {
        return _dbContext.Volunteers.Include(v => v.Applications).FirstOrDefault(v => v.VolunteerId == id);
    }

    public Volunteer? GetVolunteerWithSkills(string id)
    {
        return _dbContext.Volunteers.Include(v => v.VolunteerSkills).FirstOrDefault(v => v.VolunteerId == id);
    }

}
