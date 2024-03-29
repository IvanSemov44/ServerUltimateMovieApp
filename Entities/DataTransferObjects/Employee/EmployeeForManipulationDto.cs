﻿using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.Employee
{
    public abstract class EmployeeForManipulationDto
    {
        [Required(ErrorMessage = "Employee name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; } = string.Empty;


        [Required]
        [Range(18, int.MaxValue, ErrorMessage = "Age is required and it can't be lower than 18")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Position name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Position is 20 characters.")]
        public string Position { get; set; } = string.Empty;
    }
}
