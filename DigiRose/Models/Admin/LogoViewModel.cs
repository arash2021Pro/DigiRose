using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using DigiRose.CoreStorage.Migrations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;

namespace DigiRose.Models.Admin;

public class LogoViewModel
{
    [Required(ErrorMessage = "انتخاب فایل الزامیست")]
    [Description("this file'used as logo image for admin pannel")]
    [DataType(DataType.Upload,ErrorMessage = "شما فقظ مجاز به اپلود فایل هستید")]
    public IFormFile File { get; set; }
    public string ? Message { get; set; }
    public bool IsCompleted { get; set; }
}