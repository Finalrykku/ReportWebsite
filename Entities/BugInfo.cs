using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class BugInfo
{
    [Key]
    public Guid Guid { get; set; }
    public int WorkItemId { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public DateTime CreationTimeStamp { get; set; }
    public DateTime ResolvedTimeStamp { get; set; }
    public DateTime ClosedTimeStamp { get; set; }
    public DateTime DbEventTimeStamp { get; set; } = DateTime.Now;
    public List<LogLine> LogLines { get; set; }

}