using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagment.Data.Model
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
    }
}
