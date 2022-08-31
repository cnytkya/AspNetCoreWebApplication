using AspNetCoreWebApplication.Entities;

namespace AspNetCoreWebApplication.Controllers
{
    internal class HomePageViewModel
    {
        public HomePageViewModel()
        {
        }

        public List<Slider> Sliders { get; internal set; }
        public List<Product> Products { get; internal set; }
    }
}