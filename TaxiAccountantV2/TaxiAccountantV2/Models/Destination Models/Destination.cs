using System;
using System.Collections.Generic;
using System.Text;
using TaxiAccountantV2.Services;

namespace TaxiAccountantV2.Models
{
    internal class Placemark
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public decimal Fare { get; private set; }
    }
}