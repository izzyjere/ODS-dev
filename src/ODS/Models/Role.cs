﻿namespace ODS.Models
{
    public class Role : IdentityRole<int>
    {
        public string Description { get; set; }
        public Role() : base() { }
        public Role(string roleName, string description) : base(roleName)
        {
            Description = description;
        }
    }
}
