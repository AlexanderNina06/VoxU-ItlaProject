using Microsoft.AspNetCore.Mvc;

namespace VoxU_Backend.Helpers
{
    public static class ImageProcess
    {
        public static byte[] ImageConverter(IFormFile Picture)
        {
             var memoryStream = new MemoryStream();
             Picture.CopyTo(memoryStream);
             return memoryStream.ToArray();
        }

        //public static FileResult ByteToFile(byte[] Data)
        //{
        //    FileResult file = new(); (Data, "image/jpg");
        //    file.
        //}

    }
}
