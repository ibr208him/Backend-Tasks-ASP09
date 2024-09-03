﻿using System.ComponentModel.DataAnnotations;

namespace CRUD.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        [DataType("varchar")]
        public string? UserName { get; set; }

        [DataType(DataType.Password)]
        public string? Password  { get; set; } 
        public bool? IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
