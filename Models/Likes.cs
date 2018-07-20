using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;


namespace DojoSecret
{

    public class Likes
    {
        [Key]
        public int LikesId { get; set; }


        public int UserId { get; set; }
        public User User { get; set; }

        public int PostsId { get; set; }
        public Posts Posts { get; set; }




    }


}