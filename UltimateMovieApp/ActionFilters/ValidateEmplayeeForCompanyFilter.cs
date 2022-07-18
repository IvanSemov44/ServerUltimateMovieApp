using Constracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UltimateMovieApp.ActionFilters
{
    public class ValidateEmplayeeForCompanyFilter : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;

        public ValidateEmplayeeForCompanyFilter(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;
            var companyId = (Guid)context.ActionArguments["companyId"];
            var company = _repository.Company.GetCompanyAsync(companyId, trackChanges);

            if (company == null)
            {
                context.Result = new NotFoundResult();
                return;
            }


            var id = (Guid)context.ActionArguments["id"];
            var emplayee = _repository.Employee.GetEmployeeAsync(companyId,id, trackChanges);
            if (emplayee == null)
            {
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("employee", emplayee);
                await next();
            }
        }
    }
}
