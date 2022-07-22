using System.ComponentModel.DataAnnotations;

namespace DigiRose.Models.Admin;

public class LogoViewModel
{
    [Required(ErrorMessage = "انتخاب فایل الزامیست")]
    public IFormFile File { get; set; }
    public string ? Message { get; set; }
    public bool IsCompleted { get; set; }
}