using Models;
using Models.Models;
using QRCoder;
using Services.ViewModels;
using System.Drawing;
using System.Drawing.Imaging;

namespace Services.Repository
{
    public static class BitmapExtension
    {
        public static byte[] ConvertBitMapToByteArray(this Bitmap bitmap)
        {
            using (MemoryStream ms=new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();

            }  
        }
    }
    public class LectureDetailsService : ILectureDetailsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment env;
        public LectureDetailsService(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }
        public byte[] Make_Lecture(LectureViewModel Lec)
        {
            if (Lec != null)
            {

                Lecture lecture = new Lecture() { 
                LecNumber=Lec.LecNumber,
                Qr_Duration=Lec.Qr_Duration,
                StartTime=Lec.StartTime,
                EndTime=Lec.StartTime.AddHours(1.40),
                Subject=_context.Subjects.Find(Lec.Subject_Id),
                Doctor= _context.Doctors.Find(Lec.Doctor_Id),      
                };


                using (QRCodeGenerator qrCodeGenerator = new QRCodeGenerator())
                using (QRCodeData qrCodeData = qrCodeGenerator.CreateQrCode(lecture.Id, QRCodeGenerator.ECCLevel.Q))
                using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    //Bitmap qrCodeImage = qrCode.GetGraphic(20);
                    Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, (Bitmap)Bitmap.FromFile(env.WebRootPath + "\\logo.jpg"));
                    byte[] BitmapArray = qrCodeImage.ConvertBitMapToByteArray();
                    //var lec = _context.Lectures.Find(LecId);
                    lecture.Qr_Image = BitmapArray;
                    _context.Lectures.Add(lecture);
                    _context.SaveChanges();
                    return BitmapArray;
                    //string Img = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
                }
            }
            else
                return null;
        }
    }
}
