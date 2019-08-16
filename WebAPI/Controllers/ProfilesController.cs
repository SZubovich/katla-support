using System;
using System.Collections.Generic;
using System.Web.Http;
using BLL.Interface.Interfaces;
using DependencyResolver;
using WebAPI.Infrastructure.Mappers;
using WebAPI.Models;
using Ninject;
using System.Net.Http;
using System.Net;

namespace WebAPI.Controllers
{
    public class ProfilesController : ApiController
    {
        private readonly IEmployeeService service;
        private readonly IKernel resolver;

        public ProfilesController()
        {
            resolver = new StandardKernel();
            resolver.ConfigureResolver();
            service = resolver.Get<IEmployeeService>();
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(service.Get());
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri]int id)
        {
            return Ok(service.Get(id).ToView());
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]ProfileModel profile)
        {
            var id = service.Create(profile.ToBLL());
            string location = $"/api/profiles/{id}";
            var createdProfile = service.Get(id);
            return Created(location, createdProfile);
        }

        [HttpPut]
        public IHttpActionResult Update([FromBody]ProfileModel profile)
        {
            service.Update(profile.ToBLL());
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
        }
    }
}