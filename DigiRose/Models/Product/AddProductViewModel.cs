using System.ComponentModel.DataAnnotations;
using DigiRose.CoreBussiness.StorageEntity.ProductCategory;

namespace DigiRose.Models.Product;

public class AddProductViewModel
{
    [Required(ErrorMessage = "نام محصول الزامیست")]
    public string? ProductName { get; set; }
    
    [Required(ErrorMessage = "قیمت محصول الزامیست")]
    public int Price { get; set; }
    
    [Required(ErrorMessage = "تعداد محصول الزامیست")]
    public int Count { get; set; }
    
    [Required(ErrorMessage = "صحت موجودی الزامیست")]
    public bool IsEnough { get; set; }
    
    [Required(ErrorMessage = "دسته بندی محصول الزامیست")]
    public Category Category { get; set; }
    
    [DataType(DataType.Upload)]
    
    [Required(ErrorMessage = "عکس محصول الزامیست")]
    public IFormFile File { get; set; }
    public string? Message { get; set; }
    public bool IsCompleted { get; set; }

}