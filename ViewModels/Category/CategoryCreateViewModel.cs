﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels.Categories
{
    public class CategoryCreateViewModel
    {
        [Required(ErrorMessage ="Category Name is Required !")]
        public string Name { get; set; }
    }
}
