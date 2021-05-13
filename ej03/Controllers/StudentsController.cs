using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using UPB.Practice3.ej03;

namespace ej03.Controllers
{
    [ApiController]
    [Route("/api/prices")]


    public class StudentController : ControllerBase
    {

        private static IConfiguration _config;

        Book myBook;
        List<Pricing> PriceBook ;
        public int cont { get; set; }

        string[] nombres = { "Camila", "Fernando", "Fabricio", "Jose", "Andrea", "Stephany", "Carlos", "Nicole", "Kiara", "Matilde" };
        string[] apellidos = { "Allende", "Gonzales", "Estenssoro", "Jobs", "Sarmiento", "Duran", "Rosales", "Zambrana", "Andrade", "Burgos" };
        string[] cat = { "SOCCER", "BASKET" };
        public StudentController(IConfiguration config)
        {
            _config = config;

            myBook = new Book()
            {
                Id = 1,
                Name = "MyBook",
                Description = "Libro que utilizare para todos mis precios",
                Products = new List<Pricing>()

            };
            PriceBook = myBook.Products;
        }

        [HttpGet]
        public Book GetStudent()
        {

            string projectTitle = _config.GetSection("Project").GetSection("Title").Value;
            string dbConnection = _config.GetConnectionString("Database");
            //Console.Out.WriteLine($"We Are connecting to ...{dbConnection}");
            Console.WriteLine("========================NOS PIDEN NUEVA LISTA==========================");

            Random r = new Random();
            int est = r.Next(15, 21);
            
            cont = 0;

            for (int i = 0; i < est; i++)
            {
                cont += 1;
                string med = cont >= 100 ? "" + cont : (cont >= 10 ? "0" + cont : "00" + cont);
                Pricing p = new Pricing()
                {
                    Code = cat[r.Next(0, 2)] + "-" + med,
                    Price = r.Next(1, 5001),
                    PromotionPrice = r.Next(0, 2) == 0 ? 0 : r.Next(1, 2001),
                    PricingBookId = 1
                };
                Console.WriteLine($"Precio CodProd: {p.Code} SetPrice: {p.Price} PromotionPrice: {p.PromotionPrice}");
                PriceBook.Add(p);
            }
            return myBook;
        }

       

        [HttpPost]
        public Pricing CreateStudent([FromBody] Pricing price)//, [FromBody] string studentLastName ) 
        {
            PriceBook.Add(price);
            return price;
        }
        [HttpPut]
        public Pricing UpdateStudent([FromBody] Pricing priceToUpdate)
        {
            Pricing foundPrice = PriceBook.Find(quo => ( quo.Code == priceToUpdate.Code));

            foundPrice.Price = priceToUpdate.Price;
            foundPrice.PromotionPrice = priceToUpdate.PromotionPrice;
            return foundPrice;
        }

        [HttpDelete]
        public Pricing DeleteStudent([FromBody] Pricing priceToDelete)
        {
            PriceBook.RemoveAll(price => price.Code == priceToDelete.Code);
            return priceToDelete;
        }
    }
}
