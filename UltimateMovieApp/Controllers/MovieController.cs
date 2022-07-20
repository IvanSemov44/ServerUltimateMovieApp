﻿using AutoMapper;
using Constracts;
using Entities.DataTransferObjects.Movie;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using UltimateMovieApp.ActionFilters;

namespace UltimateMovieApp.Controllers
{
    [Route("api/movie")]
    [ApiController]
    public class MovieController : Controller
    {

        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public MovieController(IRepositoryManager repositoryManager, IMapper mapper)
        {
            this._repositoryManager = repositoryManager;
            this._mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _repositoryManager.Movie.GetAllMovieAsync(trackChanges: false);

            var movieDto = _mapper.Map<IEnumerable<MovieDto>>(movies);

            return Ok(movieDto);
        }

        [HttpGet("{id}",Name = "GetMovieById")]
        [ServiceFilter(typeof(ValidateMovieForExistFilter))]
        public IActionResult GetMovieById(Guid id)
        {
            var movie = HttpContext.Items["movie"] as Movie;

            var movieDto = _mapper.Map<MovieDto>(movie);

            return Ok(movieDto);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateMovie([FromBody]MovieForCreateDto movie)
        {
            var movieEntity = _mapper.Map<Movie>(movie);

            _repositoryManager.Movie.CreateMovie(movieEntity);
            await _repositoryManager.SaveAsync();

            var movieForReturn = _mapper.Map<MovieDto>(movieEntity);

            return CreatedAtRoute("GetMovieById", new { id = movieForReturn.Id }, movieForReturn);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateMovieForExistFilter))]
        public async Task<IActionResult> UpdateMovie(Guid id, [FromBody] MovieForUpdateDto movie)
        {
            var movieEntity = HttpContext.Items["movie"] as Movie;

            _mapper.Map(movie, movieEntity);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateMovieForExistFilter))]
        public async Task<IActionResult> DeleteMovie([FromRoute]Guid id)
        {
            var movie = HttpContext.Items["movie"] as Movie;

             _repositoryManager.Movie.DeleteMovie(movie);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }
    }
}