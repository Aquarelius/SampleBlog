using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SB.DAL.Entities;

namespace SB.Models
{
    public class CommentViewModel
    {
        public CommentViewModel() { }

        public CommentViewModel(Comment data)
        {
            PostId = data.Post.Id;
            Id = data.Id;
            Added = data.Added;
            Text = data.Text;
            ParentCommentId = data.ParentCommentId;
            AuthorId = data.Author.Id;
            Author = data.Author.UserName;
        }

        public int PostId { get; set; }
        public int Id { get; set; }
        public DateTime Added { get; set; }
        public string Text { get; set; }

        public int? ParentCommentId { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

    }
}