using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Entities.RequestParamter;

namespace Repository
{
    public class BlogRepository : IBlogRepository
    {
        ApplicationContext _context;
        public BlogRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Blog CreateBlog(Blog blog, bool trackChanges)
        {
            _context.Blogs.Add(blog);
            return blog;
        }

        public void DeleteBlog(Blog blog, bool trackChanges)
        {
            _context.Blogs.Remove(blog);
        }

        public async Task<IEnumerable<Blog>> GetAllBlogs(BlogParameters blogParameters , bool trackChanges)
        {
               return await            _context.Blogs
                                .Skip((blogParameters.PageNumber - 1) * blogParameters.PageSize)
                                .Take(blogParameters.PageSize)
                                .ToListAsync();
        }

        public async Task<Blog> GetBlog(int blogId, bool trackChanges)
        {
            return await _context.Blogs.SingleOrDefaultAsync(x => x.BlogId == blogId);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
