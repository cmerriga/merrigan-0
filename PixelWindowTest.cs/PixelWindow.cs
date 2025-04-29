using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Merrigan0 {
    public enum TextAlignment {
        Left,
        Center,
        Right,
        DoubleJustified
    }

    // Draw on it
    // Write text on it
    // Resize it
    // Minimize it
    // Set its transparency
    // Get its children
    public class Window : Control {
        private Argb[,] pixels;
        private PixelGridDrawer gridDrawer;

        //public virtual Display Display { get; }
        public virtual Array2D Pixels { get; }

        public Window(PixelGridDrawer gridDrawer) {
            this.gridDrawer = gridDrawer;
            BackColor = System.Drawing.Color.LimeGreen;
            Paint += HandlePaint;
            Resize += HandleResize;
        }

        public virtual void SetPixel(int x, int y, Argb color) {
        }

        public virtual void Circle(float xCenter, float yCenter, float radius, Argb color) {
        }

        public virtual void CircleLine(float xCenter, float yCenter, float radius, Argb color) {
        }

        public virtual void Display(Display display) {
        }

        //public virtual Display DisplayAt(int x, int y) {
        //}

        //public virtual IList<Display> DisplaysIn(int x, int y, int w, int h) {
        //}

        public virtual void Line(float x1, float y1, float x2, float y2, Argb color) {
        }

        public virtual void AlignedRectangle(int left, int top, int right, int bottom, Argb color) {
        }

        public virtual void AlignedRectangleLine(int left, int top, int right, int bottom, Argb color) {
        }

        public virtual void Polygon(IList<PointF> clockwisePoints, Argb color) {
        }

        public virtual void PolygonLine(IList<PointF> clockwisePoints, Argb color) {
        }

        public virtual void AlignedText(float xBase, float yStart, float yEnd, TextAlignment alignment) {
        }

        public virtual void Text(float xBase, float yStart, float yEnd, TextAlignment alignment 
        public virtual void Triangle(float x1, float y1, float x2, float y2, float x3, float y3, Argb color) {
        }

        public virtual void TriangleLine(float x1, float y1, float x2, float y2, float x3, float y3, Argb color) {
        }


        protected unsafe virtual void HandlePaint(object sender, PaintEventArgs pea) {
            //// Make sure the pixels are here
            //if (pixels == null) {
            //    pixels = new Argb[ClientSize.Height, ClientSize.Width];
            //    gridDrawer.Draw(pixels);
            //}

            //// Make a bitmap out of them and copy them
            ////x1 = (int)Math.Floor(surface.graphics.ClipBounds.Left);
            ////int x2 = (int)Math.Ceiling(surface.graphics.ClipBounds.Right);
            ////y1 = (int)Math.Floor(surface.graphics.ClipBounds.Top);
            ////int y2 = (int)Math.Ceiling(surface.graphics.ClipBounds.Bottom);
            ////h = y2 - y1;
            ////w = x2 - x1;
            //IntPtr pData = Marshal.UnsafeAddrOfPinnedArrayElement(pixels, 0);
            //using (Bitmap bitmap = new Bitmap(ClientSize.Width, ClientSize.Height, ClientSize.Width * Marshal.SizeOf(pixels[0, 0]), PixelFormat.Format32bppPArgb, pData)) {
            //    pea.Graphics.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
            //}
        }

        protected virtual void HandleResize(object sender, EventArgs ea) {
            pixels = null;
            //Invalidate();
        }

        // Do no background painting
        protected override void OnPaintBackground(PaintEventArgs e) { }
    }

    public abstract class PixelGridDrawer {
        public abstract void Draw(Argb[,] pixels);
    }

    public class CustomPixelGridDrawer : PixelGridDrawer {
        private Action<Argb[,]> draw;

        public CustomPixelGridDrawer(Action<Argb[,]> draw) {
            this.draw = draw;
        }

        public override void Draw(Argb[,] pixels) {
            draw(pixels);
        }
    }
}
