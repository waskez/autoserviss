using System;
using System.Collections.Generic;
using System.Text;

namespace AutoServiss.Application.Entities
{
    public class ServisaLapa
    {
        public int Id { get; set; }
        public DateTime IevietosanasDatums { get; set; }
        public DateTime? ApmaksasDatums { get; set; }
        public decimal KopejaSumma { get; set; }
        public string Piezimes { get; set; }
    }
}