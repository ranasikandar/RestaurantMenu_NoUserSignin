using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace RestaurantMenu.Helpers
{
    public class ResizeImage
    {

        public byte[] imageToJPEG(Stream strm)
        {
            System.Drawing.Image image1 = System.Drawing.Image.FromStream(strm);
            image1.Save(strm, ImageFormat.Jpeg);

            return ImageToByteArray(image1);
        }
        public System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                return System.Drawing.Image.FromStream(ms);
            }
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }


        /// <summary>
        /// Resizes and rotates an image, keeping the original aspect ratio. Does not dispose the original
        /// Image instance.
        /// </summary>
        /// <param name="image">Image instance</param>
        /// <param name="width">desired width</param>
        /// <param name="height">desired height</param>
        /// <param name="rotateFlipType">desired RotateFlipType</param>
        /// <returns>new resized/rotated Image instance</returns>
        public Image Resize(Image image, int width, int height)//, RotateFlipType rotateFlipType
        {
            // clone the Image instance, since we don't want to resize the original Image instance
            var rotatedImage = image.Clone() as Image;
            //rotatedImage.RotateFlip(rotateFlipType);
            var newSize = CalculateResizedDimensions(rotatedImage, width, height);

            var resizedImage = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format32bppArgb);
            resizedImage.SetResolution(72, 72);

            using (var graphics = Graphics.FromImage(resizedImage))
            {
                // set parameters to create a high-quality thumbnail
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                // use an image attribute in order to remove the black/gray border around image after resize
                // (most obvious on white images), see this post for more information:
                // http://www.codeproject.com/KB/GDI-plus/imgresizoutperfgdiplus.aspx
                using (var attribute = new ImageAttributes())
                {
                    attribute.SetWrapMode(WrapMode.TileFlipXY);

                    // draws the resized image to the bitmap
                    graphics.DrawImage(rotatedImage, new Rectangle(new Point(0, 0), newSize), 0, 0, rotatedImage.Width, rotatedImage.Height, GraphicsUnit.Pixel, attribute);
                }
            }

            return resizedImage;
        }

        /// <summary>
        /// Calculates resized dimensions for an image, preserving the aspect ratio.
        /// </summary>
        /// <param name="image">Image instance</param>
        /// <param name="desiredWidth">desired width</param>
        /// <param name="desiredHeight">desired height</param>
        /// <returns>Size instance with the resized dimensions</returns>
        private static Size CalculateResizedDimensions(Image image, int desiredWidth, int desiredHeight)
        {
            var widthScale = (double)desiredWidth / image.Width;
            var heightScale = (double)desiredHeight / image.Height;

            // scale to whichever ratio is smaller, this works for both scaling up and scaling down
            var scale = widthScale < heightScale ? widthScale : heightScale;

            return new Size
            {
                Width = (int)(scale * image.Width),
                Height = (int)(scale * image.Height)
            };
        }
    }
}