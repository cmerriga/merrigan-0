using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

//namespace Merrigan0 {
    //using Graphics = System.Drawing.Graphics;

    //// For drawing a series of pixels, then transferring them to the screen at the end
    //public class DrawPixelSession : IDisposable {
    //    private bool drawn;
    //    private Graphics g;
    //    private int h;
    //    private Argb[] pixels;
    //    private int stride;
    //    private int w;
    //    private int x;
    //    private int y;

    //    public DrawPixelSession(Graphics g, int y, int x, int h, int w) {
    //        this.g = g;
    //        this.h = h;
    //        pixels = new Argb[h * w];
    //        stride = sizeof(int) * w;
    //        this.x = x;
    //        this.w = w;
    //        this.y = y;
    //    }

    //    public void Dispose() {
    //        if (!drawn) {
    //            unsafe {
    //                fixed (Argb* pPixels = pixels) {
    //                    using (Bitmap bitmap = new Bitmap(w, h, sizeof(int) * w, PixelFormat.Format32bppArgb, (IntPtr)pPixels)) {
    //                        g.DrawImageUnscaled(bitmap, x, y);
    //                    }
    //                }
    //            }
    //            drawn = true;
    //        }
    //    }

    //    public void DrawPixel(int y, int x, Argb argb) {
    //        pixels[y * w + x] = argb;
    //    }
    //}
//}
