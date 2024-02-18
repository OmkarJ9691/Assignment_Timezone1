using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Assignment.Domain.Common;

namespace Assignment.Domain;

//Business Model
public class GuestModel : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    public TitleList Title { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string BirthDate  { get; set; }

    public string Email { get; set; } = string.Empty;

    [NotMapped]
    public List<string> PhoneNumbers { get; set; } = new List<string>();

    public enum TitleList
    {
        NA,
        Mr,
        Mrs,
        Miss
    }
}

