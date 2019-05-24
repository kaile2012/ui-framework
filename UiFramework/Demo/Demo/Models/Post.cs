using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public class Post
    {
        [SQLite.PrimaryKey]
        public Guid Id { get; set; }

        public Guid PosterId { get; set; }

        public string Content { get; set; }

        public int LikeCount { get; set; }

        public int CommentCount { get; set; }

        public DateTime PostedAt { get; set; }
    }
}
