﻿using Microsoft.AspNetCore.Mvc;
using UniversityRestApi.Dto;
using UniversityRestApi.Services;

namespace UniversityRestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController: ControllerBase
{
    private readonly StudentsService service;

    public StudentsController(StudentsService service)
    {
        this.service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddStudent(StudentRegistrationData registrationData)
    {
        StudentRegistrationResponseData response = await service.RegisterStudent(registrationData);
        return CreatedAtAction(nameof(GetStudentByID),new {response.ID}, response);
    }


    [HttpGet, Route("{id}")]
    public async Task<IActionResult> GetStudentByID(Guid id)
    {
        StudentResponseData data = await service.GetStudentByID(id);
        return Ok(data);
    }
}