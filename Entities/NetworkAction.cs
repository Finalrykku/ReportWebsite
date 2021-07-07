using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class NetworkAction
{
    [Key]
    public Guid Guid { get; set; }
    public LogLine LogLine { get; set; }
    public string DocumentUrl { get; set; }
    public string RequestUrl { get; set; }
    public string RequestHeaders { get; set; }
    public string ResponseUrl { get; set; }
    public string ResponseHeaders { get; set; }
    public string Method { get; set; }
    public int Status { get; set; }
    public string Type { get; set; }
    public string MimeType { get; set; }
    public DateTime LogEventTimeStamp { get; set; }
    public TimeSpan ResponseTime { get; set; }
    public DateTime DbEventTimeStamp { get; set; } = DateTime.Now;
}