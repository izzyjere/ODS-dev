﻿namespace ODS.Models
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid UserGuid { get; set; }
        public string PictureUrl { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; } = true;
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public List<UserRole> Roles { get; set; }
    }
}