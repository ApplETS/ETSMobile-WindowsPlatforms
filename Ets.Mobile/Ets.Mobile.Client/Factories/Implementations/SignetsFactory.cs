using System;
using System.Reflection;
using Ets.Mobile.Client.Factories.Abstractions;
using Ets.Mobile.Client.Factories.Interfaces;

namespace Ets.Mobile.Client.Factories.Implementations
{
    public class SignetsFactory : SignetsAbstractFactory
    {
        private ICoursesFactory _coursesFactory;
        public override ICoursesFactory GetCoursesFactory()
        {
            return _coursesFactory ?? (_coursesFactory = new CoursesFactory());
        }

        private ICoursesForSemesterFactory _coursesForSemesterFactory;
        public override ICoursesForSemesterFactory GetCoursesForSemesterFactory()
        {
            return _coursesForSemesterFactory ?? (_coursesForSemesterFactory = new CoursesForSemesterFactory());
        }

        private ICoursesIntervalSemestersFactory _coursesIntervalSemestersFactory;
        public override ICoursesIntervalSemestersFactory GetCoursesIntervalSemestersFactory()
        {
            return _coursesIntervalSemestersFactory ?? (_coursesIntervalSemestersFactory = new CoursesIntervalSemestersFactory());
        }

        private IEvaluationsFactory _evaluationsFactory;
        public override IEvaluationsFactory GetEvaluationsFactory()
        {
            return _evaluationsFactory ?? (_evaluationsFactory = new EvaluationsFactory());
        }

        private IProgramsFactory _programsFactory;
        public override IProgramsFactory GetProgramsFactory()
        {
            return _programsFactory ?? (_programsFactory = new ProgramsFactory());
        }

        private IReplacedDaysFactory _replacedDaysFactory;
        public override IReplacedDaysFactory GetReplacedDaysFactory()
        {
            return _replacedDaysFactory ?? (_replacedDaysFactory = new ReplacedDaysFactory());
        }

        private IScheduleAndTeachersFactory _scheduleAndTeachersFactory;
        public override IScheduleAndTeachersFactory GetScheduleAndTeachersFactory()
        {
            return _scheduleAndTeachersFactory ?? (_scheduleAndTeachersFactory = new ScheduleAndTeachersFactory());
        }

        private IScheduleFinalExamsFactory _scheduleFinalExamsFactory;
        public override IScheduleFinalExamsFactory GetScheduleFinalExamsFactory()
        {
            return _scheduleFinalExamsFactory ?? (_scheduleFinalExamsFactory = new ScheduleFinalExamsFactory());
        }

        private IScheduleFactory _scheduleFactory;
        public override IScheduleFactory GetScheduleFactory()
        {
            return _scheduleFactory ?? (_scheduleFactory = new ScheduleFactory());
        }

        private ISemestersFactory _semestersFactory;
        public override ISemestersFactory GetSemestersFactory()
        {
            return _semestersFactory ?? (_semestersFactory = new SemestersFactory());
        }

        private ITeammatesFactory _teammatesFactory;
        public override ITeammatesFactory GetTeammatesFactory()
        {
            return _teammatesFactory ?? (_teammatesFactory = new TeammatesFactory());
        }

        private IUserDetailsFactory _userDetailsFactory;
        public override IUserDetailsFactory GetUserDetailsFactory()
        {
            return _userDetailsFactory ?? (_userDetailsFactory = new UserDetailsFactory());
        }
    }

    public static class SignetsFactoryExtensions
    {
        /// <summary>
        /// <para>Create an object using the <param name="abstractFactory">Abstract Factory</param></para>
        /// <para>The factory is resolved by type using Reflection</para>
        /// </summary>
        /// <typeparam name="TInput">Result</typeparam>
        /// <typeparam name="TOutput">View Model</typeparam>
        /// <param name="abstractFactory">Abstract Factory</param>
        /// <param name="result">Result</param>
        /// <returns></returns>
        public static TOutput CreateFor<TInput, TOutput>(this SignetsAbstractFactory abstractFactory, TInput result)
        {
            var runtimeMethod = abstractFactory.GetType()
                    .GetRuntimeMethod(FormatGetFactory(typeof(TInput)), new Type[] { });

            var factory = (IFactory<TInput, TOutput>)runtimeMethod.Invoke(abstractFactory, new object[] { });

            return factory.Create(result);
        }

        private const string GetTFactory = "Get{0}Factory";
        private static readonly Func<Type, string> FormatGetFactory = type => string.Format(GetTFactory, type.Name.Replace("Result", ""));
    }
}
