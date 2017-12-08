using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Gossip
{
    public class User
    {

        //[Column(TypeName = "VARCHAR")]
        //[MaxLength(100)]
        //[Index]
        [Key]
        public int Id { set; get; }

        public string Username { set; get; }

        [Required]
        [RegularExpression(@"^([A-z]+[.]?( [A-z]+)+)+$", ErrorMessage = "Invalid Name, Ex: Md. Nokibul Amin")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+([._-][a-zA-Z0-9]+)?@[a-zA-Z0-9]{2,50}([.][a-z]{2,5})+$", ErrorMessage = "Inavlid Email, Ex: jhon.doe@xshop.com.bd")]
        public string Email { get; set; }

        public string DateofBirth { set; get; }
        public string OpeningDate { set; get; }
        public int AccountStatusId { set; get; }
        [ForeignKey("AccountStatusId")]
        public AccountStatus AccountStatus { set; get; }

        public int UserRoleId { set; get; }
        [ForeignKey("UserRoleId")]
        public UserRole UserRole { set; get; }

        public string ProfilePicture { set; get; }

    }
}