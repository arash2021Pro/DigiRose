using System.ComponentModel.DataAnnotations;

namespace DigiRose.Models.Users;

public class ForgetPasswordViewModel
{
    [Required(ErrorMessage = "موبایل الزامیست")]
    public string? Phonenumber { get; set; }
    public string? Message { get; set; }
    public bool IsCompleted { get; set; }
}