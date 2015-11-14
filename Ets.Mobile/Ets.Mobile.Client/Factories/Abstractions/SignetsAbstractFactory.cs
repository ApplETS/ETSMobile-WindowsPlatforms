using Ets.Mobile.Client.Factories.Interfaces;

namespace Ets.Mobile.Client.Factories.Abstractions
{
    public abstract class SignetsAbstractFactory
    {
        public abstract ICoursesFactory GetCoursesFactory();
        public abstract ICoursesForSemesterFactory GetCoursesForSemesterFactory();
        public abstract ICoursesIntervalSemestersFactory GetCoursesIntervalSemestersFactory();
        public abstract IEvaluationsFactory GetEvaluationsFactory();
        public abstract IProgramsFactory GetProgramsFactory();
        public abstract IReplacedDaysFactory GetReplacedDaysFactory();
        public abstract IScheduleAndTeachersFactory GetScheduleAndTeachersFactory();
        public abstract IScheduleFinalExamsFactory GetScheduleFinalExamsFactory();
        public abstract IScheduleFactory GetScheduleFactory();
        public abstract ISemestersFactory GetSemestersFactory();
        public abstract ITeammatesFactory GetTeammatesFactory();
        public abstract IUserDetailsFactory GetUserDetailsFactory();
    }
}
