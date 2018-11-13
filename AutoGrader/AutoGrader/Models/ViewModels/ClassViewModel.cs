using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGrader.Models.ViewModels
{
    public class ClassViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required, DisplayName("Class Key")]
        public string ClassKey { get; set; }
    }
}
