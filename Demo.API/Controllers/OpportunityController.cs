﻿using Demo.Business.Services.Interfaces;
using Demo.Business.DTOs;
using Microsoft.AspNetCore.Mvc;
using Demo.Business.DTOs.Opportunity;

namespace Demo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OpportunityController : ControllerBase
{
    private readonly IOpportunityService _opportunityService;

    public OpportunityController(IOpportunityService opportunityService)
    {
        _opportunityService = opportunityService;
    }

    [HttpGet("GetAll")]
    public ActionResult<IEnumerable<OpportunityDto>> GetAll()
    {
        try
        {
            var opportunities = _opportunityService.GetAllOpportunities();
            return Ok(opportunities);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("{id}")]
    public ActionResult<OpportunityDto> GetById(int id)
    {
        try
        {
            var opportunity = _opportunityService.GetById(id);
            if (opportunity == null)
            {
                return NotFound($"Opportunity with ID {id} not found.");
            }

            return Ok(opportunity);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("{id}/organization")]
    public ActionResult<OpportunityDto> GetOpportunityWithOrganization(int id)
    {
        try
        {
            var opportunity = _opportunityService.GetOpportunityWithOrganization(id);
            if (opportunity == null)
            {
                return NotFound($"Opportunity with ID {id} not found.");
            }

            return Ok(opportunity);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("{id}/applications")]
    public ActionResult<OpportunityDto> GetOpportunityWithApplications(int id)
    {
        try
        {
            var opportunity = _opportunityService.GetOpportunityWithApplications(id);
            if (opportunity == null)
            {
                return NotFound($"Opportunity with ID {id} not found.");
            }

            return Ok(opportunity);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public ActionResult<OpportunityDto> Create([FromBody] CreateOpportunityDto opportunityDto)
    {
        try
        {
            if (opportunityDto == null)
            {
                return BadRequest("Opportunity data is null.");
            }

            var createdOpportunity = _opportunityService.Create(opportunityDto);
            return CreatedAtAction(nameof(GetById), new { id = createdOpportunity.Id }, createdOpportunity);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public ActionResult<OpportunityDto> Update(int id, [FromBody] CreateOpportunityDto opportunityDto)
    {
        try
        {
            if (opportunityDto == null)
            {
                return BadRequest("Opportunity data is null.");
            }

            var updatedOpportunity = _opportunityService.Update(id, opportunityDto);
            if (updatedOpportunity == null)
            {
                return NotFound($"Opportunity with ID {id} not found.");
            }
            
            return Ok(updatedOpportunity);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            var isDeleted = _opportunityService.Delete(id);
            if (!isDeleted)
            {
                return NotFound($"Opportunity with ID {id} not found.");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
