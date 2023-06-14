namespace Services.Areas.External.ViewModels
{
    public class QrRandamViewModel
    {
        public string LecId { get; set; }
        public int Duration { get; set; }
        public byte[]? QrImage{ get; set; }
        public string Qr_Id { get; set; }

    }
}
