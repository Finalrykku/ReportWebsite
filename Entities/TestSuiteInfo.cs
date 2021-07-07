using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class TestSuiteInfo
{
    [Key]
    public Guid Guid { get; set; }
    public int TestrunId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string TeamProject { get; set; }
    public string Type { get; set; }
    public DateTime DbEventTimeStamp { get; set; } = DateTime.Now;
    public List<TestCaseInfo> TestCases { get; set; }
}