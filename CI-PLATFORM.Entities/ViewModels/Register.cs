using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CI_PLATFORM.Entities.ViewModels;

public class Register
{
    [Required(ErrorMessage = "Please enter First name.")]
    public string? FirstName { get; set; }
    [Required(ErrorMessage = "Please enter Last name.")]
    public string? LastName { get; set; }
    [Required(ErrorMessage = "Please enter Email.")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "Please enter Password.")]
    public string Password { get; set; } = null!;
    [Required(ErrorMessage = "Please enter PhoneNumber.")]
    public long PhoneNumber { get; set; }

}
