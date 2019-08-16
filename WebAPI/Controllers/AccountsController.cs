using System;
using System.Collections.Generic;
using System.Web.Http;
using BLL.Interface.Interfaces;
using DependencyResolver;
using WebAPI.Infrastructure.Mappers;
using WebAPI.Models;
using Ninject;

namespace WebAPI.Controllers
{
    public class AccountsController : ApiController
    {
        private readonly IAccountService service;
        private readonly IKernel resolver;

        public AccountsController()
        {
            resolver = new StandardKernel();
            resolver.ConfigureResolver();
            service = resolver.Get<IAccountService>();
        }

        [HttpGet]
        public IHttpActionResult Get()
        {            
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]AccountModel accountData)
        { 
            if (accountData is null || accountData.Login is null)
            {
                return Ok();
            }

            return Ok(service.CheckAccountData(accountData.ToBLL()));
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody]AccountModel accountData)
        {
            var id = service.Create(accountData.ToBLL());
            return Ok(id);
            string location = $"api/accounts/{id}";
            var createdAccount = service.GetAccount(id);
            //return Created(location, createdAccount);
        }
    }
}
