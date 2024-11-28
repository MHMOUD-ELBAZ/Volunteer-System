using Demo.Business.DTOs.Opportunity;
using Demo.Business.IServices;
using Demo.Business.Mappers;
using Demo.Data.IRepositories;
using Demo.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.Services;

public class OrganizationService : IOrganizationService
{
    private readonly IOrganizationRepository _organizationRepository;

    public OrganizationService(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public void Add(RegisterOrganizationDto dto, string id)
    {      
        _organizationRepository.Add(OrganizationMapper.MapToOrganization(dto, id));
        _organizationRepository.Save(); 
    }

    public OrganizationDetailsDto? GetOrganizationById(string id)
    {
        var organization = _organizationRepository.Get(id);
        return organization != null ? OrganizationMapper.MapToOrganizationDetailsDto(organization) : null;
    }

    public OrganizationWithApplicationsDto? GetOrganizationWithApplications(string id)
    {
        var organization = _organizationRepository.GetWithApplications(id);
        return organization != null ? OrganizationMapper.MapToOrgWithApplicationsDto(organization) : null;
    }

    public OrganizationWithOpportunitiesDto? GetOrganizationWithOpportunities(string id)
    {
        var organization = _organizationRepository.GetWithOpportunities(id);
        return organization != null ? OrganizationMapper.MapToOrgWithOpportunitiesDto(organization) : null;
    }

    public IEnumerable<OrganizationDto> GetAllOrganizations()
    {
        var organizations = _organizationRepository.GetAll().ToList();
        return organizations.Select(o => OrganizationMapper.MapToOrganizationDto(o));
    }

    public OrganizationDto? UpdateOrganization(string id, UpdateOrganizationDto organizationDto)
    {
        var organization = _organizationRepository.Get(id);
        if (organization == null) return null;

        organization.Mission = organizationDto.Mission;
        organization.Website = organizationDto.Website;
        organization.MainBranch = organizationDto.MainBranch;
        organization.BankName = organizationDto.BankName;
        organization.BankAccount = organizationDto.BankAccount;

        _organizationRepository.Update(organization);
        _organizationRepository.Save();

        return OrganizationMapper.MapToOrganizationDto(organization);
    }

    public bool DeleteOrganization(string id)
    {
        var organization = _organizationRepository.Get(id);
        if (organization == null) return false;

        _organizationRepository.Delete(organization);
        _organizationRepository.Save();

        return true;
    }

}
