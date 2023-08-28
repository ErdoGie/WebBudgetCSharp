﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Application.WebBudget
{
    public class IncomeCategoryDTO
    {
        [Required(ErrorMessage ="Category name required")]
        public string CategoryName { get; set; } = default!;
    }
}
