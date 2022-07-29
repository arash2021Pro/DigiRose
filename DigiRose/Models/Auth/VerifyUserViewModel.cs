using System.ComponentModel.DataAnnotations;

namespace DigiRose.Models.Users;

public class VerifyUserViewModel
{
    [Required(ErrorMessage = "موبایل الزامیست")]
    public string ? Phone { get; set; }
    public string ? Message { get; set; }
    public string? Code { get; set; }
    public bool IsCompleted { get; set; }
}