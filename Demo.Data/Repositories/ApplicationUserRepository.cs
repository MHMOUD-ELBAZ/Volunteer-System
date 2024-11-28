using Demo.Data.IRepositories;
using Demo.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Demo.Data.Repositories;

public class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationUserRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApplicationUser> Get(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }
}
