using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiAccountantV2.Models
{
    internal class TaxiFare
    {
        public int FareId { get; set; }
        public Placemark Location { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
    }
}
