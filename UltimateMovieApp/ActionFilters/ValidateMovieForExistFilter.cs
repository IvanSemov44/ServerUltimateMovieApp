using Constracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UltimateMovieApp.ActionFilters
{
    public class ValidateMovieForExistFilter : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;

        public ValidateMovieForExistFilter(IRepositoryManager repository)
        {
            _repository = repository;
        }



        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;
            var movieId = (Guid)context.ActionArguments["id"];
            var movie = await _repository.Movie.GetMovieByIdsAsync(movieId, trackChanges);

            if (movie == null)
            {
                context.Result = new NotFoundResult();
                return;
            }
            else
            {
                context.HttpContext.Items.Add("movie", movie);
                await next();
            }
        }
    }
}
