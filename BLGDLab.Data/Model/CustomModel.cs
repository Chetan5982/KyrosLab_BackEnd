using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLGDLab.Data.Model
{
    public  class CustomModel
    {
        
        public class ManageFile
        {
            public FilePaths filePaths { get; set; }

            private string PathToFiles { get; set; }

            public string ErrorPath { get; set; }

            public ManageFile(IConfiguration configuration, string pathToFiles, string errorPath)
            {
                PathToFiles = pathToFiles;

                ErrorPath = errorPath;

                var serverRootPath = configuration?.GetConnectionString("ServerRootPath");

                filePaths = new FilePaths(serverRootPath,pathToFiles);
            }
        }
    }

    
  
    public enum DiamondAnatomies
    {
        Shape = 1,
        Clarity = 3,
        Cut = 4,
        Color = 2,
        Polish = 5,
        Symm = 6,
        Fluo = 10,
        FluoColor = 11,
        Lab = 15,
        FancyColorIntensity = 8,
        FancyColor = 7,
        FancyColorOvertone = 9,
        PlateType = 14,
        OverTone = 16,
        Tone = 18,
        Zone = 19,
        Tinge = 17,
        GirdleCondition = 12,
        GirdleThin = 14,
        GirdleThick = 13,

    }

}
