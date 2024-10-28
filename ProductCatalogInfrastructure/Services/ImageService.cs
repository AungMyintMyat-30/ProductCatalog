using Microsoft.AspNetCore.Http;

using ProductCatalogCore.Interfaces;

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ProductCatalogInfrastructure.Services
{
    public class ImageService : IImageService
    {
        private readonly IAppLogger<ImageService> _logger;
        public ImageService(IAppLogger<ImageService> logger)
        {
            _logger = logger;
        }
        public bool Upload(IFormFile file, string name, string folder)
        {
            try
            {
                #region Path Built

                string pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\images\\{folder}\\");

                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }

                #endregion

                string extension = ("." + file.FileName.Split('.')[^1]).ToLower();
                string path = pathBuilt + name + extension;

                FileDelete(path);
                Stream stream = file.OpenReadStream();

                using Bitmap bmpImage = new(stream);
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                Encoder QualityEncoder = Encoder.Quality;

                EncoderParameters myEncoderParameters = new(1);

                EncoderParameter myEncoderParameter = new(QualityEncoder, 50L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                bmpImage.Save(path, jpgEncoder, myEncoderParameters);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
      
        }
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
        private static bool FileDelete(string path)
        {
            FileInfo file = new(path);
            if (file.Exists) //check file exsit or not 
            {
                file.Delete();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
