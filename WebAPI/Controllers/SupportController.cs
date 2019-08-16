using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Interfaces;
using DependencyResolver;
using WebAPI.Infrastructure.Mappers;
using WebAPI.Models;
using Ninject;

namespace WebAPI.Controllers
{
    public class SupportController : Controller
    {
        private readonly ITaskService service;
        private readonly IKernel resolver;

        public SupportController()
        {
            resolver = new StandardKernel();
            resolver.ConfigureResolver();
            service = resolver.Get<ITaskService>();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Tasks()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserInfo()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserInfoEdit()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Task(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateTask()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTask(HttpPostedFileBase upload, string category, string text, int creatorId)
        {
            FullTaskInfo task = new FullTaskInfo()
            {
                BlobPath = Path.GetFileName(upload.FileName),
                Category = category,
                CreatingDate = DateTime.Now.Date,
                File = upload.InputStream,
                Text = text,
                CreatorId = creatorId,
                IsClosed = false
            };

            service.Create(task.ToBLL());

            return View("Thanks", new ThanksModel {EntityName = "task", AdditionalInfo = "You can find it in your tasks list." });
        }

        [HttpGet]
        public ActionResult TaskDetails(int id)
        {
            return View(new FullTaskInfo { Id = id });
        }

        [NonAction]
        public ActionResult Thanks(ThanksModel model)
        {
            if (model is null)
            {
                model = new ThanksModel() { EntityName = "account", AdditionalInfo = "Welcome to our amazing service!" };
            }
            return View(model);
        }
    }
}
