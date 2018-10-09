using DevExpress.Mvvm.POCO;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MVVMDemo.AsyncCommands {
    public class AsyncPOCOCommandsViewModel {
        public virtual int Progress { get; set; }

        public Task Calculate() {
            return Task.Factory.StartNew(CaclulateCore);
        }
        void CaclulateCore() {
            for(int i = 0; i <= 100; i++) {
                if(this.GetAsyncCommand(x => x.Calculate()).IsCancellationRequested) {
                    Progress = 0;
                    return;
                }
                Progress = i;
                Thread.Sleep(TimeSpan.FromMilliseconds(20));
            }
        }
    }
}
