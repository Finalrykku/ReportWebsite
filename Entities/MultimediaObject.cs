using System;
using System.ComponentModel.DataAnnotations;

public class MultimediaObject
{
    [Key]
    public Guid Guid { get; set; }
    public string Type { get; set; }
    public string MediaUrl { get; set; }
    public LogLine LogLine { get; set; }
    public DateTime LogDateTime { get; set; }
    public DateTime DbEventTimeStamp { get; set; } = DateTime.Now;
}