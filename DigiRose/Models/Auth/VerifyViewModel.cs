using System.ComponentModel.DataAnnotations;

namespace DigiRose.Models.Users;

public class VerifyViewModel
{
    [Required(ErrorMessage = "کد تایید الزامیست")]
    public string code { get; set; }
    public string? phonenumber { get; set; }
    public string? Message { get; set; }
    public bool IsCompleted { get; set; }
}