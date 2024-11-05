﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    [Table("Profile")]
    public class Profile
    {
        [Column("Profile ID")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfileId { get; set; }
        [Column("Account ID")]
        public int? AccountId { get; set; }
        [Column("Code")]
        public string? Code { get; set; }
        [Column("First Name")]
        public string? FirstName { get; set; }
        [Column("Middel Name")]
        public string? MiddleName { get; set; }
        [Column("Last Name")]
        public string? LastName { get; set; }
        [Column("Gender ID")]
        public int GenderId { get; set; }
        [Column("Birthday")]
        public string Birthday { get; set; }
        [Column("Major ID")]
        public int MajorId { get; set; }
        [Column("Created At")]
        public string CreatedAt { get; set; }
        [Column("Last Modified At")]
        public string LastModifiedAt { get; set; }
        public Account? Account { get; set; }
        public Major? Major { get; set; }
    }
}
