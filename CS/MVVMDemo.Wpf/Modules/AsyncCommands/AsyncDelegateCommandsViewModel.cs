using DevExpress.Mvvm;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MVVMDemo.AsyncCommands {
    public class AsyncDelegateCommandsViewModel : ViewModelBase {
        public AsyncCommand CalculateCommand { get; private set; }

        Task Calculate() {
            return Task.Factory.StartNew(CaclulateCore);
        }
        void CaclulateCore() {
            for(int i = 0; i <= 100; i++) {
                if(CalculateCommand.IsCancellationRequested) {
                    Progress = 0;
                    return;
                }
                Progress = i;
                Thread.Sleep(TimeSpan.FromMilliseconds(20));
            }
        }

        int _Progress;
        public int Progress {
            get { return _Progress; }
            set { SetProperty(ref _Progress, value, () => Progress); }
        }

        public AsyncDelegateCommandsViewModel() {
            CalculateCommand = new AsyncCommand(Calculate);
        }

    }
}
