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
    }
}
