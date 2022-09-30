using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesClassLibrary.Models
{
    public class Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EntityType Type { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
