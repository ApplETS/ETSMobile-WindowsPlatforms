using Ets.Mobile.Business.Entities.Results.Signets;
using Ets.Mobile.Client.Factories.Interfaces.Signets;
using Ets.Mobile.Entities.Signets;
using System;
using System.Linq;

namespace Ets.Mobile.Client.Factories.Implementations.Signets
{
    public class ScheduleAndTeachersFactory : IScheduleAndTeachersFactory
    {
        public ScheduleAndTeachersVm Create(ScheduleAndTeachersResult result)
        {
            return new ScheduleAndTeachersVm
            {
                Activities = result.Activities.Select(activity => new ActivityVm
                {
                    Acronym = activity.Acronym,
                    Day = Convert.ToInt32(activity.Day),
                    DayName = activity.DayName,
                    EndHour = TimeSpan.Parse(activity.EndHour),
                    Group = activity.Group,
                    IsPrincipalActivity = activity.IsPrincipalActivity == "Oui",
                    Location = activity.Location,
                    Name = activity.Name,
                    StartHour = TimeSpan.Parse(activity.StartHour),
                    Title = activity.Title,
                    Type = activity.Type
                }).ToArray(),
                Teachers = result.Teachers.Select(teacher => new TeacherVm
                {
                    Email = teacher.Email,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    IsPrimaryTeacher = teacher.IsPrimaryTeacher == "Oui",
                    Location = teacher.Location,
                    Phone = teacher.Phone
                }).ToArray()
            };
        }
    }
}