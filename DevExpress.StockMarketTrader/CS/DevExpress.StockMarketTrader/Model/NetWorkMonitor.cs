using System;
using System.Net.NetworkInformation;
using System.Windows.Threading;
using System.Windows;

namespace DevExpress.StockMarketTrader.Model {
    public class NetworkMonitor2 {

        DispatcherTimer timer;
        Ping ping;
        bool isInit = false;
        public NetworkMonitor2() {
            ping = new Ping();
            ping.PingCompleted += ping_PingCompleted;
            timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 1) };
            timer.Tick += timer_Tick;
            timer.Start();
        }

        public bool IsInternetAvailable { get; private set; }
        public event EventHandler<EventArgs> InternetAvailableChanged;

        void timer_Tick(object sender, EventArgs e) {
            timer.Stop();
            ping.SendAsync("google.com", null);
        }

        void ping_PingCompleted(object sender, PingCompletedEventArgs e) {
            timer.Start();
            bool isAvailable = e.Reply == null || e.Reply.Status != IPStatus.Success ? false : true;
            if (isAvailable != IsInternetAvailable) {
                isInit = true;
                IsInternetAvailable = isAvailable;
                RaiseInternetAvailableChanged();
            }
            else if (!isInit) {
                isInit = true;
                RaiseInternetAvailableChanged();
            }
        }

        private void RaiseInternetAvailableChanged() {
            if (this.InternetAvailableChanged != null)
                this.InternetAvailableChanged(this, EventArgs.Empty);
        }

    }
    public class NetworkMonitor {

        public static bool IsNetworkAvailable() {
            var networks = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var net in networks) {
                if (net.NetworkInterfaceType != NetworkInterfaceType.Loopback && net.NetworkInterfaceType != NetworkInterfaceType.Tunnel && net.OperationalStatus == OperationalStatus.Up)
                    return true;
            }
            return false;
        }

        public event EventHandler IsAvailableChanged;
        public NetworkMonitor() {
            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(NetworkChange_NetworkAvailabilityChanged);
            CheckConnection();
        }

        bool isAvailable = false;
        public bool IsAvailable {
            get { return isAvailable; }
            private set {
                isAvailable = value;
                RaiseIsAvailableChanged();
            }
        }

        private void OnPingCompleted(object sender, PingCompletedEventArgs e) {
            IsAvailable = e.Reply == null || e.Reply.Status != IPStatus.Success ? false : true;
        }

        private void RaiseIsAvailableChanged() {
            this.IsAvailableChanged(this, EventArgs.Empty);
        }
        private void CheckConnection() {
            Ping ping = new Ping();
            ping.SendAsync("www.devexpress.com", null);
            ping.PingCompleted += new PingCompletedEventHandler(OnPingCompleted);

        }
        private void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e) {
            IsAvailable = e.IsAvailable;
        }
    }
}
