using System.ComponentModel.DataAnnotations;
using DNTPersianUtils.Core;

namespace DigiRose.Models.Users;

public class SignupViewModel
{
    [Required(ErrorMessage = "شماره همراه الزامیست")]
    
    public string Phonenumber { get; set; }
    [Required(ErrorMessage = "کلمه ی عبور الزامیست ")]
    public string Password { get; set; }
    public string? Message { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsObedient { get; set; }
    public string?RefCode { get; set; }
}