using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiAccountantV2.Models
{
    internal class Fuel
    {
        public string Type { get; set; }
        public decimal Price { get; set; }
        public decimal PricePerLitre { get; set; }
        public double Litres { get; set; }
    }
}
