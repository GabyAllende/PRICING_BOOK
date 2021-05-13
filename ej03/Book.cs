using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UPB.Practice3.ej03
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Pricing> Products { get; set; }
    }
}
