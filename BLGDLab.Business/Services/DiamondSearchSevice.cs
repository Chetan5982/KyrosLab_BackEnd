using BLGDLab.Business.Helper;
using BLGDLab.Business.IServices;
using BLGDLab.Data;
using BLGDLab.Data.IRepositories;
using BLGDLab.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLGDLab.Business.Services
{
    public class DiamondSearchSevice : IDiamondSearchService
    {
        private readonly IDiamondSearchRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _url;
        public DiamondSearchSevice(IDiamondSearchRepository repository, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this._repository = repository;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _url = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

        }
        public async Task<Dictionary<string, IEnumerable<dynamic>>> GetDiamondFilter(bool isForDataSet, int userId, bool isIncludeOnlyInstockCriteria = false)
        {
            IEnumerable<IEnumerable<dynamic>> data = (await _repository.GetDiamondFilter(isForDataSet, userId, isIncludeOnlyInstockCriteria));
            List<string> stockStatuses = new List<string> { "On Hand", "On Memo", "On Hold" };

            var fields = data.ElementAtOrDefault(0);
            var saleFast = data.ElementAtOrDefault(1);
            var branches = data.ElementAtOrDefault(2);
            var vendors = data.ElementAtOrDefault(5);

            var PhotoPaths = new CustomModel.ManageFile(_configuration, _url + "/Images/DiamondShapes", _url + "../Images/NoImagePic.png");

            IEnumerable<object> stockStatusList = stockStatuses.Select((value, index) => new
            {
                Code = index + 1,
                Value = value,
                IsShape = false,
                KeyForId = "OnMemoInStock",
                PhotoPaths = PhotoPaths
            });

            IEnumerable<object> AllSalefastlist = saleFast.Select((dr, index) => new
            {
                Code = index + 1,
                Value = dr.Field<string>("FieldValue"),
                IsShape = false,
                KeyForId = "SaleFast",
                PhotoPaths = PhotoPaths
            });

            IEnumerable<object> objVendors = vendors.Select((dr, index) => new
            {
                Code = dr.Field<int>("Code"),
                Value = dr.Field<string>("Value"),
                KeyForId = dr.Field<string>("VendorType") + "Vendor",
                MsclData = dr.Field<string>("VendorType"),
                PhotoPaths = "",
                IsShape = false
            });

            var shapeList = DiamondSearchHelper.CreateFilterList(fields, "Shape", DiamondAnatomies.Shape.ToString(), true, PhotoPaths, true);

            var colorList = DiamondSearchHelper.CreateFilterList(fields, "Color", DiamondAnatomies.Color.ToString(), false, PhotoPaths);

            var clarityList = DiamondSearchHelper.CreateFilterList(fields, "Clarity", DiamondAnatomies.Clarity.ToString(), false, PhotoPaths);

            var cutList = DiamondSearchHelper.CreateFilterList(fields, "Cut", DiamondAnatomies.Cut.ToString(), false, PhotoPaths);

            var polishList = DiamondSearchHelper.CreateFilterList(fields, "Polish", DiamondAnatomies.Polish.ToString(), false, PhotoPaths);

            var symmList = DiamondSearchHelper.CreateFilterList(fields, "Symm", DiamondAnatomies.Symm.ToString(), false, PhotoPaths);

            var fluoList = DiamondSearchHelper.CreateFilterList(fields, "Fluo", DiamondAnatomies.Fluo.ToString(), false, PhotoPaths);


            var labList = DiamondSearchHelper.CreateFilterList(fields, "Lab", DiamondAnatomies.Lab.ToString(), false, PhotoPaths);

            var fancyColorIntensityList = DiamondSearchHelper.CreateFilterList(fields, "FancyColorIntensity", DiamondAnatomies.FancyColorIntensity.ToString(), false, PhotoPaths);


            var fancyColorOverToneList = DiamondSearchHelper.CreateFilterList(fields, "FancyColorIntensity", DiamondAnatomies.FancyColorOvertone.ToString(), false, PhotoPaths);

            var fancyColorList = DiamondSearchHelper.CreateFilterList(fields, "FancyColor", DiamondAnatomies.FancyColor.ToString(), false, PhotoPaths);
            var tingeList = DiamondSearchHelper.CreateFilterList(fields, "Tinge", DiamondAnatomies.Tinge.ToString(), false, PhotoPaths);

            var sizeGroupList = DiamondSearchHelper.CreateFilterList(fields, "SizeGroup", "SizeGroup", false, PhotoPaths); ;

            return new Dictionary<string, IEnumerable<dynamic>>
            {
                ["stock"] = stockStatusList,
                ["saleFast"] = AllSalefastlist,
                ["vendors"] = objVendors,
                ["branches"] = branches,
                ["shapes"] = shapeList,
                ["colors"] = colorList,
                ["clarities"] = clarityList,
                ["cuts"] = cutList,
                ["polishs"] = polishList,
                ["symms"] = symmList,
                ["fluos"] = fluoList,
                ["labs"] = labList,
                ["fancyColorIntensities"] = fancyColorIntensityList,
                ["fancyColorOverTones"] = fancyColorOverToneList,
                ["fancyColors"] = fancyColorList,
                ["tingles"] = tingeList,
                ["sizeGroups"] = sizeGroupList
            };
        }
    }
}
