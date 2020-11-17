using System;
using System.ComponentModel.DataAnnotations;

namespace WebAdvertisment.Model
{
    public class SignUpModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}