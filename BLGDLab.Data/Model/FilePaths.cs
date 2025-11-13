using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLGDLab.Data.Model
{
    public class FilePaths
    {
        public string OriginalFilePath_Absolute { get; set; }
        public string ThumbnailFilePath_Absolute { get; set; }
        public string OriginalFilePath_Relative { get; set; }
        public string ThumbnailFilePath_Relative { get; set; }

        public FilePaths(string serverRootPath, string pathToFiles, string fileName = "")
        {
            OriginalFilePath_Absolute = $"{serverRootPath}{pathToFiles}/Originals/{fileName}";
            OriginalFilePath_Relative = $"../{pathToFiles}/Originals/{fileName}";
            ThumbnailFilePath_Absolute = $"{serverRootPath}{pathToFiles}/Thumbnails/{fileName}";
            ThumbnailFilePath_Relative = $"../{pathToFiles}/Thumbnails/{fileName}";
        }

        public FilePaths() { }
    }
}
