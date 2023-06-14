using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Models;
using QRCoder;
using Services.Areas.External.ViewModels;
using Services.Repository;
using Services.ViewModels;
using System.Drawing;
using System.Security.Claims;

namespace Services.Areas.External.Controllers
{
    [Area("External")]
    public class LectureController : Controller
    {
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment env;
        public LectureController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ApplicationDbContext context, IWebHostEnvironment env)
        {
            _SignInManager = signInManager;
            _userManager = userManager;
            this._context = context;
            this.env = env;
        }
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existuser=await   _userManager.FindByIdAsync(userId);
            ViewBag.name = existuser.Name;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> QrGeneration()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var doctorId = _context.Doctors.FirstOrDefault(x => x.UserId == userId).Id;
            ViewBag.id = doctorId;
            var subjects = _context.DoctorSubjects.Include(x=>x.Subject)
                .Where(x => x.DoctorsId == doctorId)
                .Select(c=> new Services.Areas.External.ViewModels.SubjectViewModel {Id=c.SubjectsId ,Name=c.Subject.Name})
                .ToList();
            LectureWebViewModel model = new LectureWebViewModel { Subjects=new SelectList(subjects, "Id", "Name") };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> QrGeneration(LectureWebViewModel model)
        {
            if (model != null)
            {

                Lecture lecture = new Lecture()
                {
                    LecNumber = model.LecNumber,
                    StartTime = model.StartTime,
                    EndTime = model.StartTime.AddHours(1.40),
                    SubjectId=model.Subject_Id,
                    DoctorId=model.Doctor_Id,
                };
                model.Qr_Duration = model.Qr_Duration == 0 || model.Qr_Duration > 0 ? 1 : model.Qr_Duration;
                Qrs_Code qr = new Qrs_Code() { LectureId = lecture.Id, Qr_Duration = model.Qr_Duration, Status = true };

                using (QRCodeGenerator qrCodeGenerator = new QRCodeGenerator())
                using (QRCodeData qrCodeData = qrCodeGenerator.CreateQrCode(lecture.Id + " "+ qr.Id, QRCodeGenerator.ECCLevel.Q))
                using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    //Bitmap qrCodeImage = qrCode.GetGraphic(20);
                    Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.CadetBlue, Color.White, (Bitmap)Bitmap.FromFile(env.WebRootPath + "\\logo.jpg"));
                    byte[] BitmapArray = qrCodeImage.ConvertBitMapToByteArray();
                    //var lec = _context.Lectures.Find(LecId);
                    //lecture.Qr_Image = BitmapArray;
                    qr.Qr_Image = BitmapArray;
                    _context.Lectures.Add(lecture);
                    _context.Qrs_Code.Add(qr);
                    _context.SaveChanges();
                    return View("RandomQr", new QrRandamViewModel {LecId=lecture.Id,QrImage=qr.Qr_Image,Duration=model.Qr_Duration,Qr_Id=qr.Id });
                    //string Img = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
                }
            }
            else
                return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RandomQr(QrRandamViewModel model)
        {
            return View(model);
        }

        public async Task<IActionResult> GenerateRandomQr(string LecId)
        {
            var qrs_codes=_context.Qrs_Code.Where(x => x.LectureId == LecId).ToList();

            foreach (var item in qrs_codes)
            {
                item.Status = false;
            }
            _context.SaveChanges();

            Qrs_Code qr = new Qrs_Code() { LectureId = LecId, Status = true };
            using (QRCodeGenerator qrCodeGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrCodeGenerator.CreateQrCode(LecId+" "+qr.Id, QRCodeGenerator.ECCLevel.Q))
            using (QRCode qrCode = new QRCode(qrCodeData))
            {
                Bitmap qrCodeImage = qrCode.GetGraphic(20,Color.Black, Color.White, (Bitmap)Bitmap.FromFile(env.WebRootPath+"\\logo.jpg"));
                //Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, (Bitmap)Bitmap.FromFile("C:\\myimage.png"));
                byte[] BitmapArray = qrCodeImage.ConvertBitMapToByteArray();
                //var lec = _context.Lectures.Find(LecId);
                //lecture.Qr_Image = BitmapArray;
                qr.Qr_Image = BitmapArray;
                _context.Qrs_Code.Add(qr);
                _context.SaveChanges();
                return Json(new { key = 1, qr_image = BitmapArray });
                //string Img = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
            }
            return null;
        }
        public async Task<IActionResult> InactiveQr(string QrId)
        {
            if(QrId != null)
            {
                var qr = _context.Qrs_Code.FirstOrDefault(f=>f.Id==QrId);
                qr.Status = false;
                _context.Qrs_Code.Update(qr);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
            
    }
}
