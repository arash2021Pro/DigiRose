using System.ComponentModel.DataAnnotations;

namespace DigiRose.Models.Users;

public class LoginViewModel
{
    [Required(ErrorMessage = "شماره همراه الزامیست")]
    public string Phonenumber { get; set; }
    [Required(ErrorMessage = "کلمه عبور الزامیست ")]
    public string Password { get; set; }
    public string? Message { get; set; }
    public bool IsCompleted { get; set; }
}