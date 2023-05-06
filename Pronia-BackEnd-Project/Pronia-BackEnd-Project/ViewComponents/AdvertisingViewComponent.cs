using Microsoft.AspNetCore.Mvc;

using Pronia_BackEnd_Project.Services.Interfaces;

namespace Pronia_BackEnd_Project.ViewComponents
{
    public class AdvertisingViewComponent : ViewComponent
    {
        private readonly IAdvertisingService _advertisingService;
        public AdvertisingViewComponent(IAdvertisingService advertisingService)
        {
            _advertisingService = advertisingService;

        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View( await _advertisingService.GetAllAsync()));
        }

    }
}
