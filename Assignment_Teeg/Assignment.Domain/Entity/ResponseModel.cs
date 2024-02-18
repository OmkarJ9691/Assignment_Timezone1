using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Assignment.Domain.Common;

namespace Assignment.Domain;

public class ResponseModel
{
    [Key]
    public Guid GuestId { get; set; }

    public string Status { get; set; } = string.Empty;

    public ErrorModel ErrorModel { get; set; }
}

public class ErrorModel
{
    public string ErrorMessage { get; set; } = string.Empty;
}

public enum Status
{
    Success,
    Failure
}
