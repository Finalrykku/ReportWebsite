using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class TestCaseInfo
{
    [Key]
    public Guid Guid { get; set; }
    public int Id { get; set; }
    public int Sprint { get; set; }
    public string Area { get; set; }
    public string AssignedTo { get; set; }
    public string Title { get; set; }
    public string Priority { get; set; }
    public string Url { get; set; }
    public string AdoData { get; set; }
    public string ValidationAssessment { get; set; }
    public int AssertBugCount { get; set; }
    public int BugCount { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Testobject { get; set; }
    public string TestobjectVersion { get; set; }
    public string AgentName { get; set; }
    public string OsVersion { get; set; }
    public string ScreenResolution { get; set; }
    public TimeSpan Duration { get; set; }
    public string TestPlatform { get; set; }
    public string TestPlatformVersion { get; set; }
    public string Result { get; set; }
    public DateTime DbEventTimeStamp { get; set; } = DateTime.Now;
    public TestSuiteInfo TestSuiteInfo { get; set; }
    public List<LogLine> LogLines { get; set; }
    public List<BugInfo> BugInfos { get; set; }
}