﻿using CI_PLATFORM.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CI_PLATFORM.Entities.ViewModels;

public class Login
{
    //[Required(ErrorMessage = "Please enter Email.")]
    public string Email { get; set; } = null!;
    //[Required(ErrorMessage = "Please enter Password.")]
    public string Password { get; set; } = null!;
    public User user { get; set; } 
    public Admin admin { get; set; }
    public string? returnUrl { get; set; }
}
public class Register
{
    [Required(ErrorMessage = "Please enter First name.")]
    public string? FirstName { get; set; }
    [Required(ErrorMessage = "Please enter Last name.")]
    public string? LastName { get; set; }
    [Required(ErrorMessage = "Please enter Email.")]
    public string Email { get; set; } = null!;
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one uppercase letter and one digit")]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters long")]
    public string Password { get; set; } = null!;
    [Required(ErrorMessage = "Please enter ConfirmPassword.")]
    [Compare("Password", ErrorMessage = "Password must match")]
    public string ConfirmPassword { get; set; } = null!;

    [Required(ErrorMessage = "Please enter PhoneNumber.")]
  
    public long PhoneNumber { get; set; }

}

public class ForgotPwd
{
    [Required(ErrorMessage = "Email is Required")]
    public string Email { get; set; } = null!;

}

public class ResetPwd
{

    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one uppercase letter and one digit")]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters long")]
    [Required(ErrorMessage = "Password is Required")]
    public string Password { get; set; } = null!;

    [Compare("Password", ErrorMessage = "Password must match")]
    [Required(ErrorMessage = "Confirm PassWord is Required")]
    public string ConfirmPassword { get; set; } = null!;


}