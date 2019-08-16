using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BLL.Interface.Interfaces;
using DependencyResolver;
using WebAPI.Infrastructure.Mappers;
using WebAPI.Models;
using Ninject;
using System.Net.Http;
using System.Net;
using System.Web;
using System.IO;

namespace WebAPI.Controllers
{
    public class TasksController : ApiController
    {
        private readonly ITaskService service;
        private readonly IKernel resolver;

        public TasksController()
        {
            resolver = new StandardKernel();
            resolver.ConfigureResolver();
            service = resolver.Get<ITaskService>();
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri]int id)
        {
            return Ok(service.GetAllForUser(id).Select(x => x.ToViewShort()));
        }

        [HttpGet]
        public IHttpActionResult GetById([FromUri]int taskId, [FromUri]int? userId)
        {
            return Ok(service.GetById(taskId).ToViewFull());
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]HttpPostedFileBase upload, [FromBody]string category, [FromBody]string text, [FromBody]int creatorId)
        //public IHttpActionResult Post()
        {
            FullTaskInfo task = new FullTaskInfo()
            {                
                Category = "asfsf",
                CreatingDate = DateTime.Now.Date,
                Text = "asfsf",
                CreatorId = 1,
                IsClosed = false
            };

            var id = service.Create(task.ToBLL());
            string location = $"api/tasks/{id}";
            var createdTask = service.GetById(id);
            return Created(location, createdTask);
        }

        [HttpPut]
        public IHttpActionResult Update([FromBody]FullTaskInfo task)
        {
            service.SaveChanges(task.ToBLL());
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
        }
    }
}