using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.DTOs.Skill;

public class SkillWithVolunteersDto : SkillDto  
{
    public List<VolunteerDto> Volunteers { get; set; } = new List<VolunteerDto>();
}
