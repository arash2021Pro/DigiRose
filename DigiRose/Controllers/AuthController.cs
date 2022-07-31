using System.Diagnostics;
using System.Security.Claims;
using System.Web;
using DigiRose.CoreApplication.CoreManagerApplication;
using DigiRose.CoreBussiness.RepsPattern;
using DigiRose.CoreBussiness.StorageEntity.OTP;
using DigiRose.CoreBussiness.StorageEntity.Users;
using DigiRose.CoreStructure.MessageService;
using DigiRose.Models.Users;
using Hangfire;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace DigiRose.Controllers;

public class AuthController:Controller
{
    public ICoreServiceManager CoreServiceManager;
    public IMapper Mapper;
    public IUnitOfWork Work;
    public IMessageService MessageService;
    private readonly ILogger<AuthController> _logger;
    public AuthController(ICoreServiceManager coreServiceManager, IMapper mapper, ILogger<AuthController> logger, IUnitOfWork work, IMessageService messageService)
    {
        CoreServiceManager = coreServiceManager;
        Mapper = mapper;
        _logger = logger;
        Work = work;
        MessageService = messageService;
    }
    

    [HttpGet]
    public async Task<IActionResult> Signup()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Signup(SignupViewModel model)
    {
       
            if (ModelState.IsValid)
            {
                var result = await CoreServiceManager.UserService.IsPhoneExistsAsync(model.Phonenumber);
                var user = await CoreServiceManager.UserService.GetUserAsync(model.Phonenumber);
                if (result && user.UserStatus != UserStatus.None)
                {
                    model.IsCompleted = false;
                    model.Message = "این شماره قبلا ثبت شده";
                    ModelState.AddModelError(nameof(model.Phonenumber),model.Message);
                    return View(model);
                }
                var code = CoreServiceManager.OtpService.GenerateCode(6);
                var otp = new Otp();
                otp.code = code;
                if (user == null)
                {
                    user = new User();
                    user.RoleId = 4;
                    user.UserStatus = UserStatus.None;
                    Mapper.Map(model, user);
                    await CoreServiceManager.UserService.AddNewUserAsync(user);
                }
                otp.User = user;
                var HostUser = await CoreServiceManager.UserService.SearchUserAsync(model.RefCode);
                HostUser.ReferralCount += 1;
                await CoreServiceManager.OtpService.AddNewOtpAsync(otp);
                var change = await Work.SaveChangesAsync();
                if (change > 0)
                {
                   // BackgroundJob.Enqueue(() => MessageService.SendMessageAsync(user.Phonenumber, "DigiRose", code));
                   return RedirectToAction("Verify",new {phonenumber = HttpUtility.UrlEncode(model.Phonenumber.Replace("-",""))});
                }
            } 
            return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Login()
    {
       
        
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await CoreServiceManager.UserService.GetUserAsync(model.Password,model.Phonenumber); 
                    if (user == null)
                    {
                        ModelState.AddModelError(nameof(user.Phonenumber), "کاربر پیدا نشد ");
                        return View(model);
                    }
                    if (user.UserStatus == UserStatus.Inactive)
                    {
                        ModelState.AddModelError(nameof(user.Phonenumber), "کاربر فعال نیست");
                        return View(model);
                    }
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Phonenumber));
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    if (user.RoleId == 3)
                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                    
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    if (user.Role.Rolename == "admin")
                        return RedirectToAction("Dashboard", "Admin");

                    return RedirectToAction("Index", "Home");
        }
        return View(model);
    }
    [HttpGet]
    public async Task<IActionResult> logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
        return RedirectToAction("Login","Auth");
    }


    [HttpGet]
    public async Task<IActionResult> Verify(string? phonenumber)
    {
        var otp = new VerifyViewModel();
        otp.phonenumber = HttpUtility.UrlDecode(phonenumber.Replace("-",""));
        return View(otp);
    }

    [HttpPost]
    public async Task<IActionResult> Verify(VerifyViewModel? model)
    {
        if (ModelState.IsValid)
        {
                var otp = await CoreServiceManager.OtpService.GetOtpAsync(model.code);
                var user = await CoreServiceManager.UserService.GetUserAsync(otp.UserId);
                if (otp.IsAuthentic)
                {
                    user.UserStatus = UserStatus.Active;
                    otp.IsUsed = true;
                    var row = await Work.SaveChangesAsync();
                    if (row > 0)
                    {
                        model.IsCompleted = true;
                        model.Message = "حساب کاربری شما فعال گردید";
                        return View(model);
                    }
                    model.IsCompleted = false;
                    model.Message = "خطا در پاسخگویی";
                    return View(model);
                }
                model.IsCompleted = false;
                model.Message = "کد وارد شده اعتبار ندارد";
                return View(model);
        }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ForgetPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel? model)
    {
        if (ModelState.IsValid)
        {
            var user = await CoreServiceManager.UserService.GetUserAsync(model.Phonenumber);
            if (user == null)
            {
                model.Message = "کاربر وجود ندارد";
                model.IsCompleted = false;
                ModelState.AddModelError(nameof(model.Phonenumber),model.Message);
                return View(model);
            }
            var code = CoreServiceManager.OtpService.GenerateCode(6);
            var otp = new Otp() {UserId = user.Id, code = code};
            await CoreServiceManager.OtpService.AddNewOtpAsync(otp);
            var change = await Work.SaveChangesAsync();
            if (change > 0)
            {
                //BackgroundJob.Enqueue(() => MessageService.SendMessageAsync(user.Phonenumber, "DigiRose", code));
                return RedirectToAction("VerifyUser", "Auth",new{Phone = HttpUtility.UrlEncode(model.Phonenumber.Replace("-",""))});
            }
        }
        return View(model);
    }
    [HttpGet]
    public async Task<IActionResult> VerifyUser(string ? Phone)
    {
        var Verify = new VerifyUserViewModel() {Phone = HttpUtility.UrlDecode(Phone.Replace("-",""))};
        return View(Verify);
    }
    [HttpPost]
    public async Task<IActionResult> VerifyUser(VerifyUserViewModel? model)
    {
     
            var otp = await CoreServiceManager.OtpService.GetOtpAsync(model.Code);
            if (otp.IsAuthentic)
            {
                otp.IsUsed = true;
                var change = await Work.SaveChangesAsync();
                if (change > 0)
                {
                    model.IsCompleted = true;
                    model.Message = "هویت شما تایید شد";
                    return RedirectToAction("ResetPassword", "Auth",
                        new {Id = otp.UserId, Message = HttpUtility.UrlEncode(model.Message.Replace("-","")), IsCompleted = model.IsCompleted});
                }
            }
            model.IsCompleted = false;
            model.Message = "کد وارد شده اعتبار ندارد";
            return View(model);
            
    }

    
    [HttpGet]
    public async Task<IActionResult> ResetPassword(int Id,string Message,bool IsCompleted)
    {
        var resetpass = new ResetPasswordViewModel() {Id = Id, Message = HttpUtility.UrlDecode(Message.Replace("-","")), IsCompleted = IsCompleted};
        return View(resetpass);
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel? model)
    {
        if (ModelState.IsValid)
        {
            var user = await CoreServiceManager.UserService.GetUserAsync(model.Id);
            user.Password = CoreServiceManager.UserService.GenerateHash(user.Password);
            var change = await Work.SaveChangesAsync();
            if (change > 0)
            {
                model.IsCompleted = true;
                model.IsSuccessful = true;
                model.Message = "رمز ورود شما با موفقیت تغییر یافت";
                return View(model);
            }

            model.IsCompleted = false;
            model.Message = "خطا در پاسخگویی";
            return View(model);
        }
        return View(model);
    }
    
}