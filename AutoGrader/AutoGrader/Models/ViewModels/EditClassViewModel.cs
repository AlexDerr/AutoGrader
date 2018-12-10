using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGrader.Models.ViewModels
{
    public class EditClassViewModel
    {
        [Required]
        public string Name { get; set; }

        public int ClassId { get; set; }
    }
}
