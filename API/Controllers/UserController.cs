using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PublicTimeAPI.Models;
using PublicTimeAPI.Repository;
using PublicTimeAPI.Services;

namespace PublicTimeAPI.Controllers
{
    public class UserController : ControllerBase<ApplicationDbContext, UserController>
    {
        private IEmailService emailService;

        public UserController(ApplicationDbContext context, ILogger<UserController> logger, IEmailService emailService)
          : base(context, logger)
        {
            this.emailService = emailService;
        }

        [AllowAnonymous]
        [HttpPut]
        public async Task<ActionResult<UserData>> CreateUser(string email)
        {
            var userData = await Context.UserDatas.FirstOrDefaultAsync(o => o.ClientId == Request.GetClientId() || o.Email == email);

            if (userData != null)
            {
                return Forbid();
            }

            Context.UserDatas.Add(userData);
            await Context.SaveChangesAsync();

            return Ok(userData);
        }

        [HttpGet("{userid}")]
        public async Task<ActionResult<UserData>> GetUserData(int userid)
        {
            var userData = await Context.UserDatas.FirstOrDefaultAsync(o => o.ClientId == Request.GetClientId() || o.Id == userid);

            if (userData == null)
            {
                return NotFound();
            }

            return Ok(userData);
        }

    }
}
