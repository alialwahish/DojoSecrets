
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;


namespace DojoSecret
{
    public class Posts
    {

        [Key]
        [Required]
        public int PostsId { get; set; }


        [Required]
        public string Post_Content { get; set; }


        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        
        public int likeCount {get;set;}

        public int UserId { get; set; }

        public int? LikesId { get; set; }

        public List<Likes> Likes { get; set; }


        public Posts()
        {
            likeCount=0;
            Likes = new List<Likes>();
        }





    }







}