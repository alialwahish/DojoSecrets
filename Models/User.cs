using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;


namespace DojoSecret
{

    public class User
    {


        [Key]
        [Required]
        public int UserId { get; set; }


        [Required(ErrorMessage = "Field Required")]
        [RegularExpression("^[a-zA-Z ]*$"
    , ErrorMessage = "Letters Only!")]
        [MinLength(2)]

        public string First_Name { get; set; }

        [Required(ErrorMessage = "Field Required")]
        [RegularExpression("^[a-zA-Z ]*$"
    , ErrorMessage = "Letters Only!")]
        [MinLength(2)]
        public string Last_Name { get; set; }

        [Required(ErrorMessage = "Field Required")]
        [EmailAddress]

        public string Email { get; set; }

        [Required(ErrorMessage = "Field Required")]
        [MinLength(8)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password Don't match")]
        public string Confirm_Password { get; set; }


        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }

        public int? PostsId { get; set; }

        
        public int? LikesId {get;set;}
        
        public List<Likes> Likes {get;set;}
        public List<Posts> Posts { get; set; }

        public User (){
            Likes = new List<Likes>();
            Posts = new List<Posts>();
        }

    }

}
