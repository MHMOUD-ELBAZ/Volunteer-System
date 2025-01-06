using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.DTOs.Application;

public class UpdateApplicationDto
{
    public int Id { get; set; }
    
    public required string Status { get; set; }   
}
