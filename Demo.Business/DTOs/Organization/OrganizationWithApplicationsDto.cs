using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.DTOs.Organization;

public class OrganizationWithApplicationsDto : OrganizationDto
{
    public List<ApplicationDto>? Applications { get; set; }
}
