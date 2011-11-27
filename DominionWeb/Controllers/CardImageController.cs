using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls;
using System.Globalization;
using System.IO;
using System.Windows;

namespace DominionWeb.Controllers
{
    public class CardImageController : Controller
    {
        //
        // GET: /CardImage/

        public ActionResult Index(int id, string size)
        {
            DrawingVisual visual = new DrawingVisual();
            DrawingContext ctx = visual.RenderOpen();
            
            FormattedText txt = new FormattedText("45", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Verdana"), 100, Brushes.Red);
            ctx.DrawRectangle(Brushes.White, new Pen(Brushes.White, 10), new System.Windows.Rect(0, 0, 300, 200));
            ctx.DrawText(txt, new System.Windows.Point((300 - txt.Width)/2, 10));
            ctx.Close();

            RenderTargetBitmap bity = new RenderTargetBitmap(300, 300, 96, 96, PixelFormats.Default);
            bity.Render(visual);
            BitmapFrame frame = BitmapFrame.Create(bity);
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(frame);
            MemoryStream ms = new MemoryStream();
            encoder.Save(ms);

            return new FileContentResult(ms.GetBuffer(), "image/jpeg");
        }

    }
}
