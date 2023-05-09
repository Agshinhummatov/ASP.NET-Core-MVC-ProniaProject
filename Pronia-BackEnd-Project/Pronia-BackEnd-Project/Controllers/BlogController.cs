using Microsoft.AspNetCore.Mvc;
using Pronia_BackEnd_Project.Models;
using Pronia_BackEnd_Project.Services;
using Pronia_BackEnd_Project.Services.Interfaces;
using Pronia_BackEnd_Project.ViewModels;

namespace Pronia_BackEnd_Project.Controllers
{
    public class BlogController : Controller
    {

        private readonly IBlogService _blogService;

        private readonly ICategoryService _categoryService;

        private readonly ITagService _tagService;

       private readonly IBannerService _bannerService;



        public BlogController(IBlogService blogService, ICategoryService categoryService, ITagService tagService  ,IBannerService bannerService)
        {
            _blogService = blogService;
            _categoryService = categoryService;
             _tagService = tagService;
            _bannerService = bannerService;

        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Banner> banners = await _bannerService.GetAllAsync();

            IEnumerable<Blog> blogs = await _blogService.GetAllAsync();

            IEnumerable<Category> categories = await _categoryService.GetAllAsync();

            IEnumerable<Tag> tags = await _tagService.GetAllAsync();


            BlogVM model = new()
            {
                Blogs = blogs,
                Categories = categories,
                Tags = tags,
                Banners = banners

            };

            return View(model);


        }



        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            if (id is null) return BadRequest();

            Blog blog = await _blogService.GetFullDataByIdAsync((int)id);

            IEnumerable<Blog> blogAll = await _blogService.GetAllAsync();

            IEnumerable<Banner> banners = await _bannerService.GetAllAsync();


            IEnumerable<Tag> tags = await _tagService.GetAllAsync();


             IEnumerable<Category> categories = await _categoryService.GetAllAsync();

            if (blog is null) return NotFound();

            return View(new BlogDetailVM   // view gonderirik bunlari 
            {

                Id = blog.Id,
                Title = blog.Title,
                Desciption = blog.Desciption,
                AuthorName = blog.Author.Name,
                Images = blog.Images,
                BlogAll = blogAll,
                Tags = tags,
                Categories = categories,
                Created = blog.Created,
                Banners = banners


            });



        }



    }
}
