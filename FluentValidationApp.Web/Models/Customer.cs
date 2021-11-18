using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FluentValidationApp.Web.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        [DataType(DataType.Date)]
        public DateTime      BirthDay { get; set; }
        public IList<Address> Addresses { get; set; }
    }
}
