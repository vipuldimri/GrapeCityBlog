using AutoMapper;
using AutoMapper.Configuration;
using Contracts;
using Entities.Constants;
using Entities.DTO.CreateDTO;
using Entities.DTO.OutputDTO;
using Entities.DTO.UpdateDTO;
using Entities.Error;
using Entities.Models;
using Entities.RequestParamter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GrapeCityBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogRepository _repository;
        private readonly IMapper _mapper;

        public BlogsController(IBlogRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        // Get
        // api/Blogs
        /// <summary>
        /// Get Blogs 
        /// <returns>A response contains Blogs list</returns>
        /// <response code="200">If everyting is fine return Blogs list</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBlogs([FromQuery] BlogParameters blogParameters)
        {
            var dbblogs = await _repository.GetAllBlogs(blogParameters ,  false);
            var blogdtos = _mapper.Map<IEnumerable<BlogOutputDTO>>(dbblogs);
            return Ok(blogdtos);
        }


        // Get
        // api/Blogs/{blogId}
        /// <summary>
        /// Get Blog 
        /// <returns>A response contains perticular Blog</returns>
        /// <response code="200">If everyting is fine return Blog with provided Id</response>
        /// <response code="500">If there was an internal server error</response>
        /// <response code="404">Blog with supplied id does not exist</response>
        [HttpGet("{blogId}", Name = "BlogById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBlogsById(int blogId)
        {
            var dbblog = await _repository.GetBlog(blogId , false);
            if (dbblog == null){
                return NotFound(new ErrorDetails((int)HttpStatusCode.NotFound, $"Blog with id {blogId} does not exits."));
            }
            var blogdto = _mapper.Map<BlogOutputDTO>(dbblog);
            return Ok(blogdto);
        }


        // Post
        // api/Blogs/
        /// <summary>
        /// Post Blog 
        /// <returns>A response contains newly created blog</returns>
        /// <response code="201">If everyting is fine return newly created blog</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateBlog([FromBody] BlogForCreationDTO blogForCreationDTO)
        {
            Blog blog = _mapper.Map<Blog>(blogForCreationDTO);
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier).Value;
            blog.UserId = userId;
            var dbblog = _repository.CreateBlog(blog, false);
            _repository.SaveChanges();
            var blogdto = _mapper.Map<BlogOutputDTO>(dbblog);
            return CreatedAtRoute("BlogById", new { blogId = dbblog.BlogId }, blogdto);
        }


        // Delete
        // api/Blogs/
        /// <summary>
        /// Delete Blog 
        /// <returns>A response with NoContent</returns>
        /// <response code="204">If everyting is fine delete blog with provided id</response>
        /// <response code="500">If there was an internal server error</response>
        /// <response code="404">Blog with supplied id does not exist</response>

        [HttpDelete("{blogId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteBlog(int blogId)
        {
            var dbblog = await _repository.GetBlog(blogId, false);
            if (dbblog == null)
            {
                return NotFound(new ErrorDetails((int)HttpStatusCode.NotFound, $"Blog with id {blogId} does not exits."));
            }
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId != dbblog.UserId)
            {
                ModelState.AddModelError("Delete not allowed.", $"You can only delete your own Blogs.");
                return BadRequest(ModelState);
            }
            _repository.DeleteBlog(dbblog, false);
            _repository.SaveChanges();
            return NoContent();
        }

        // PUT
        // api/Blogs/
        /// <summary>
        /// PUT Blog 
        /// <returns>A response with NoContent</returns>
        /// <response code="204">If everyting is fine delete blog with provided id</response>
        /// <response code="500">If there was an internal server error</response>
        /// <response code="404">Blog with supplied id does not exist</response>

        [HttpPut("{blogId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBlog(int blogId , [FromBody] BlogForUpdationDTO blogForUpdationDTO)
        {
            var dbblog = await _repository.GetBlog(blogId, true);
            if (dbblog == null)
            {
                return NotFound(new ErrorDetails((int)HttpStatusCode.NotFound, $"Blog with id {blogId} does not exits."));
            }
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId != dbblog.UserId){
                ModelState.AddModelError("Edit not allowed.", $"You can only edit your own Blogs.");
                return BadRequest(ModelState);
            }
            dbblog.Title = blogForUpdationDTO.Title;
            dbblog.Text = blogForUpdationDTO.Text;
            _repository.SaveChanges();
            return NoContent();
        }

    }
}
