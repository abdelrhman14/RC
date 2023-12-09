using Dd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.IO;

namespace Dd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public JsonResult GetProjectPaths(string selectedFolderPath, FolderViewModel model)
        {
            string rootDirectory = selectedFolderPath.Split('\\')[0];
            string sourFolderPath = model.SourFolderPath;
            string desFolderPath = model.DesFolderPath;
            return Json(new { rootDirectory = rootDirectory, sourFolderPath = sourFolderPath, desFolderPath = desFolderPath });
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult SelectFolder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SelectFolder(string selectedFolderPath,FolderViewModel model)
        {
            string sourFolderPath = model.SourFolderPath;
            string desFolderPath = model.DesFolderPath;
            string rootDirectory = selectedFolderPath.Split('\\')[0];

            string action = "robocopy";
            string cmd = "/e /move";
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            string command = action + " " + sourFolderPath + " " + desFolderPath + " " + cmd ;

            try
            {
                if (command != null)
                {
                    process.StandardInput.WriteLine(command);
                    process.StandardInput.Flush();
                    process.StandardInput.Close();

                    process.WaitForExit();
                }
                else
                {
                    Console.WriteLine("Command Not Found !");
                }
            }
            catch (Exception ex)
            {


                Console.WriteLine(ex.Message);


            }


           
            return View();


        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}