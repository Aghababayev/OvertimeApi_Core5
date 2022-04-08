using System;
using System.ComponentModel.DataAnnotations;

namespace OvertimeApi.DataAceess
{
    public class Overtime
    {
        [Key]
        public int İD { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
    }
}
