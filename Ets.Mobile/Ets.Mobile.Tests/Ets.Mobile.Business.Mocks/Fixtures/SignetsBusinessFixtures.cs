using Ets.Mobile.Business.Entities.Results.Signets;
using Ets.Mobile.Business.Entities.Signets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ets.Mobile.Business.Mocks.Fixtures
{
    public static class SignetsBusinessFixtures
    {
        private static readonly Teacher[] TeachersFixture =
        {
            new Teacher
            {
                Location = "B-1234",
                Phone = "514-123-4567, poste 1234",
                IsPrimaryTeacher = "Oui",
                LastName = "Last1",
                FirstName = "First1",
                Email = "first1.last1@contoso.ca"
            },
            new Teacher
            {
                Location = "A-1234",
                Phone = "514-123-4567",
                IsPrimaryTeacher = "Oui",
                LastName = "Last2",
                FirstName = "First2",
                Email = "first2.last2@contoso.ca"
            },
            new Teacher
            {
                Location = "A-9876",
                Phone = "514-123-4567",
                IsPrimaryTeacher = "Oui",
                LastName = "Last3",
                FirstName = "First3",
                Email = "first3.last3@contoso.ca"
            },
            new Teacher
            {
                Location = "B-6547",
                Phone = "514-123-4567, poste 1234",
                IsPrimaryTeacher = "Oui",
                LastName = "Last4",
                FirstName = "First4",
                Email = "first1.last1@contoso.ca"
            }
        };

        public static readonly LoginResult Login = new LoginResult
        {
            IsAuthentificated = true
        };

        public static readonly UserDetailsResult UserDetails = new UserDetailsResult
        {
            Balance = "10,00$",
            ErrorMessage = "",
            FirstName = "FirstName",
            LastName = "LastName",
            IsMan = true,
            PermanentCode = "LASF01019101"
        };

        public static readonly CoursesResult Courses = new CoursesResult
        {
            Courses = new List<Course>
            {
                new Course
                {
                    Acronym = "ABC123",
                    Credits = 4,
                    Grade = "A+",
                    Group = "01",
                    Name = "Learn Abc!",
                    Program = "0123",
                    Semester = "H2016"
                },
                new Course
                {
                    Acronym = "DEF456",
                    Credits = 4,
                    Grade = "A",
                    Group = "02",
                    Name = "Learn Def!",
                    Program = "0456",
                    Semester = "H2016"
                },
                new Course
                {
                    Acronym = "GHI789",
                    Credits = 4,
                    Grade = "A-",
                    Group = "03",
                    Name = "Learn Ghi!",
                    Program = "0789",
                    Semester = "H2016"
                },
                new Course
                {
                    Acronym = "JKL123",
                    Credits = 4,
                    Grade = "B+",
                    Group = "04",
                    Name = "Learn Jkl!",
                    Program = "0124",
                    Semester = "H2016"
                },
                new Course
                {
                    Acronym = "MNO456",
                    Credits = 4,
                    Grade = "B",
                    Group = "05",
                    Name = "Learn Mno!",
                    Program = "0235",
                    Semester = "H2016"
                },
                new Course
                {
                    Acronym = "QRS789",
                    Credits = 4,
                    Grade = "B-",
                    Group = "06",
                    Name = "Learn Qrs!",
                    Program = "0457",
                    Semester = "H2016"
                }
            },
            ErrorMessage = ""
        };

        public static readonly CoursesIntervalSemesterResult CoursesInterval = new CoursesIntervalSemesterResult
        {
            Courses = new List<Course>
            {
                new Course
                {
                    Acronym = "ABC123",
                    Credits = 4,
                    Grade = "A+",
                    Group = "01",
                    Name = "Learn Abc!",
                    Program = "0123",
                    Semester = "H2016"
                },
                new Course
                {
                    Acronym = "DEF456",
                    Credits = 4,
                    Grade = "A",
                    Group = "02",
                    Name = "Learn Def!",
                    Program = "0456",
                    Semester = "H2016"
                },
                new Course
                {
                    Acronym = "GHI789",
                    Credits = 4,
                    Grade = "A-",
                    Group = "03",
                    Name = "Learn Ghi!",
                    Program = "0789",
                    Semester = "H2016"
                },
                new Course
                {
                    Acronym = "JKL123",
                    Credits = 4,
                    Grade = "B+",
                    Group = "04",
                    Name = "Learn Jkl!",
                    Program = "0124",
                    Semester = "H2016"
                },
                new Course
                {
                    Acronym = "MNO456",
                    Credits = 4,
                    Grade = "B",
                    Group = "05",
                    Name = "Learn Mno!",
                    Program = "0235",
                    Semester = "H2016"
                },
                new Course
                {
                    Acronym = "QRS789",
                    Credits = 4,
                    Grade = "B-",
                    Group = "06",
                    Name = "Learn Qrs!",
                    Program = "0457",
                    Semester = "H2016"
                }
            },
            ErrorMessage = ""
        };

        public static readonly SemestersResult Semesters = new SemestersResult
        {
            Semesters = new List<Semester>
            {
                new Semester
                {
                    AbridgedName = "A2013",
                    Name = "Automne 2013",
                    StartDate = new DateTime(2013, 09, 03),
                    EndDate = new DateTime(2013, 12, 19),
                    EndOfClassesDate = new DateTime(2013, 12, 06),
                    StartCheminot = new DateTime(2013, 06, 02),
                    EndCheminot = new DateTime(2013, 06, 18),
                    StartCancellationDateWithReimbursement = new DateTime(2013, 09, 03),
                    EndCancellationDateWithReimbursement = new DateTime(2013, 09, 13),
                    EndCancellationDateWithReimbursementForNewStudent = new DateTime(2013, 09, 27),
                    StartCancellationDateWithoutReimbursementForNewStudent = new DateTime(2013, 09, 30),
                    EndCancellationDateWithoutReimbursementForNewStudent = new DateTime(2013, 11, 11),
                    LimitForCancellingASEQ = new DateTime(2013, 09, 30)
                },
                new Semester
                {
                    AbridgedName = "H2014",
                    Name = "Hiver 2014",
                    StartDate = new DateTime(2014-01-06),
                    EndDate = new DateTime(2014-04-23),
                    EndOfClassesDate = new DateTime(2014-04-08),
                    StartCheminot = new DateTime(2013-11-03),
                    EndCheminot = new DateTime(2013-11-19),
                    StartCancellationDateWithReimbursement = new DateTime(2014-01-06),
                    EndCancellationDateWithReimbursement = new DateTime(2014-01-17),
                    EndCancellationDateWithReimbursementForNewStudent = new DateTime(2014-01-31),
                    StartCancellationDateWithoutReimbursementForNewStudent = new DateTime(2014-02-03),
                    EndCancellationDateWithoutReimbursementForNewStudent = new DateTime(2014-03-10),
                    LimitForCancellingASEQ = new DateTime(2014-01-31)
                },
                new Semester
                {
                     AbridgedName = "É2014",
                    Name = "Été 2014",
                    StartDate = new DateTime(2014-04-28),
                    EndDate = new DateTime(2014-08-12),
                    EndOfClassesDate = new DateTime(2014-08-01),
                    StartCheminot = new DateTime(2014-03-02),
                    EndCheminot = new DateTime(2014-03-18),
                    StartCancellationDateWithReimbursement = new DateTime(2014-04-28),
                    EndCancellationDateWithReimbursement = new DateTime(2014-05-09),
                    EndCancellationDateWithReimbursementForNewStudent = new DateTime(2014-05-09),
                    StartCancellationDateWithoutReimbursementForNewStudent = new DateTime(2014-05-20),
                    EndCancellationDateWithoutReimbursementForNewStudent = new DateTime(2014-07-07),
                    LimitForCancellingASEQ = new DateTime(1964-01-01)
                },
                new Semester
                {
                    AbridgedName = "A2014",
                    Name = "Automne 2014",
                    StartDate = new DateTime(2014-09-02),
                    EndDate = new DateTime(2014-12-19),
                    EndOfClassesDate = new DateTime(2014-12-05),
                    StartCheminot = new DateTime(2014-06-01),
                    EndCheminot = new DateTime(2014-06-18),
                    StartCancellationDateWithReimbursement = new DateTime(2014-09-02),
                    EndCancellationDateWithReimbursement = new DateTime(2014-09-12),
                    EndCancellationDateWithReimbursementForNewStudent = new DateTime(2014-09-26),
                    StartCancellationDateWithoutReimbursementForNewStudent = new DateTime(2014-09-27),
                    EndCancellationDateWithoutReimbursementForNewStudent = new DateTime(2014-11-10),
                    LimitForCancellingASEQ = new DateTime(2014-10-01)
                },
                new Semester
                {
                    AbridgedName = "H2015",
                    Name = "Hiver 2015",
                    StartDate = new DateTime(2015-01-06),
                    EndDate = new DateTime(2015-04-22),
                    EndOfClassesDate = new DateTime(2015-04-10),
                    StartCheminot = new DateTime(2014-11-02),
                    EndCheminot = new DateTime(2014-11-18),
                    StartCancellationDateWithReimbursement = new DateTime(2015-01-06),
                    EndCancellationDateWithReimbursement = new DateTime(2015-01-19),
                    EndCancellationDateWithReimbursementForNewStudent = new DateTime(2015-01-30),
                    StartCancellationDateWithoutReimbursementForNewStudent = new DateTime(2015-02-02),
                    EndCancellationDateWithoutReimbursementForNewStudent = new DateTime(2015-03-10),
                    LimitForCancellingASEQ = new DateTime(2015-02-02)
                },
                new Semester
                {
                    AbridgedName = "É2015",
                    Name = "Été 2015",
                    StartDate = new DateTime(2015-04-27),
                    EndDate = new DateTime(2015-08-08),
                    EndOfClassesDate = new DateTime(2015-07-29),
                    StartCheminot = new DateTime(2015-03-01),
                    EndCheminot = new DateTime(2015-03-17),
                    StartCancellationDateWithReimbursement = new DateTime(2015-04-27),
                    EndCancellationDateWithReimbursement = new DateTime(2015-05-08),
                    EndCancellationDateWithReimbursementForNewStudent = new DateTime(2015-05-08),
                    StartCancellationDateWithoutReimbursementForNewStudent = new DateTime(2015-05-19),
                    EndCancellationDateWithoutReimbursementForNewStudent = new DateTime(2015-07-03),
                    LimitForCancellingASEQ = new DateTime(2015-02-02)
                },
                new Semester
                {
                    AbridgedName = "É2015",
                    Name = "Automne 2015",
                    StartDate = new DateTime(2015-08-31),
                    EndDate = new DateTime(2015-12-18),
                    EndOfClassesDate = new DateTime(2015-12-05),
                    StartCheminot = new DateTime(2015-05-31),
                    EndCheminot = new DateTime(2015-06-17),
                    StartCancellationDateWithReimbursement = new DateTime(2015-08-31),
                    EndCancellationDateWithReimbursement = new DateTime(2015-09-12),
                    EndCancellationDateWithReimbursementForNewStudent = new DateTime(2015-09-25),
                    StartCancellationDateWithoutReimbursementForNewStudent = new DateTime(2015-09-26),
                    EndCancellationDateWithoutReimbursementForNewStudent = new DateTime(2015-11-14),
                    LimitForCancellingASEQ = new DateTime(2015-09-30)
                },
                new Semester
                {
                    AbridgedName = "H2016",
                    Name = "Hiver 2016",
                    StartDate = new DateTime(2016-01-05),
                    EndDate = new DateTime(2016-04-22),
                    EndOfClassesDate = new DateTime(2016-04-11),
                    StartCheminot = new DateTime(2015-11-01),
                    EndCheminot = new DateTime(2015-11-17),
                    StartCancellationDateWithReimbursement = new DateTime(2016-01-05),
                    EndCancellationDateWithReimbursement = new DateTime(2016-01-16),
                    EndCancellationDateWithReimbursementForNewStudent = new DateTime(2016-01-30),
                    StartCancellationDateWithoutReimbursementForNewStudent = new DateTime(2016-01-31),
                    EndCancellationDateWithoutReimbursementForNewStudent = new DateTime(2016-03-12),
                    LimitForCancellingASEQ = new DateTime(2016-02-01)
                }
            },
            ErrorMessage = ""
        };

        public static readonly ProgramsResult Programs = new ProgramsResult
        {
            Programs = new List<Program>
            {
                new Program
                {
                    Average = "3.5/4.3",
                    Code = "7065",
                    CompletedCreditsCount = "15",
                    EquivalenceCount = "4",
                    FailedCreditsCount = "4",
                    Name = "Baccalauréat en génie logiciel",
                    PotentialCreditsCount = "4",
                    RegisteredCreditsCount = "4",
                    ResearchCreditsCount = "0",
                    SemesterEnd = "H2016",
                    SemesterStart = "A2013",
                    Status = "actif",
                    SuceededCreditsCount = "1"
                }
            },
            ErrorMessage = ""
        };

        public static readonly TeammatesResult Teammates = new TeammatesResult()
        {
            Teammates = new List<Teammate>
            {
                new Teammate
                {
                    Email = "first.last.1@contoso.ca",
                    FirstName = "First1",
                    LastName = "Last1"
                },
                new Teammate
                {
                    Email = "first.last.2@contoso.ca",
                    FirstName = "First2",
                    LastName = "Last1"
                },
                new Teammate
                {
                    Email = "first.last.2@contoso.ca",
                    FirstName = "First2",
                    LastName = "Last2"
                },
                new Teammate
                {
                    Email = "first.last.3@contoso.ca",
                    FirstName = "First3",
                    LastName = "Last3"
                }
            },
            ErrorMessage = ""
        };

        public static EvaluationsResult Evaluations = new EvaluationsResult()
        {
            ActualGrade = 80,
            ActualGradeOfIndividualElements = 80,
            Average = 80,
            FinalGradeOnHundred = 80,
            GradeOnHundredOfIndividualElements = 80,
            Median = 75,
            Percentile = 90,
            StandardDeviation = 10,
            Evaluations = new List<Evaluation>
            {
                new Evaluation
                {
                    Name = "Homework 1",
                    StandardDeviation = "10",
                    Percentile = "100",
                    Median = 80,
                    Average = 75,
                    CourseAndGroup = "ABC123-01",
                    Grade = 80,
                    IgnoredFromCalculation = "Non",
                    MessageOfTeacher = "",
                    Published = DateTime.Now.ToString("O"),
                    TargetDate = DateTime.Now.AddDays(1),
                    Team = "Team 1",
                    Total = "80",
                    Weighting = 10
                },
                new Evaluation
                {
                    Name = "Homework 2",
                    StandardDeviation = "10",
                    Percentile = "100",
                    Median = 80,
                    Average = 75,
                    CourseAndGroup = "ABC123-01",
                    Grade = 80,
                    IgnoredFromCalculation = "Non",
                    MessageOfTeacher = "",
                    Published = DateTime.Now.ToString("O"),
                    TargetDate = DateTime.Now.AddDays(1),
                    Team = "Team 1",
                    Total = "80",
                    Weighting = 10
                }
            },
            ErrorMessage = ""
        };

        public static ScheduleAndTeachersResult ScheduleAndTeachers = new ScheduleAndTeachersResult()
        {
            Activities = new List<Activity>
            {
                new Activity
                {
                    Acronym = "GIA400",
                    Group = "07",
                    Day = "1",
                    DayName = "Lundi",
                    Type = "H",
                    Name = "Travaux pratiques (2 sous-groupes)",
                    IsPrincipalActivity = "Non",
                    StartHour = "08:30",
                    EndHour = "12:30",
                    Location = "A-2512",
                    Title = "Analyse de rentabilité de projets"
                },
                new Activity
                {
                    Acronym = "LOG320",
                    Group = "01",
                    Day = "1",
                    DayName = "Lundi",
                    Type = "Q",
                    Name = "Laboratoire (Groupe A)",
                    IsPrincipalActivity = "Non",
                    StartHour = "13:30",
                    EndHour = "16:30",
                    Location = "A-6953",
                    Title = "Structures de données et algorithmes"
                },
                new Activity
                {
                    Acronym = "LOG320",
                    Group = "01",
                    Day = "1",
                    DayName = "Lundi",
                    Type = "R",
                    Name = "Laboratoire (Groupe B)",
                    IsPrincipalActivity = "Non",
                    StartHour = "13:30",
                    EndHour = "16:30",
                    Location = "A-6842",
                    Title = "Structures de données et algorithmes"
                },
                new Activity
                {
                    Acronym = "LOG515",
                    Group = "01",
                    Day = "1",
                    DayName = "Lundi",
                    Type = "C",
                    Name = "Activité de cours",
                    IsPrincipalActivity = "Oui",
                    StartHour = "18:00",
                    EndHour = "21:30",
                    Location = "A-2536",
                    Title = "Gestion de projets en génie logiciel"
                },
                new Activity
                {
                    Acronym = "GIA400",
                    Group = "07",
                    Day = "3",
                    DayName = "Mercredi",
                    Type = "C",
                    Name = "Activité de cours",
                    IsPrincipalActivity = "Oui",
                    StartHour = "09:00",
                    EndHour = "12:30",
                    Location = "A-3596",
                    Title = "Analyse de rentabilité de projets"
                },
                new Activity
                {
                    Acronym = "PHY335",
                    Group = "01",
                    Day = "3",
                    DayName = "Mercredi",
                    Type = "C",
                    Name = "Activité de cours",
                    IsPrincipalActivity = "Oui",
                    StartHour = "13:30",
                    EndHour = "17:00",
                    Location = "B-6589",
                    Title = "Physique des ondes"
                },
                new Activity
                {
                    Acronym = "LOG515",
                    Group = "01",
                    Day = "3",
                    DayName = "Mercredi",
                    Type = "Q",
                    Name = "Laboratoire (Groupe A)",
                    IsPrincipalActivity = "Non",
                    StartHour = "18:00",
                    EndHour = "21:00",
                    Location = "A-6547",
                    Title = "Gestion de projets en génie logiciel"
                },
                new Activity
                {
                    Acronym = "LOG515",
                    Group = "01",
                    Day = "3",
                    DayName = "Mercredi",
                    Type = "R",
                    Name = "Laboratoire (Groupe B)",
                    IsPrincipalActivity = "Non",
                    StartHour = "18:00",
                    EndHour = "21:00",
                    Location = "A-9876",
                    Title = "Gestion de projets en génie logiciel"
                },
                new Activity
                {
                    Acronym = "LOG320",
                    Group = "01",
                    Day = "4",
                    DayName = "Jeudi",
                    Type = "C",
                    Name = "Activité de cours",
                    IsPrincipalActivity = "Oui",
                    StartHour = "13:30",
                    EndHour = "17:00",
                    Location = "A-4321",
                    Title = "Structures de données et algorithmes"
                },
                new Activity
                {
                    Acronym = "PHY335",
                    Group = "01",
                    Day = "5",
                    DayName = "Vendredi",
                    Type = "E",
                    Name = "Travaux pratiques",
                    IsPrincipalActivity = "Non",
                    StartHour = "09:00",
                    EndHour = "12:00",
                    Location = "B-1234",
                    Title = "Physique des ondes"
                }
            },
            Teachers = TeachersFixture.ToList(),
            ErrorMessage = ""
        };

        public static ScheduleFinalExamsResult ScheduleFinalExams = new ScheduleFinalExamsResult
        {
            ScheduleFinalExams = new List<ScheduleFinalExam>
            {
                new ScheduleFinalExam
                {
                    Abridged = "ABC123",
                    Date = DateTime.Now.AddDays(1),
                    Group = "01",
                    StartHour = DateTime.Now.AddDays(1).AddHours(1).TimeOfDay,
                    EndHour = DateTime.Now.AddDays(1).AddHours(4).TimeOfDay,
                    Location = "A-1234"
                },
                new ScheduleFinalExam
                {
                    Abridged = "ABC123",
                    Date = DateTime.Now.AddDays(2),
                    Group = "01",
                    StartHour = DateTime.Now.AddDays(2).AddHours(1).TimeOfDay,
                    EndHour = DateTime.Now.AddDays(2).AddHours(4).TimeOfDay,
                    Location = "A-1234"
                },
                new ScheduleFinalExam
                {
                    Abridged = "DEF456",
                    Date = DateTime.Now.AddDays(3),
                    Group = "01",
                    StartHour = DateTime.Now.AddDays(3).AddHours(1).TimeOfDay,
                    EndHour = DateTime.Now.AddDays(3).AddHours(4).TimeOfDay,
                    Location = "B-1234"
                },
                new ScheduleFinalExam
                {
                    Abridged = "GHI789",
                    Date = DateTime.Now.AddDays(4),
                    Group = "01",
                    StartHour = DateTime.Now.AddDays(4).AddHours(1).TimeOfDay,
                    EndHour = DateTime.Now.AddDays(4).AddHours(4).TimeOfDay,
                    Location = "A-5678"
                }
            },
            ErrorMessage = ""
        };

        public static CourseForSemesterResult CoursesForSemester = new CourseForSemesterResult
        {
            CoursesForSemester = new List<CourseForSemester>
            {
                new CourseForSemester
                {
                    StartHour = "18:00",
                    EndHour = "21:00",
                    Location = "A-1234",
                    Title = "Course title",
                    Group = "01",
                    Name = "Lab",
                    Acronym = "ABC123",
                    Day = "1",
                    DayName = "Lundi",
                    IsPrincipalActivity = "Oui",
                    Type = "L",
                    Teachers = TeachersFixture
                }
            },
            ErrorMessage = ""
        };

        public static ReplacedDaysResult ReplacedDays = new ReplacedDaysResult
        {
            ReplacedDays = new List<ReplacedDay>
            {
                new ReplacedDay
                {
                    Description = "Replaced for a reason",
                    OriginDate = DateTime.Now.AddDays(1),
                    TargetDate = DateTime.Now.AddDays(2)
                },
                new ReplacedDay
                {
                    Description = "Replaced for a reason",
                    OriginDate = DateTime.Now.AddDays(8),
                    TargetDate = DateTime.Now.AddDays(9)
                },
                new ReplacedDay
                {
                    Description = "Replaced for a reason",
                    OriginDate = DateTime.Now.AddDays(12),
                    TargetDate = DateTime.Now.AddDays(14)
                }
            },
            ErrorMessage = ""
        };

        public static ScheduleResult Schedule = new ScheduleResult
        {
            Schedules = new List<Schedule>
            {
                new Schedule
                {
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddHours(3),
                    CourseAndGroup = "ABC123-01",
                    Description = "Course",
                    Location = "A-1234",
                    Name = "Cours",
                    Title = "Abc Course!"
                },
                new Schedule
                {
                    StartDate = DateTime.Now.AddHours(4),
                    EndDate = DateTime.Now.AddHours(7),
                    Name = "Lab (Group A)",
                    Location = "A-5678",
                    Title = "Def Lab!"
                },
                new Schedule
                {
                    StartDate = DateTime.Now.AddDays(1),
                    EndDate = DateTime.Now.AddDays(1).AddHours(3),
                    Name = "Lab (Group B)",
                    Location = "A-6842",
                    Title = "Abc Lab!"
                },
                new Schedule
                {
                    StartDate = DateTime.Now.AddDays(1).AddHours(4),
                    EndDate = DateTime.Now.AddDays(1).AddHours(7),
                    Name = "Course",
                    Location = "A-2536",
                    Title = "Ghi Course!"
                },
                new Schedule
                {
                    StartDate = DateTime.Now.AddDays(2),
                    EndDate = DateTime.Now.AddDays(2).AddHours(3),
                    Name = "Course",
                    Location = "A-3596",
                    Title = "Def Course!"
                },
                new Schedule
                {
                    StartDate = DateTime.Now.AddDays(2).AddHours(4),
                    EndDate = DateTime.Now.AddDays(2).AddHours(7),
                    Name = "Course",
                    Location = "B-6589",
                    Title = "Jkl Course!"
                },
                new Schedule
                {
                    StartDate = DateTime.Now.AddDays(3),
                    EndDate = DateTime.Now.AddDays(3).AddHours(3),
                    Name = "Lab (Group A)",
                    Location = "A-6547",
                    Title = "Ghi Lab!"
                },
                new Schedule
                {
                    StartDate = DateTime.Now.AddDays(3).AddHours(4),
                    EndDate = DateTime.Now.AddDays(3).AddHours(7),
                    Name = "Lab (Group B)",
                    Location = "A-9876",
                    Title = "Jkl Lab!"
                },
                new Schedule
                {
                    StartDate = DateTime.Now.AddDays(4),
                    EndDate = DateTime.Now.AddDays(4).AddHours(3),
                    Name = "Course",
                    Location = "A-4321",
                    Title = "Mno TP!"
                },
                new Schedule
                {
                    StartDate = DateTime.Now.AddDays(4).AddHours(4),
                    EndDate = DateTime.Now.AddDays(4).AddHours(7),
                    Name = "TP",
                    Location = "B-1234",
                    Title = "Mno TP!"
                }
            },
            ErrorMessage = ""
        };
    }
}