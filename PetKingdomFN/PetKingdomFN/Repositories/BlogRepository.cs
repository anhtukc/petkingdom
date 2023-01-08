using Microsoft.EntityFrameworkCore;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Interfaces;
using PetKingdomFN.Models;
using System.Linq.Dynamic.Core;
using PetKingdomFN.Helpers;
using static NuGet.Packaging.PackagingConstants;

namespace PetKingdomFN.Repositories
{
    public class BlogRepository : IBlogRepository
    {

        private readonly PetKingdomContext _DbContext;
        private readonly ICloudStorageService _cloud;
        private readonly string folder = "img/blog/";

        public BlogRepository(PetKingdomContext DBContext, ICloudStorageService cloud)
        {
            _DbContext = DBContext;
            _cloud = cloud;
        }
        public async Task<DataList<Blog>> GetPageList(Pagination page)
        {
            DataList<Blog> result = new DataList<Blog>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;
            List<Blog> allData = await _DbContext.Blogs
                .Join(_DbContext.BlogCategories,
                        x=>x.BlogCategoryId,
                        y=>y.Id,
                        (x,y)=>new Blog
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Author = x.Author,
                            Content = x.Content,
                            Thumbnail = x.Thumbnail,
                            CreatedDate = x.CreatedDate,
                            UpdateDate = x.UpdateDate,
                            BlogCategoryId = x.BlogCategoryId,
                            BlogCategoryName = y.Name
                        }).OrderBy(sortQuery).ToListAsync();
            result.numberOfRecords = allData.Count();
            result.list = allData.Skip((page.currentPage - 1) * page.pageSize)
                .Take(page.pageSize)
                .ToList();
            return result;
        }

        public async Task<DataList<Blog>> SearchBlog(Pagination page, basedSearchObject searchObj)
        {
            DataList<Blog> result = new DataList<Blog>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;

            List<Blog> allData = await _DbContext.Blogs.Join(_DbContext.BlogCategories,
                        x => x.BlogCategoryId,
                        y => y.Id,
                        (x, y) => new Blog
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Author = x.Author,
                            Content = x.Content,
                            Thumbnail = x.Thumbnail,
                            CreatedDate = x.CreatedDate,
                            UpdateDate = x.UpdateDate,
                            BlogCategoryId = x.BlogCategoryId,
                            BlogCategoryName = y.Name
                        })
                .Where(x => (string.IsNullOrEmpty(searchObj.name) || x.Name.Contains(searchObj.name))
                && (searchObj.status == -1 || x.Status == searchObj.status)).OrderBy(sortQuery)
                .ToListAsync();
            if (allData.Count() > 0)
            {
                result.numberOfRecords = allData.Count();

                result.list = allData.Skip((page.currentPage - 1) * page.pageSize)
                    .Take(page.pageSize)
                    .ToList();
            }

            return result;
        }
        public async Task<Blog> AddBlog(Blog blog)
        {

            blog.Id = Guid.NewGuid().ToString();
            blog.CreatedDate = DateTime.Now;
            blog.UpdateDate = DateTime.Now;
            if (!(blog.file is null))
            {
                blog.Thumbnail = await _cloud.UploadFileAsync(blog.file, folder + blog.Id);
            }
            var obj = _DbContext.Blogs.AddAsync(blog);
            await _DbContext.SaveChangesAsync();
            return obj.Result.Entity;
        }
        public async Task<Blog> UpdateBlog(Blog blog)
        {
            if (!(blog.file is null))
            {
                blog.Thumbnail = await _cloud.UploadFileAsync(blog.file, folder + blog.Id);
            }
            blog.UpdateDate = DateTime.Now;
            _DbContext.Entry(blog).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync();
            return blog;
        }
        public async Task<Blog> GetBlogById(string id)
        {
            var obj = await _DbContext.Blogs.Where(x => x.Id == id).FirstAsync();
            return obj;
        }
        public async Task<int> DeleteBlog(string id)
        {
            var Blog = await _DbContext.Blogs.FindAsync(id);
            if (Blog is null)
            {
                return 0;
            }
            _DbContext.Blogs.Remove(Blog);
            await _DbContext.SaveChangesAsync();
            return 1;

        }

    }
}
