using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace ej03.Controllers
{
    [ApiController]
    [Route("/api/prices")]


    public class StudentController : ControllerBase
    {

        private static IConfiguration _config;

        List<Price> PriceBook ;
        public int cont { get; set; }

        string[] nombres = { "Camila", "Fernando", "Fabricio", "Jose", "Andrea", "Stephany", "Carlos", "Nicole", "Kiara", "Matilde" };
        string[] apellidos = { "Allende", "Gonzales", "Estenssoro", "Jobs", "Sarmiento", "Duran", "Rosales", "Zambrana", "Andrade", "Burgos" };
        string[] cat = { "SOCCER", "BASKET" };
        public StudentController(IConfiguration config)
        {
            _config = config;
            PriceBook = new List<Price>();
        }

        [HttpGet]
        public List<Price> GetStudent()
        {

            string projectTitle = _config.GetSection("Project").GetSection("Title").Value;
            string dbConnection = _config.GetConnectionString("Database");
            Console.Out.WriteLine($"We Are connecting to ...{dbConnection}");


            Random r = new Random();
            int est = r.Next(15, 21);
            
            cont = 0;

            for (int i = 0; i < est; i++)
            {
                cont += 1;
                string med = cont >= 100 ? "" + cont : (cont >= 10 ? "0" + cont : "00" + cont);
                PriceBook.Add(new Price()
                {
                    CodProd = cat[r.Next(0, 2)] + "-" + med,
                    SetPrice = r.Next(1,5001),
                    PromotionPrice = r.Next(0, 2) == 0 ? 0 : r.Next(1,2001)
                });
            }
            return PriceBook;
        }

       

        [HttpPost]
        public Price CreateStudent([FromBody] Price price)//, [FromBody] string studentLastName ) 
        {
            PriceBook.Add(price);
            return price;
        }
        [HttpPut]
        public Price UpdateStudent([FromBody] Price priceToUpdate)
        {
            Price foundPrice = PriceBook.Find(quo => ( quo.CodProd == priceToUpdate.CodProd));

            foundPrice.SetPrice = priceToUpdate.SetPrice;
            foundPrice.PromotionPrice = priceToUpdate.PromotionPrice;
            return foundPrice;
        }

        [HttpDelete]
        public Price DeleteStudent([FromBody] Price priceToDelete)
        {
            PriceBook.RemoveAll(price => price.CodProd == priceToDelete.CodProd);
            return priceToDelete;
        }
    }
}
