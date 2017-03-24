using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using PayrollApp.Repository;
using PayrollApp.Service.Helper;
using PayrollApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollApp.Service.Services
{
    public class ImageService : IImageService, IDisposable
    {

        #region Variables

        private readonly IRepository<Image> _imageRepository;

        int response;

        #endregion

        #region _ctor

        public ImageService(IRepository<Image> imageRepository)
        {
            _imageRepository = imageRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD
        public async Task<PagedData<Image>> Get(SearchDataTable search)
        {
            PagedData<Image> pageData = new PagedData<Image>();

            var query = _imageRepository.Table;

            query = query.Where(x => x.IsDelete == search.IsDelete);

            if (!string.IsNullOrEmpty(search.SearchValue))
            {
                long ImageID = 0, tempImageID = 0;
                bool? isEnable = null;
                DateTime? Created = null; DateTime tempCreated;

                if (long.TryParse(search.SearchValue, out tempImageID))
                    ImageID = Convert.ToInt64(search.SearchValue);
                else
                    if (DateTime.TryParse(search.SearchValue, out tempCreated))
                        Created = Convert.ToDateTime(search.SearchValue);
                    else
                        if (search.SearchValue.ToLower() == "yes")
                            isEnable = true;
                        else
                            if (search.SearchValue.ToLower() == "no")
                                isEnable = false;

                query = query.Where(x => x.ImageID == ImageID ||
                    x.ImageName.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
                    x.IsEnable == isEnable ||
                    x.Created.Value.Day == Created.Value.Day &&
                    x.Created.Value.Month == Created.Value.Month &&
                    x.Created.Value.Year == Created.Value.Year);
            }

            if (!(string.IsNullOrEmpty(search.SortColumn) && string.IsNullOrEmpty(search.SortColumnDir)))
            {
                //query = query.OrderBy(search.SortColumn + " " + search.SortColumnDir);

                string dir = search.SortColumnDir;

                if (dir == "asc")
                {
                    switch (search.SortColumn)
                    {
                        case "ImageID":
                            query = query.OrderBy(x => x.ImageID);
                            break;

                        case "ImageName":
                            query = query.OrderBy(x => x.ImageName);
                            break;

                        default:
                            query = query.OrderBy(x => x.ImageID);
                            break;
                    }
                }
                else
                {
                    switch (search.SortColumn)
                    {
                        case "ImageID":
                            query = query.OrderByDescending(x => x.ImageID);
                            break;

                        case "ImageName":
                            query = query.OrderByDescending(x => x.ImageName);
                            break;

                        default:
                            query = query.OrderByDescending(x => x.ImageID);
                            break;
                    }
                }

            }

            pageData.Count = await query.CountAsync();

            query = query.Skip(search.Skip).Take(search.PageSize);

            pageData.Items = await query.ToListAsync();

            return pageData;
        }

        public async Task<Image> GetByID(long ImageID)
        {
            var query = await _imageRepository.GetByIdAsync(ImageID); ;
            return query;
        }
        #endregion

        #region Extra

        public async Task<List<Image>> GetAllImages(bool displayAll = false, bool isDelete = false)
        {
            List<Image> ImageList = new List<Image>();

            var query = _imageRepository.Table;

            query = query.Where(x => x.IsDelete == isDelete);

            if (displayAll)
                ImageList = await query.ToListAsync();
            else
                ImageList = await query.Where(x => x.IsEnable == true).ToListAsync();

            return ImageList;
        }

        #endregion
    }
}
