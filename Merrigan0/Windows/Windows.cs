using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using Image = System.Drawing.Image;

namespace Merrigan0 {
    public static class Windows {
        public static void InitApp(Func<Form> createTopWindow = null) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ApplicationContext context;
            if (createTopWindow != null) {
                context = new ApplicationContext(createTopWindow());
                Application.Run(context);
            } else {
                Application.Run();
            }
        }

        // Draws the image a single time
        public static void Draw(Graphics g, Image image) {
            foreach (Image child in image.Children) {
                Draw(g, child);
            }
        }
    }

    //using Graphics = System.Drawing.Graphics;

    //public class Windows {
    //    public static LruCache<Argb, Brush> Brushes = new LruCache<Argb, Brush>(a => new SolidBrush(Color.FromArgb(a)));
    //    public static LruCache<Argb, Pen> Pens = new LruCache<Argb, Pen>(a => new Pen(Color.FromArgb(a)), 10);

    //    public static void Draw(Graphics g, Image image) {
    //        Type imageType = image.GetType();
    //        if (imageType == typeof(MonochromeCircleImage)) {
    //            DrawCircle(g, (MonochromeCircleImage)image);
    //            return;
    //        }

    //        ////// The image type didn't have a specific efficient function for it, so render it pixel by pixel
    //        ////int h = image.Region.MinimumBoundingRectangle.H;
    //        ////int w = image.Region.MinimumBoundingRectangle.W;
    //        ////int yBegin = image.Region.MinimumBoundingRectangle.Y;
    //        ////int yEnd = yBegin + h;
    //        ////int xBegin = image.Region.MinimumBoundingRectangle.X;
    //        ////int xEnd = xBegin + w;
    //        ////using (DrawPixelSession session = new DrawPixelSession(g, yBegin, xBegin, h, w)) {
    //        ////    for (int y = yBegin; y < yEnd; ++y) {
    //        ////        int iRow = y * w;
    //        ////        for (int x = xBegin; x < xEnd; ++x) {
    //        ////            if (image.Region.Covers(y, x)) {
    //        ////                session.DrawPixel(y, x, image[y, x]);
    //        ////            }
    //        ////        }
    //        ////    }
    //        ////}
    //    }

    //    public static void DrawCircle(Graphics g, MonochromeCircleImage image) {
    //        float r = image.Evaluate<float>("Radius");
    //        g.FillEllipse(
    //            Brushes[image.Color],
    //            image.Evaluate<float>("CenterX") - r,
    //            image.Evaluate<float>("CenterY") - r,
    //            2f * r,
    //            2f * r);
    //    }

    //    public static Pen Pen(Argb argb) {
    //        Pen pen = Pens[argb];
    //        return pen;
    //    }
    //}
}
