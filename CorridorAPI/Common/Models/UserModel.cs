﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Common.Models
{
    public class UserModel
    {
        [Required]
        //[Display(Name = "User name")]
        public string UserName { get; set; }
        [Required]
        [StringLength(100,ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        //[Display(Name = "Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        //[Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string firstname { get; set; }
        [Required]
        public string lastname { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string mobile { get; set; }
        [Required]
        public bool isAdmin { get; set; }
        [Required]
        public string roomNr { get; set; }
    }
}