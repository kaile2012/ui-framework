using System;

namespace Demo.Models
{
    public class PostResponse
    {
        [SQLite.PrimaryKey]
        public Guid Id { get; set; }

        public Guid PostId { get; set; }

        public Guid CommenterId { get; set; }

        public string Content { get; set; }

        public bool IsLike { get; set; }

        public DateTime PostedAt { get; set; }
    }
}
