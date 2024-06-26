﻿using System.ComponentModel.DataAnnotations;

namespace AzureFunctionWeb.Models
{
    public class SalesRequest
    {
        public string ApiKey { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
    }
}
