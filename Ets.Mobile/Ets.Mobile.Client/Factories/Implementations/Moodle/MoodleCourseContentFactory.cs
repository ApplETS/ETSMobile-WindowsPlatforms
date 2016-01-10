using Ets.Mobile.Business.Entities.Moodle.CoursesContent;
using Ets.Mobile.Client.Factories.Interfaces.Moodle;
using Ets.Mobile.Entities.Moodle;
using System.Linq;

namespace Ets.Mobile.Client.Factories.Implementations.Moodle
{
    public class MoodleCourseContentFactory : IMoodleCourseContentFactory
    {
        public MoodleCourseContentVm[] Create(MoodleCourseContent[] result)
        {
            return result.Select(mcc =>
                new MoodleCourseContentVm
                {
                    Id = mcc.Id,
                    Name = mcc.Name,
                    IsVisible = mcc.IsVisible,
                    Summary = mcc.Summary,
                    SummaryFormat = mcc.SummaryFormat,
                    Modules = mcc.Modules?.Select(mccm => new MoodleCourseModuleVm
                    {
                        Id = mccm.Id,
                        Name = mccm.Name,
                        Instance = mccm.Instance,
                        Description = mccm.Description,
                        IsVisible = mccm.IsVisible,
                        ModIcon = mccm.ModIcon,
                        ModName = mccm.ModName,
                        ModPlural = mccm.ModPlural,
                        Indent = mccm.Indent,
                        Url = mccm.Url,
                        Contents = mccm.Contents?.Select(contents => new MoodleCourseModuleContentVm
                        {
                            ModuleType = contents.Type,
                            FileName = contents.FileName,
                            FilePath = contents.FilePath,
                            FileSize = contents.FileSize,
                            FileUrl = contents.FileUrl,
                            TimeCreated = contents.TimeCreated,
                            TimeModified = contents.TimeModified,
                            SortOrder = contents.SortOrder,
                            UserId = contents.UserId,
                            Author = contents.Author,
                            License = contents.License
                        }).ToArray()
                    }).ToArray()
                }
            ).ToArray();
        }
    }
}