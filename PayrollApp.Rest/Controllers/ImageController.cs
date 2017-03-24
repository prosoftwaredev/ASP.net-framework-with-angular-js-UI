using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using PayrollApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;

namespace PayrollApp.Rest.Controllers
{
    public class ImageController : ApiController
    {
        private readonly IImageService _imageService;
        string response;

        public ImageController() { }

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        //[Authorize(Images = "1")]
        [HttpPost]
        public async Task<IHttpActionResult> GetImages(FormDataCollection form)
        {
            
            var draw = form.GetValues("draw").FirstOrDefault();
            var start = form.GetValues("start").FirstOrDefault();
            var length = form.GetValues("length").FirstOrDefault();
            var sortColumn = form.GetValues("columns[" + form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = form.GetValues("search[value]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            SearchDataTable search = new SearchDataTable
            {
                Skip = skip,
                PageSize = pageSize,
                SortColumn = sortColumn,
                SortColumnDir = sortColumnDir,
                SearchValue = searchValue,
                RecordsTotal = recordsTotal
            };

            PagedData<Image> pagedData = await _imageService.Get(search);

            if (pagedData != null)
            {
                var data = new { draw = draw, recordsFiltered = pagedData.Count, recordsTotal = pagedData.Count, data = pagedData.Items.Select(x => new { x.ImageID, x.ImageName, x.Created, x.IsEnable }) };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

        //[Authorize(Images = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetImageByID(int ImageID)
        {
            if (ImageID <= 0)
                return NotFound();

            Image Image = await _imageService.GetByID(ImageID);

            if (Image != null)
            {
                var data = new { Image.ImageID, Image.ImageName, Image.Created, Image.IsEnable, Image.LastUpdated, Image.Remark, Image.SortOrder };
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

        //[Authorize(Roles = "1")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllImages(bool isDisplayAll)
        {
            List<Image> ImageList = await _imageService.GetAllImages();

            if (ImageList != null)
            {
                ImageList = ImageList.OrderBy(x => x.ImageName).ToList();
                var data = ImageList.Select(x => new { x.ImageID, x.ImageName });
                return Ok(data);
            }
            else
            {
                return InternalServerError();
            }
        }

      
    }
}