using Microsoft.AspNetCore.Mvc;
using Pronia_BackEnd_Project.Services.Interfaces;

namespace Pronia_BackEnd_Project.ViewComponents
{
    public class BrandViewComponent : ViewComponent
    {
        private readonly IBrandService _brandService;
        public BrandViewComponent(IBrandService brandService)
        {
            _brandService = brandService;

        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View( await _brandService.GetAllAsync()));
        }

    }
}
