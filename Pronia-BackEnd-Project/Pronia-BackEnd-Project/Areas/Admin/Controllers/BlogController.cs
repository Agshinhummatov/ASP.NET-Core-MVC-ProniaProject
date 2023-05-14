using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pronia_BackEnd_Project.Areas.Admin.ViewModels;
using Pronia_BackEnd_Project.Data;
using Pronia_BackEnd_Project.Helpers;
using Pronia_BackEnd_Project.Models;
using Pronia_BackEnd_Project.Services;
using Pronia_BackEnd_Project.Services.Interfaces;
using System.Text.RegularExpressions;

namespace Pronia_BackEnd_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IBlogService _blogService;
        private readonly IAuthorService _authorService;
       
        public BlogController(AppDbContext context, IWebHostEnvironment env, IBlogService blogService,  IAuthorService authorService)
        {
            _context = context;
            _env = env;
            _blogService = blogService;
            _authorService = authorService;
        }

        public async Task<IActionResult> Index(int page = 1, int take = 5)  
        {
            IEnumerable<Blog> blogs = await _blogService.GetPaginatedDatas(page, take); 

            IEnumerable<BlogListVM> mappedDatas = GetMappedDatas(blogs);

            int pageCount = await GetPageCountAsync(take); 

            Paginate<BlogListVM> paginatedDatas = new(mappedDatas, page, pageCount);  

            ViewBag.take = take;

            return View(paginatedDatas);
        }

        //paglerin sayini veren method

        private async Task<int> GetPageCountAsync(int take)
        {
            var productCount = await _blogService.GetCountAsync();  
            return (int)Math.Ceiling((decimal)productCount / take);     
        }

        // pasingation method 
        private IEnumerable<BlogListVM> GetMappedDatas(IEnumerable<Blog> blogs)
        {
            List<BlogListVM> mappedDatas = new();

            foreach (var blog in blogs)
            {
                BlogListVM prodcutVM = new()
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    Description = blog.Desciption,
                    AuthorName = blog.Author.Name,
                    MainImage = blog.Images.Where(m => m.IsMain).FirstOrDefault()?.Image
                };

                mappedDatas.Add(prodcutVM);

            }
            return mappedDatas;
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
            

            ViewBag.authors = await GetAuthorsAsync();

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM model)
        {
            try
            {
                

                ViewBag.author = await GetAuthorsAsync();

                if (!ModelState.IsValid)
                {
                    return View(model); //  is validi yoxlayirki bos olmasin ve view icine bize gelen model  gonderiki eger biri sehv olarsa inputlari bos saxlamasin
                }


                foreach (var photo in model.Photos)
                {

                    if (!photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View();
                    }

                    if (!photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View();
                    }



                }

                List<BlogImage> blogImages = new();  // list yaradiriq  burda hemin listada asqi methoda add edecik imagleri

                foreach (var photo in model.Photos)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName; // Guid.NewGuid() bu neynir bir id kimi dusune birerik hemise ferqli herifler verir mene ki men sekilin name qoyanda o ferqli olsun tostring ele deyirem yeni random oalraq ferlqi ferqli sekil adi gelecek  ve  slider.Photo.FileName; ordan gelen ada birslerdir 


                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", fileName);

                    await FileHelper.SaveFileAsync(path, photo);


                    BlogImage blogImage = new()   // bir bir sekileri goturur forech icinde
                    {
                        Image = fileName
                    };

                    blogImages.Add(blogImage); // yuxardaki  List<ProductImage> add edir sekileri yeni nece dene sekili varsa o qederde add edecek

                }

                blogImages.FirstOrDefault().IsMain = true; // bu neynir elimizdeki list var icinde imagler var gelir onlardan biricsin defaltunu ture edirki productlarda 1 ci sekili gosdersin

              

                Blog newBlog = new()
                {
                    Title = model.Title,       
                    Desciption = model.Description,
                    AuthorId = model.AuthorId,
                    Images = blogImages  
                };

                await _context.BlogImages.AddRangeAsync(blogImages); // AddRangeAsync bu method bize listi yigir add edir 
                await _context.Blogs.AddAsync(newBlog);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }




        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            Blog dbProduct = await _blogService.GetFullDataByIdAsync((int)id);

            if (dbProduct is null) return NotFound();

             ViewBag.author = await GetAuthorsAsync();


            return View(new BlogEditVM
            {
                Title = dbProduct.Title,
                Description = dbProduct.Desciption,
                AuthorId = dbProduct.AuthorId,
                Images = dbProduct.Images
            });


        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, BlogEditVM updatedProduct)
        {
            ViewBag.author = await GetAuthorsAsync();

            if (!ModelState.IsValid) return View(updatedProduct);

            Blog dbProduct = await _blogService.GetFullDataByIdAsync((int)id);

            if (dbProduct is null) return NotFound();

            if (updatedProduct.Photos != null)
            {

                foreach (var photo in updatedProduct.Photos)
                {

                    if (!photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(dbProduct);
                    }

                    if (!photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View(dbProduct);
                    }



                }

                foreach (var item in dbProduct.Images)
                {

                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", item.Image);

                    FileHelper.DeleteFile(path);
                }



                List<BlogImage> productImages = new();  // list yaradiriq  burda hemin listada asqi methoda add edecik imagleri

                foreach (var photo in updatedProduct.Photos)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName; // Guid.NewGuid() bu neynir bir id kimi dusune birerik hemise ferqli herifler verir mene ki men sekilin name qoyanda o ferqli olsun tostring ele deyirem yeni random oalraq ferlqi ferqli sekil adi gelecek  ve  slider.Photo.FileName; ordan gelen ada birslerdir 


                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", fileName);

                    await FileHelper.SaveFileAsync(path, photo);


                    BlogImage productImage = new()   // bir bir sekileri goturur forech icinde
                    {
                        Image = fileName
                    };

                    productImages.Add(productImage); // yuxardaki  List<ProductImage> add edir sekileri yeni nece dene sekili varsa o qederde add edecek

                }

                productImages.FirstOrDefault().IsMain = true;

                dbProduct.Images = productImages;
            }

           

            dbProduct.Title = updatedProduct.Title;
            dbProduct.Desciption = updatedProduct.Description;
           
            dbProduct.AuthorId = updatedProduct.AuthorId;

            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }




        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Blog dbProduct = await _blogService.GetFullDataByIdAsync((int)id);

            ViewBag.desc = Regex.Replace(dbProduct.Desciption, "<.*?>", String.Empty);

            return View(new BlogDetailVM   
            {

                Title = dbProduct.Title,
                Description = dbProduct.Desciption,
                AuthorId = dbProduct.AuthorId,
                Images = dbProduct.Images,
                AuthorName = dbProduct.Author.Name
            });
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
    
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            Blog product = await _blogService.GetFullDataByIdAsync((int)id);

            if (product is null) return NotFound();


            foreach (var item in product.Images)
            {

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", item.Image);

                FileHelper.DeleteFile(path);

            }


            _context.Blogs.Remove(product);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }




        private async Task<SelectList> GetAuthorsAsync()
        {

            IEnumerable<Author> categories = await _authorService.GetAllAsync();

            return new SelectList(categories, "Id", "Name"); 


        }







    }
}
