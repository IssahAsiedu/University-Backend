﻿namespace UniversityRestApi.Dto;

public record CourseDto(string Title, string Credits);

public record CourseUpdateDto(string? Title, string? Credits, int ID)
{
    public bool UpdateProvided()
    {
        return Title != null && Credits != null;
    }
}

public record PaginationFilter(int CurrentIndex = 0, int PageSize = 10);