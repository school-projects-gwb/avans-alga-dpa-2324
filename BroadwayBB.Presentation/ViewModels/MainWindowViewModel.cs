using GalaSoft.MvvmLight;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BroadwayBB.Presentation.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private bool _enable = false;
        public bool Enable
        {
            get { return _enable; }
            set {
                this.RaiseAndSetIfChanged(ref _enable, value);
            }
        }

        private string _gridPath = string.Empty;
        public string GridPath
        {
            get
            {
                return _gridPath;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _gridPath, value);

                Enable = (!string.IsNullOrEmpty(_gridPath) && !string.IsNullOrEmpty(_artistsPath));
            }
        }

        private string _artistsPath = string.Empty;
        public string ArtistsPath
        {
            get
            {
                return _artistsPath;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _artistsPath, value);

                Enable = (!string.IsNullOrEmpty(_gridPath) && !string.IsNullOrEmpty(_artistsPath));
            }
        }
    }
}
