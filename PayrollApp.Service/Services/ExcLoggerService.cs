using PayrollApp.Core.Data.System;
using PayrollApp.Core.Data.ViewModels;
using PayrollApp.Repository;
using PayrollApp.Service.IServices;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollApp.Service.Services
{
    public class ExcLoggerService : IExcLoggerService, IDisposable
    {
        #region Variables

        private readonly IRepository<ExcLogger> _exceptionLoggerRepository;
        int response;

        #endregion

        #region _ctor

        public ExcLoggerService(IRepository<ExcLogger> exceptionLoggerRepository)
        {
            _exceptionLoggerRepository = exceptionLoggerRepository;
        }

        #endregion

        #region Self Dispose

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #region CRUD

        public async Task<PagedData<ExcLogger>> Get(SearchDataTable search)
        {
            PagedData<ExcLogger> pageData = new PagedData<ExcLogger>();

            var query = _exceptionLoggerRepository.Table;

            query = query.Where(x => x.IsDelete == search.IsDelete);

            if (!string.IsNullOrEmpty(search.SearchValue))
            {
                long LoggerID = 0, tempLoggerID = 0;
                DateTime? Created = null; DateTime tempCreated;

                if (long.TryParse(search.SearchValue, out tempLoggerID))
                    LoggerID = Convert.ToInt64(search.SearchValue);
                else
                    if (DateTime.TryParse(search.SearchValue, out tempCreated))
                        Created = Convert.ToDateTime(search.SearchValue);


                query = query.Where(x => x.ExcLoggerID == LoggerID ||
                    x.Message.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
                    x.Controller.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
                    x.Action.Trim().ToLower().Contains(search.SearchValue.Trim().ToLower()) ||
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
                        case "ExcLoggerID":
                            query = query.OrderBy(x => x.ExcLoggerID);
                            break;

                        case "Message":
                            query = query.OrderBy(x => x.Message);
                            break;

                        case "Controller":
                            query = query.OrderBy(x => x.Controller);
                            break;

                        case "Action":
                            query = query.OrderBy(x => x.Action);
                            break;

                        default:
                            query = query.OrderBy(x => x.ExcLoggerID);
                            break;
                    }
                }
                else
                {
                    switch (search.SortColumn)
                    {
                        case "ExcLoggerID":
                            query = query.OrderByDescending(x => x.ExcLoggerID);
                            break;

                        case "Message":
                            query = query.OrderByDescending(x => x.Message);
                            break;

                        case "Controller":
                            query = query.OrderByDescending(x => x.Controller);
                            break;

                        case "Action":
                            query = query.OrderByDescending(x => x.Action);
                            break;


                        default:
                            query = query.OrderByDescending(x => x.ExcLoggerID);
                            break;
                    }
                }

            }

            pageData.Count = await query.CountAsync();

            query = query.Skip(search.Skip).Take(search.PageSize);

            pageData.Items = await query.ToListAsync();

            return pageData;
        }

        public async Task<ExcLogger> GetByID(long LoggerID)
        {
            var query = await _exceptionLoggerRepository.GetByIdAsync(LoggerID); ;
            return query;
        }

        public async Task<string> Create(ExcLogger ExceptionLogger)
        {
            response = await _exceptionLoggerRepository.InsertAsync(ExceptionLogger);
            if (response == 1)
                return ExceptionLogger.ExcLoggerID.ToString();
            else
                return response.ToString();
        }

        public async Task<string> Update(ExcLogger ExceptionLogger)
        {
            response = await _exceptionLoggerRepository.UpdateAsync(ExceptionLogger);
            if (response == 1)
                return ExceptionLogger.ExcLoggerID.ToString();
            else
                return response.ToString();
        }

        #endregion
    }
}
