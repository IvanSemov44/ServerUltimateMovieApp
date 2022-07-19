using Constracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UltimateMovieApp.ActionFilters
{
    public class ValidateCompanyExistAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;

        public ValidateCompanyExistAttribute(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
            var id= (Guid)context.ActionArguments["id"];
            var company = await _repository.Company.GetCompanyAsync(id, trackChanges);

            if (company == null)
            {
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("company", company);
                await next();
            }
        }
    }
}
