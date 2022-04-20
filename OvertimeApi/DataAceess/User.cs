using System.ComponentModel.DataAnnotations;

namespace OvertimeApi.DataAceess
{
    public class User

    {    [Key]
        public int UserId{ get; set; }
        public string UserName{ get; set; }
        
        public string UserPassword{ get; set; }
        public string UserRole { get; set; }        
    }
}
