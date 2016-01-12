using System;

namespace Ets.Mobile.Client.Entities.Schedule
{
    public class ScheduleForLiveTile
    {
        public ScheduleForLiveTile(string activityName, string location, string name, string group, DateTime startTime, DateTime endTime)
        {
            ActivityName = activityName;
            Location = location;
            Name = name;
            Group = group;
            StartDate = startTime;
            EndDate = endTime;
        }

        public string ActivityName { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}