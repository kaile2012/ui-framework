using System;

namespace Demo.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string ImageUri { get; set; }

        public int FollowerCount { get; set; }
    }
}
