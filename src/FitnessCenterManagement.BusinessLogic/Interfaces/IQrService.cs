using System.Drawing;

namespace FitnessCenterManagement.BusinessLogic.Interfaces
{
    public interface IQrService
    {
        Bitmap CreateQrCode(string text);
    }
}
