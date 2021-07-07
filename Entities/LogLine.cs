using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;



public class LogLine
{
    [Key]
    public Guid Guid { get; set; }
    public string Severity { get; set; }
    public int Step { get; set; }
    public string StepDescription { get; set; }
    public string Message { get; set; }
    public string MessageTemplate { get; set; }
    public string MessageType { get; set; }
    public string EventSiteUrl { get; set; }
    public string ExpectedResult { get; set; }
    public string ActionResult { get; set; }
    public string Properties { get; set; }
    public int Index { get; set; }
    public DateTime LogEventTimeStamp { get; set; }
    public DateTime DbEventTimeStamp { get; set; } = DateTime.Now;
    public TimeSpan ResponseTime { get; set; }
    public string Exception { get; set; }
    public TauInfo TauInfo { get; set; }
    public BugInfo BugInfo { get; set; }
    public List<MultimediaObject> MultimediaObjects { get; set; }
    public List<NetworkAction> NetworkActions { get; set; }
}