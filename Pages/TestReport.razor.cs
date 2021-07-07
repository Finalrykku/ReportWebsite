using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

/*
 * TODO: Aktuell beinhaltet ein highlevelStep alle Actions, die während des Steps ausgeführt werden. Anpassen
 */

namespace ReportWebsite.Pages
{
    public partial class TestReport
    {
        private string _Testrunid;
        private bool _expandSteps;

        List<Testlog> CurrentTestlogs = new List<Testlog>();
        List<Testlog> CurrentActions = new List<Testlog>();

        List<Testlog> highlevelSteps = new List<Testlog>();
        List<Testlog> actions = new List<Testlog>(); // TODO entfernen

        TestCaseInfo _testCaseInfo;

        public TestReport()
        {
            //recieveTestCaseInfo();

            // random entries for development
            for (int i = 0; i <= 95; i++)
            {
                highlevelSteps.Add(new Testlog(i * 5, "uhuhuhu"));
                actions.Add(new Testlog(i, "tztztztz"));
            }
            assignActionsToSteps();
        }

        private void recieveTestCaseInfo()
        {
            _testCaseInfo = new TestCaseInfo();
            _testCaseInfo.Result = "passed";

            // random entries for development
            for (int i = 0; i <= 95; i++)
            {
                _testCaseInfo.LogLines.Add(new LogLine()
                {
                    Severity = "Info",
                    StepDescription = "actiondesc",
                    ActionResult = "pass",
                    Step = i,
                    LogEventTimeStamp = DateTime.Today.AddMinutes(i)
                });
            }
        }

        private async void toggleExpandSteps()
        {
            _expandSteps = !_expandSteps;
        }

        private string passed()
        {
            if (true) // (_testCaseInfo.Result == "passed")
                return "Passed";
            return "Failed";
        }

        public Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private async void buttonclick()
        {
            Console.WriteLine("test");
        }

        //private async void RowClick(int timestamp)
        private async void VideoJumpTo(double timestamp)
        {
            await JS.InvokeVoidAsync("videoJumpTo", timestamp.ToString());
        }

        private static Func<double, Task> ChangeParaContentActionAsync;
        [JSInvokableAttribute("TrackCurrentRow")]
        public static async void TrackCurrentRow(double timestamp)
        {
            await ChangeParaContentActionAsync.Invoke(timestamp);
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            ChangeParaContentActionAsync = LocalTrackCurrentRow;
        }

        async Task LocalTrackCurrentRow(double timestamp)
        {
            CurrentTestlogs = highlevelSteps.Where(x => timestamp - x.starttime >= 0).OrderBy(x => x.starttime).ToList();
            CurrentActions = CurrentTestlogs.Last().actions.Where(x => timestamp - x.starttime >= 0).OrderBy(x => x.starttime).ToList();
            // expand current step if checkbox is checked
            if(_expandSteps)
                ShowRows(CurrentTestlogs.Last(), true);
            //Console.WriteLine($"update currenttestlogs with {CurrentTestlogs.Count()} entries");
            base.StateHasChanged();
        }

        private async void ShowRows(Testlog testlog, bool forceShow = false)
        {
            foreach (var ele in highlevelSteps)
                if (ele != testlog)
                    ele.ShowActions = false;
            highlevelSteps.First(x => x == testlog).ShowActions = !highlevelSteps.First(x => x == testlog).ShowActions || forceShow ;
            //Console.WriteLine($"showrows {testlog.actions.Count()}");
        }

        
        private void assignActionsToSteps()
        {
            if (actions.Count == 0 || highlevelSteps.Count == 0)
                return;

            for(int i = 0; i < highlevelSteps.Count(); ++i)
            {
                if(i == highlevelSteps.Count() -1)
                    highlevelSteps[i].actions.AddRange(actions.Where(x => x.starttime >= highlevelSteps[i].starttime));
                else
                    highlevelSteps[i].actions.AddRange(actions.Where(x => x.starttime >= highlevelSteps[i].starttime && x.starttime < highlevelSteps[i + 1].starttime));
            }
        }

        /// <summary>
        /// Sets the color for the current and past steps. Update JS in index.html if current Step/Action is modified
        /// </summary>
        /// <param name="testlog"></param>
        /// <returns></returns>
        string GetClassRowSteps(Testlog testlog)
        {
            if (CurrentTestlogs != null && CurrentTestlogs.Count() > 0)
            {
                if (testlog == CurrentTestlogs.Last())
                    return "table-success";

                if (CurrentTestlogs.Contains(testlog))
                    return "table-info";
            }
            return "";
        }
        string GetClassRowActions(Testlog testlog)
        {
            if (CurrentActions != null && CurrentActions.Count() > 0)
            {
                if (testlog == CurrentActions.Last())
                    return "table-warning";

                if (CurrentActions.Contains(testlog))
                    return "table-secondary";
            }
            return "";
        }
        /*
         * 
         
    
    
      {​"Guid":"84ba2223-4131-4e33-bc3b-d72a5860ad62","TestCaseInfo":"fbf0ac0c-b1fb-4f50-8611-638191dc8c59","TauInfo":"75f6f6bc-cdf0-4ddd-93ae-632007b3ed01","Severity":"Debug","Step":0,"Message":"PrintShots: System.Collections.Generic.List`1[System.DateTime]","MessageTemplate":"PrintShots: System.Collections.Generic.List`1[System.DateTime]","MessageType":null,"EventSiteUrl":"https://....com/main.aspx?appid=77beae7e-f2d7-4796-bca1-0b695ad74bf7&forceUCI=1&pagetype=entityrecord&etn=dw_serviceorder&id=ccb63ae8-c765-4377-b8df-621427cdf0d4","ExpectedResult":null,"ActionResult":null,"Properties":"{​\"PrintScreenTimeStamps\":{​\"Elements\":[{​\"Value\":\"2021-07-07T11:28:49.0507741+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:49.2833473+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:49.5506831+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:49.7994648+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:50.0688882+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:50.3231831+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:50.5725435+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:50.8215226+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:51.0785255+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:51.3362934+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:51.6032571+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:51.8702985+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:52.1467124+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:52.3860212+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:52.6533425+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:52.9044297+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:53.1795273+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:53.4384968+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:53.7042485+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:53.9557834+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:54.2235166+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:54.4882007+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:54.7390246+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:55.0079927+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:55.2579909+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:55.5445272+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:55.7726064+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:56.0307661+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:56.3058919+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:56.5613153+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:56.8093184+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:57.0767603+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:57.3417657+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:57.6107651+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:57.858791+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:58.1277703+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:58.3947733+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:58.6597729+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:58.9097773+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:59.1777756+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:59.4429425+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:59.6936299+02:00\"}​,{​\"Value\":\"2021-07-07T11:28:59.9615951+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:00.2125977+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:00.5116396+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:00.7446017+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:01.0116033+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:01.3016084+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:01.5316083+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:01.7966099+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:02.0638315+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:02.3308309+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:02.5988334+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:02.8478357+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:03.1178399+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:03.3848409+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:03.6488422+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:03.9158456+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:04.1829763+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:04.4329796+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:04.6989843+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:04.9659837+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:05.2339863+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:05.4871674+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:05.7381696+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:06.0001703+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:06.2665436+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:06.5302712+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:06.7678149+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:07.0358182+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:07.2841974+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:07.5512361+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:07.8200607+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:08.0850639+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:08.3360283+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:08.6131011+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:08.8605514+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:09.1205511+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:09.3871335+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:09.6527864+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:09.9209699+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:10.1712685+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:10.438238+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:10.6909727+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:10.9626872+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:11.2224194+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:11.4995401+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:11.7394194+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:12.0063177+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:12.2826079+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:12.5241793+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:12.7961816+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:13.0567951+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:13.3069162+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:13.574824+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:13.8432234+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:14.0914494+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:14.3593029+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:14.6093058+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:14.8768826+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:15.1519293+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:15.4091269+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:15.66811+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:15.9355299+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:16.1938212+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:16.4448234+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:16.7102118+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:16.9777907+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:17.2457513+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:17.5118718+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:17.779249+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:18.0390731+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:18.3120621+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:18.5635529+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:18.8297173+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:19.1052691+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:19.3629017+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:19.63095+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:19.883009+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:20.1310111+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:20.3977935+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:20.6650345+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:20.9321433+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:21.2004135+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:21.478235+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:21.7185248+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:21.9885106+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:22.2505139+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:22.5185162+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:22.7903292+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:23.0504123+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:23.3003773+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:23.5699878+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:23.8349875+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:24.0843232+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:24.3520806+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:24.6184555+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:24.8694386+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:25.137069+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:25.4030655+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:25.6700681+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:25.9470732+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:26.1991334+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:26.4546101+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:26.7212966+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:26.988006+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:27.2417634+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:27.4887631+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:27.738982+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:28.0046134+02:00\"}​,{​\"Value\":\"2021-07-07T11:29:28.2556156+02:00\"}​,
    
    
  
  




         * 
         * 
         */

        string getIndex(Testlog testlog)
        {
            return highlevelSteps.IndexOf(testlog).ToString();
        }
    }
    public class Testlog
    {
        public double starttime;
        public string description;
        public List<Testlog> actions = new List<Testlog>();
        public bool ShowActions = false;
        public Testlog(double start, string desc)
        {
            starttime = start;
            description = desc;
        }
    }
}
