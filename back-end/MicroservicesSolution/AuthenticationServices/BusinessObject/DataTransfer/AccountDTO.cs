﻿namespace BusinessObject.DataTransfer
{
    public class AccountDTO
    {
        public int Id { get; set;}
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? RoleId { get; set; }
        public RoleDTO? Role { get; set; }
    }
}
