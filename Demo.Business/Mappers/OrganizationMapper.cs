using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Data.Models;

namespace Demo.Business.Mappers;

public static class OrganizationMapper
{
    public static OrganizationDto MapToOrganizationDto(Organization organization)
    {
        return new OrganizationDto
        {
            OrganizationId = organization.OrganizationId,
            Mission = organization.Mission,
            Website = organization.Website,
            MainBranch = organization.MainBranch,
            BankName = organization.BankName,
            BankAccount = organization.BankAccount
        };
    }

    public static OrganizationDetailsDto MapToOrganizationDetailsDto(Organization organization) 
    {
        return new OrganizationDetailsDto
        {
            OrganizationId = organization.OrganizationId,
            Mission = organization.Mission,
            Website = organization.Website,
            MainBranch = organization.MainBranch,
            BankName = organization.BankName,
            BankAccount = organization.BankAccount,
            Email = organization.User?.Email?? string.Empty, 
            Name = organization.User?.UserName ?? string.Empty, 
            Phone = organization.User?.PhoneNumber ?? string.Empty, 
            Photo = (organization.User?.Photo != null ? $"Photos/Organization/{organization.User.Photo}" : null)
        }; 
    }

    public static OrganizationWithApplicationsDto MapToOrgWithApplicationsDto(Organization organization)
    {
        return new OrganizationWithApplicationsDto
        {
            OrganizationId = organization.OrganizationId,
            Mission = organization.Mission,
            Website = organization.Website,
            MainBranch = organization.MainBranch,
            BankName = organization.BankName,
            BankAccount = organization.BankAccount,
            Applications = organization.Applications?.Select(a => ApplicationMapper.MapToApplicationDto(a)).ToList()
        };
    }
    public static OrganizationWithOpportunitiesDto MapToOrgWithOpportunitiesDto(Organization organization)
    {
        return new OrganizationWithOpportunitiesDto
        {
            OrganizationId = organization.OrganizationId,
            Mission = organization.Mission,
            Website = organization.Website,
            MainBranch = organization.MainBranch,
            BankName = organization.BankName,
            BankAccount = organization.BankAccount,
            Opportunities = organization.Opportunities?.Select(a => OpportunityMapper.MapToOpportunityDto(a)).ToList()
        };
    }

    public static Organization MapToOrganization(RegisterOrganizationDto dto, string id)
    {
        return new Organization
        {
            OrganizationId = id,
            Mission = dto.Mission,
            Website = dto.Website,
            MainBranch = dto.MainBranch,
            BankName = dto.BankName,
            BankAccount = dto.BankAccount
        };
    }

}
