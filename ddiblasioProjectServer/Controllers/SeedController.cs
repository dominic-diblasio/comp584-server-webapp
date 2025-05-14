using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using champsModel;
using CsvHelper.Configuration;
using System.Globalization;
using champsProjectServer.Data;
using CsvHelper;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.Metrics;

namespace champsProjectServer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SeedController(ChampsDBContext context, IHostEnvironment environment,
        UserManager<ChampsUser> userManager) : ControllerBase
    {
        //string _pathName = Path.Combine(environment.ContentRootPath, "Data/champs.csv");

        [HttpPost("ChampsUsers")]
        public async Task ImportUsersAsync()
        {
            ChampsUser user = new()
            {
                UserName = "user",
                Email = "user@email.com",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult x = await userManager.CreateAsync(user, "Passw0rd!");

            int y = await context.SaveChangesAsync();

        }
    }
}