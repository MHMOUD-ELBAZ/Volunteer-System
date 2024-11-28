using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.DTOs.Skill;

public class SkillDto
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? Descritption { get; set; }
}
