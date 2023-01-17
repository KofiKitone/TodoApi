using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Models
{
    public class AddTodoRequest
    {
         public string Title { get; set; } = "";
        public string Activity { get; set; } = "";
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:ddd, dd MMM yyyy}")]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:h:mm tt}")]
        public string Time { get; set; } = "";
        public bool Done { get; set; } = false;
    }
}