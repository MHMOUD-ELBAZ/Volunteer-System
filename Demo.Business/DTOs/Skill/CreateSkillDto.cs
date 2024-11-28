using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.DTOs.Skill;

public class CreateSkillDto
{
    public required string Name { get; set; }

    public string? Descritption { get; set; }
}
