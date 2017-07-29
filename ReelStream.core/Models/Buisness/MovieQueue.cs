using ReelStream.data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelStream.core.Models.Buisness
{
    public class MovieQueue
    {
        public string Name { get; set; }
        public List<Movie> Movies { get; set; }
        
    }
}
