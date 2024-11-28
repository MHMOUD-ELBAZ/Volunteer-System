using Demo.Business.DTOs.Skill;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.DTOs.Volunteer;

public class VolunteerDto
{
    public string VolunteerId { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string? Bio { get; set; }
    public int Age { get; set; }
    public double? Rating { get; set; } = 0;


    public string Name { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Photo { get; set; } = null!;

    // Skills
    public List<SkillDto> Skills { get; set; } = new List<SkillDto>();
}
