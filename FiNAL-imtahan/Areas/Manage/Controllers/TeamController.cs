using FiNAL_imtahan.DAL;
using FiNAL_imtahan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiNAL_imtahan.Areas.Manage.Controllers
{
    [Authorize]
    [Area("Manage")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public TeamController(AppDbContext context,IWebHostEnvironment environment)
        {
           _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View(_context.Teams.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Team team) 
        {

             if(!ModelState.IsValid)return View();
             string path = _environment.WebRootPath + @"\Upload\";
             string filename = Guid.NewGuid() + team.formFile.FileName;
             using(FileStream fileStream= new FileStream(path+filename, FileMode.Create))
              {
                 team.formFile.CopyTo(fileStream);
              }
             team.ImgUrl = filename;
            _context.Teams.Add(team);
             _context.SaveChanges();

            return RedirectToAction("Index");
        }
      
        public IActionResult Update(int id)
        {
            Team expert = _context.Teams.FirstOrDefault(x => x.Id == id);
            if (expert == null)
            {
                return RedirectToAction("Index");
            }

            return View(expert);
        }

        [HttpPost]
        public IActionResult Update(Team expert)
        {
            Team olddexpert = _context.Teams.FirstOrDefault(x => x.Id == expert.Id);
            if (!ModelState.IsValid) return NotFound();
            if (expert.formFile != null)
            {
                string path = _environment.WebRootPath + @"\Upload\";
                string filename = Guid.NewGuid() + expert.formFile.FileName;
                using (FileStream fileStream = new FileStream(path + filename, FileMode.Create))
                {
                    expert.formFile.CopyTo(fileStream);
                }
                olddexpert.ImgUrl = filename;

            }
            olddexpert.Name = expert.Name;
            olddexpert.Position = expert.Position;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Team expert = _context.Teams.FirstOrDefault(x => x.Id == id);
            if (expert != null)
            {
                string path = _environment.WebRootPath + @"\Upload\" + expert.formFile;
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
                _context.Teams.Remove(expert);
                _context.SaveChanges();

            }
            return RedirectToAction("Index");
        }
    }
}
