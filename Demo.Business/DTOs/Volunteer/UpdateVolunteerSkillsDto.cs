using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.DTOs.Volunteer
{
    public class UpdateVolunteerSkillsDto
    {
        public string volunteerId { get; set; }
        public List<int>? SkillIds { get; set; }
    }
}
