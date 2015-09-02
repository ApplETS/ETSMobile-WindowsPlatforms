using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ets.Mobile.Business.Contracts;
using Ets.Mobile.Business.Entities.Results.Signets;
using Ets.Mobile.Business.Entities.Signets;

namespace Ets.Mobile.Business.DesignTime
{
    public class DtSignetsBusinessService : ISignetsBusinessService
    {
        public Task<LoginResult> LoginRaw(Dictionary<string, string> form)
        {
            var loginResult = new LoginResult()
            {
                IsAuthentificated = true
            };

            return ReturnT(loginResult);
        }

        public Task<UserDetailsResult> UserDetailsRaw(Dictionary<string, string> form)
        {
            var userDetailsResult = new UserDetailsResult
            {
                PermanentCode = "CODEPERM",
                ErrorMessage = null,
                IsMan = true,
                LastName = "Last Name",
                FirstName = "First Name",
                Balance = "0"
            };

            return ReturnT(userDetailsResult);
        }

        public Task<CoursesResult> CoursesRaw(Dictionary<string, string> form)
        {
            var coursesResult = new CoursesResult
            {
                ErrorMessage = null,
                Courses = new List<Course>
                {
                    new Course
                    {
                        Grade = "A",
                        Group = "01",
                        Credits = 4,
                        Program = "LOG",
                        Semester = "E" + DateTime.Now.Year,
                        Acronym = "LOG240",
                        Name = "Analyse et conception de logiciels"
                    },
                    new Course
                    {
                        Grade = "A",
                        Group = "01",
                        Credits = 4,
                        Program = "LOG",
                        Semester = "E" + DateTime.Now.Year,
                        Acronym = "LOG210",
                        Name = "Analyse et conception de logiciels"
                    },
                }
            };

            return ReturnT(coursesResult);
        }

        public Task<CoursesIntervalSemesterResult> CoursesIntervalSemesterRaw(Dictionary<string, string> form)
        {
            var coursesResult = new CoursesIntervalSemesterResult
            {
                ErrorMessage = null,
                Courses = new List<Course>
                {
                    new Course
                    {
                        Grade = "A",
                        Group = "01",
                        Credits = 4,
                        Program = "LOG",
                        Semester = "E" + DateTime.Now.Year,
                        Acronym = "LOG240",
                        Name = "Analyse et conception de logiciels"
                    },
                    new Course
                    {
                        Grade = "A",
                        Group = "01",
                        Credits = 4,
                        Program = "LOG",
                        Semester = "E" + DateTime.Now.Year,
                        Acronym = "LOG210",
                        Name = "Analyse et conception de logiciels"
                    },
                }
            };

            return ReturnT(coursesResult);
        }

        public Task<SemestersResult> SemestersRaw(Dictionary<string, string> form)
        {
            var semestersResult = new SemestersResult
            {
                ErrorMessage = null,
                Semesters = new List<Semester>
                {
                    new Semester
                    {
                        AbridgedName = "E2015",
                        Name = "Été 2015",
                        StartDate = new DateTime(2015, 04, 27),
                        EndDate = new DateTime(2015, 08, 08),
                        EndOfClassesDate = new DateTime(2015, 07, 29),
                        StartCheminot = new DateTime(2015, 03, 01),
                        EndCheminot = new DateTime(2015, 03, 17),
                        StartCancellationDateWithReimbursement = new DateTime(2015, 04, 27),
                        EndCancellationDateWithReimbursement = new DateTime(2015, 05, 08),
                        EndCancellationDateWithReimbursementForNewStudent = new DateTime(2015, 05, 08),
                        StartCancellationDateWithoutReimbursementForNewStudent = new DateTime(2015, 05, 19),
                        EndCancellationDateWithoutReimbursementForNewStudent = new DateTime(2015, 07, 03),
                        LimitForCancellingASEQ = new DateTime(2015, 02, 02)
                    }
                }
            };

            return ReturnT(semestersResult);
        }

        public Task<ProgramsResult> ProgramsRaw(Dictionary<string, string> form)
        {
            var programsResult = new ProgramsResult
            {
                ErrorMessage = null,
                Programs = new List<Program>
                {
                    new Program
                    {
                        Code = "0725",
                        Name = "Microprogramme de 1er cycle en enseignement coopératif I",
                        Average = 60,
                        CompletedCreditsCount = "26",
                        RegisteredCreditsCount = "4",
                        PotentialCreditsCount = "56",
                        SearchCreditsCount = "35",
                        FailedCreditsCount = "0",
                        SuceededCreditsCount = "4",
                        EquivalenceCount = "0",
                        SemesterStart = "A2014",
                        SemesterEnd = "A2014",
                        Status = "diplômé"
                    }
                }
            };

            return ReturnT(programsResult);
        }

        public Task<TeammatesResult> TeammatesRaw(Dictionary<string, string> form)
        {
            var teammatesResult = new TeammatesResult
            {
                ErrorMessage = null,
                Teammates = new List<Teammate>
                {
                    new Teammate
                    {
                        FirstName = "Lorem",
                        LastName = "Ipsum",
                        Email = "lorem.ipsum@ens.etsmtl.ca"
                    }
                }
            };

            return ReturnT(teammatesResult);
        }

        public Task<EvaluationsResult> EvaluationsRaw(Dictionary<string, string> form)
        {
            var evaluationsResult = new EvaluationsResult
            {
                ErrorMessage = null,
                StandardDeviationOfClass = 95,
                Evaluations = null,
                MedianOfClass = 75,
                AverageOfClass = 79,
                ActualGrade = 95,
                ActualGradeOfIndividualElements = 95,
                GradeOnHundredOfIndividualElements = 95,
                PercentileOfClass = 75,
                FinalGradeOnHundred = 95
            };

            return ReturnT(evaluationsResult);
        }

        public Task<ScheduleAndTeachersResult> ScheduleAndTeachersRaw(Dictionary<string, string> form)
        {
            var scheduleAndTeachersResult = new ScheduleAndTeachersResult
            {
                Activities = new List<Activity>
                {
                    new Activity
                    {
                        Group = "01",
                        Acronym = "LOG240",
                        Title = "Analyse et conception de logiciels",
                        IsPrincipalActivity = "Oui",
                        Type = "C",
                        StartHour = DateTime.Now.ToString(),
                        EndHour = DateTime.Now.AddMinutes(90).ToString(),
                        Day = "5",
                        DayName = "Friday",
                        Location = "A-1300",
                        Name = "Activité de cours"
                    }
                },
                Teachers = new List<Teacher>
                {
                    new Teacher
                    {
                        Email = "enseignant@ens.etsmtl.ca",
                        IsPrimaryTeacher = "Oui",
                        Location = "B-2400",
                        LastName = "enseignant",
                        FirstName = "enseignant",
                        Phone = "5141234567"
                    }
                }
            };

            return ReturnT(scheduleAndTeachersResult);
        }

        public Task<ScheduleFinalExamsResult> ScheduleFinalExamsRaw(Dictionary<string, string> form)
        {
            var scheduleFinalExamsResult = new ScheduleFinalExamsResult
            {
                ErrorMessage = null,
                ScheduleFinalExams = new List<ScheduleFinalExam>
                {
                    new ScheduleFinalExam
                    {
                        Abridged = "LOG330",
                        Date = DateTime.Now.AddMinutes(30),
                        StartHour = DateTime.Now.AddMinutes(30).TimeOfDay,
                        EndHour = DateTime.Now.AddMinutes(120).TimeOfDay,
                        Group = "01",
                        Location = "A-3000"
                    }
                }
            };

            return ReturnT(scheduleFinalExamsResult);
        }

        public Task<CourseForSemesterResult> CoursesForSemesterRaw(Dictionary<string, string> form)
        {
            var courseForSemesterResult = new CourseForSemesterResult
            {
                ErrorMessage = null,
                CoursesForSemester = new List<CourseForSemester>
                {
                    new CourseForSemester
                    {
                        Group = "01",
                        Acronym = "LOG999",
                        Title = "Lorem ipsum dolor sit amet",
                        IsPrincipalActivity = "Oui",
                        Type = "C",
                        StartHour = DateTime.Now.ToString(),
                        EndHour = DateTime.Now.AddMinutes(90).ToString(),
                        Day = "5",
                        DayName = "Vendredi",
                        Location = "A-1300",
                        Name = "Activité de cours"
                    },
                    new CourseForSemester
                    {
                        Group = "01",
                        Acronym = "LOG998",
                        Title = "Interdum et malesuada fames",
                        IsPrincipalActivity = "Oui",
                        Type = "C",
                        StartHour = DateTime.Now.AddMinutes(120).ToString(),
                        EndHour = DateTime.Now.AddMinutes(240).ToString(),
                        Day = "5",
                        DayName = "Vendredi",
                        Location = "A-1300",
                        Name = "Activité de cours"
                    }
                }
            };

            return ReturnT(courseForSemesterResult);
        }

        public Task<ReplacedDaysResult> ReplacedDaysRaw(Dictionary<string, string> form)
        {
            var replacedDaysResult = new ReplacedDaysResult
            {
                ErrorMessage = null,
                ReplacedDays = new List<ReplacedDay>
                {
                    new ReplacedDay
                    {
                        OriginDate = DateTime.Now,
                        TargetDate = DateTime.Now.AddDays(1),
                        Description = "Date Change"
                    }
                }
            };

            return ReturnT(replacedDaysResult);
        }

        public Task<ScheduleResult> ScheduleRaw(Dictionary<string, string> form)
        {
            var listeSceance = new ScheduleResult
            {
                ErrorMessage = null,
                Schedules = new List<Schedule>
                {
                    new Schedule
                    {
                        CourseAndGroup = "LOG210-01",
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 0, 0),
                        EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 30, 0),
                        Description = 
                            "Méthodes et techniques de modélisation orientés objet, langage de modélisation, cas d'utilisation, analyse orientée objet, modèle du domaine, conception et programmation orientées objet, principes GRASP, patrons de conception, processus itératif et évolutif.",
                        Title = "Analyse et conception de logiciels",
                        Location = "A-1300",
                        Name = "Analyse et conception de logiciels"
                    },
                    new Schedule
                    {
                        CourseAndGroup = "LOG240-01",
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 30, 0),
                        EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0),
                        Description = 
                            "Méthodes et techniques de modélisation orientés objet, langage de modélisation, cas d'utilisation, analyse orientée objet, modèle du domaine, conception et programmation orientées objet, principes GRASP, patrons de conception, processus itératif et évolutif.",
                        Title = "Analyse et conception de logiciels",
                        Location = "A-1300",
                        Name = "Analyse et conception de logiciels"
                    },
                    new Schedule
                    {
                        CourseAndGroup = "STA999-01",
                        StartDate = new DateTime(DateTime.Now.AddMinutes(34).Year, DateTime.Now.AddMinutes(34).Month, DateTime.Now.AddMinutes(34).Day, DateTime.Now.AddMinutes(34).Hour, DateTime.Now.AddMinutes(34).Minute, 0),
                        EndDate = new DateTime(DateTime.Now.AddHours(1).AddMinutes(30).Year, DateTime.Now.AddHours(1).AddMinutes(30).Month, DateTime.Now.AddHours(1).AddMinutes(30).Day, DateTime.Now.AddHours(1).AddMinutes(30).Hour, DateTime.Now.AddHours(1).AddMinutes(30).Minute, 0),
                        Description = 
                            "Méthodes et techniques de modélisation orientés objet, langage de modélisation, cas d'utilisation, analyse orientée objet, modèle du domaine, conception et programmation orientées objet, principes GRASP, patrons de conception, processus itératif et évolutif.",
                        Title = "Analyse et conception de logiciels",
                        Location = "A-1300",
                        Name = "Analyse et conception de logiciels"
                    },
                    new Schedule
                    {
                        CourseAndGroup = "STA998-01",
                        StartDate = new DateTime(DateTime.Now.AddHours(1).AddMinutes(1).Year, DateTime.Now.AddHours(1).AddMinutes(1).Month, DateTime.Now.AddHours(1).AddMinutes(1).Day, DateTime.Now.AddHours(1).AddMinutes(1).Hour, DateTime.Now.AddHours(1).AddMinutes(1).Minute, 0),
                        EndDate = new DateTime(DateTime.Now.AddHours(1).AddMinutes(30).Year, DateTime.Now.AddHours(1).AddMinutes(30).Month, DateTime.Now.AddHours(1).AddMinutes(30).Day, DateTime.Now.AddHours(1).AddMinutes(30).Hour, DateTime.Now.AddHours(1).AddMinutes(30).Minute, 0),
                        Description = 
                            "Méthodes et techniques de modélisation orientés objet, langage de modélisation, cas d'utilisation, analyse orientée objet, modèle du domaine, conception et programmation orientées objet, principes GRASP, patrons de conception, processus itératif et évolutif.",
                        Title = "Analyse et conception de logiciels",
                        Location = "A-1300",
                        Name = "Analyse et conception de logiciels"
                    }
                }
            };

            return ReturnT(listeSceance);
        }

        #region Return Helper

        private static Task<T> ReturnT<T>(T obj)
        {
            return Task.FromResult(obj);
        }

        #endregion
    }
}
