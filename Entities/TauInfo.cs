using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


public class TauInfo
{
    [Key]
    public Guid Guid { get; set; }
    public string Mail { get; set; }

    public string Username { get; set; }

    public string BusinessUnit { get; set; }
    public DateTime DbEventTimeStamp { get; set; } = DateTime.Now;
    public List<RoleAssignment> RoleAssignments { get; set; }

}