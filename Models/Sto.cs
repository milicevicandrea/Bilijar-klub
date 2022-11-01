using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WebApplication1.Models
{
    public class Sto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti broj stola.")]
        [Display(Name = "Bilijarski sto")]
        public string BilijarskiSto { get; set; }


        [Required(ErrorMessage = "Obavezno je uneti datum.")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "01/11/2022", "31/12/2023", ErrorMessage = "Morate uneti validan datum.")]
        public DateTime Datum { get; set; }


        [Required(ErrorMessage = "Obavezno je uneti vreme.")]
        [DataType(DataType.Time)]
        public string Vreme { get; set; }
    }
}