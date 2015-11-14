using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using ReactiveUI;

namespace Ets.Mobile.ViewModel.Content.Main
{
    [DataContract]
    public class GradeGroupViewModel : ReactiveObject, IDisposable
    {
        [DataMember]
        public GradeSummaryViewModelGroup Model { get; protected set; }

        public GradeGroupViewModel(GradeSummaryViewModelGroup gradeSummaryViewModelGroup)
        {
            Model = gradeSummaryViewModelGroup;
        }

        public void Dispose()
        {
            Model?.Dispose();
        }
    }
}
