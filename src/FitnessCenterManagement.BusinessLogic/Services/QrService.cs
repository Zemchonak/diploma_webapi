using System.Drawing;
using FitnessCenterManagement.BusinessLogic.Interfaces;
using QRCoder;

namespace FitnessCenterManagement.BusinessLogic.Services
{
    public class QrService : IQrService
    {
        public Bitmap CreateQrCode(string text)
        {
            using (QRCodeGenerator qrcodeGenerator = new QRCodeGenerator())
            {
                QRCodeData qrcodeData = qrcodeGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                using (QRCode qrcode = new QRCode(qrcodeData))
                {
                    return qrcode.GetGraphic(20);
                }
            }
        }
    }
}
