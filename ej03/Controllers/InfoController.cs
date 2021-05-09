using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ej03.Controllers
{
    [Route("/api/info")]
    public class InfoController : Controller
    {

        private static IConfiguration _config;

        public InfoController(IConfiguration config)
        {
            _config = config;
        }

        // GET: api/<controller>
        [HttpGet]
        public string GetInfo()
        {
            string projectTitle = _config.GetSection("Project").GetSection("Title").Value;
            string envName = _config.GetSection("EnvironmentName").Value;

            string dbConnection = _config.GetConnectionString("Database");
            Console.Out.WriteLine($"We Are connecting to ...{dbConnection}");

            return $"Project Title: {projectTitle} || Environment Name: {envName}";
        }

       
    }
}
