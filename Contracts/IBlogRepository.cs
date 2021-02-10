using Entities.Models;
using Entities.RequestParamter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBlogRepository
    {
        Task<IEnumerable<Blog>> GetAllBlogs(BlogParameters blogParameters , bool trackChanges);
        Task<Blog> GetBlog(int blogId, bool trackChanges);
        Blog CreateBlog(Blog blog, bool trackChanges);
        void DeleteBlog(Blog blog, bool trackChanges);

        void SaveChanges();
    }
}
