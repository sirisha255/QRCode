using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing;
using System.Linq;
using System.IO;

namespace QRCodeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRCodeController : ControllerBase
    {
        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms= new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
       
        }

        [HttpGet("GenerateQRCode")]
        public async Task<ActionResult> GenerateQRCode(string QRCodeText)
        {
            QRCodeGenerator _qrCode = new QRCodeGenerator();
            QRCodeData _qrCodeData = _qrCode.CreateQrCode(QRCodeText, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCoder(_qrCodeData);
            Image qrCodeImage = qrCode.GetGraphic(20);

            var bytes = ImageToByteArray(qrCodeImage);
            return File(bytes, "image/bmp");
        }
    }
}
