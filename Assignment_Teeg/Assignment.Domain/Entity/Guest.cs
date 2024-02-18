using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Assignment.Domain.Common;

namespace Assignment.Domain;

//Database Model
public class Guest : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    public TitleList Title { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string BirthDate  { get; set; }

    public string Email { get; set; } = string.Empty;

    public string PhoneNumbers { get; set; } = string.Empty;

    public enum TitleList
    {
        NA,
        Mr,
        Mrs,
        Miss
    }
}

