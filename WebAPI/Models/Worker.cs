﻿using WebAPI.Models.Base;

namespace WebAPI.Models
{
    public class Worker : BaseModel
    {
        public string? Name { get; set; }
        public string? Position { get; set; }
        public string? Telephone { get; set; }
    }
}
