
using System;
using System.ComponentModel.DataAnnotations;

public class RoleAssignment
{
    [Key]
    public Guid Guid { get; set; }
    public string Role { get; set; }
    public TauInfo TauInfo { get; set; }
    public DateTime DbEventTimeStamp { get; set; } = DateTime.Now;
}