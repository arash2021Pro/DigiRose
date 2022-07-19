using System.ComponentModel.DataAnnotations;

namespace DigiRose.Models.Users;

public class ResetPasswordViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "کلمه ی عبور جدید الزامیست")]
    public string? Password { get; set; }
    
    public string? Message { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsSuccessful { get; set; }
}