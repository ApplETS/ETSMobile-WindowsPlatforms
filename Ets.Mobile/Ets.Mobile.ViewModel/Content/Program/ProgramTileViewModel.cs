using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Ets.Mobile.Entities.Signets;
using ReactiveUI;

namespace Ets.Mobile.ViewModel.Content.Program
{
    [DataContract]
    public class ProgramTileViewModel : ReactiveObject, IDisposable
    {
        #region IDisposable Implementation

        public void Dispose()
        {
            Model = null;
        }

        #endregion

        [DataMember]
        public ProgramVm Model { get; protected set; }

        public ProgramTileViewModel(ProgramVm model)
        {
            Model = model;
        }
    }
}
