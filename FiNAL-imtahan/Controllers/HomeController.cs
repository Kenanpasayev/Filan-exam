
using FiNAL_imtahan.DAL;
using Microsoft.AspNetCore.Mvc;


namespace FiNAL_imtahan.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController( AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Teams.ToList());
        }

    }
}
