using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class ProfileModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Role { get; set; }
        public string Room { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}