using FluentValidationApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationApp.Web.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Eposta { get; set; }
        public int Yasi { get; set; }
        public CraditCard CraditCard { get; set; }

    }
}
