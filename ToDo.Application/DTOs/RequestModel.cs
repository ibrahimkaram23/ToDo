﻿namespace ToDo.Application.DTOs
{
    public class RequestModel
    {
        public string? Search { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
