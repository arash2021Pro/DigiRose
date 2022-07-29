using System.ComponentModel.DataAnnotations;

namespace DigiRose.Models.Admin;

public class EditUserPhoneViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "موبایل الزامیست")]
    public string ? Phonenumber { get; set; }
    public string ? Message { get; set; }
    public bool IsCompleted { get; set; }
}