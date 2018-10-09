using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChartsDemo {
    class PerformanceDataSource : List<PerformanceDataItem> {
        #region Data
        public PerformanceDataSource() {
            Add(new PerformanceDataItem(1, 0.21, 0.22, 0.21, 5, 5, 5));
            Add(new PerformanceDataItem(2, 0.31, 0.11, 0.02, 7, 7, 20));
            Add(new PerformanceDataItem(3, 0.11, 0.21, 0.35, 2, 12, 18));
            Add(new PerformanceDataItem(4, 0.13, 0.25, 0.29, 7, 25, 21));
            Add(new PerformanceDataItem(5, 0.02, 0.10, 0.15, 25, 25, 19));
            Add(new PerformanceDataItem(6, 0.05, 0.11, 0.21, 27, 20, 10));
            Add(new PerformanceDataItem(7, 0.11, 0.15, 0.23, 44, 17, 8));
            Add(new PerformanceDataItem(8, 0.15, 0.20, 0.30, 45, 24, 15));
            Add(new PerformanceDataItem(9, 0.18, 0.25, 0.36, 50, 29, 17));
            Add(new PerformanceDataItem(10, 0.23, 0.12, 0.38, 52, 25, 12));
            Add(new PerformanceDataItem(11, 0.21, 0.08, 0.36, 52, 28, 40));
            Add(new PerformanceDataItem(12, 0.16, 0.08, 0.37, 55, 29, 47));
            Add(new PerformanceDataItem(13, 0.22, 0.27, 0.33, 53, 25, 50));
            Add(new PerformanceDataItem(14, 0.25, 0.29, 0.22, 51, 28, 45));
            Add(new PerformanceDataItem(15, 0.22, 0.31, 0.19, 49, 30, 50));
            Add(new PerformanceDataItem(16, 0.23, 0.34, 0.15, 45, 42, 51));
            Add(new PerformanceDataItem(17, 0.25, 0.40, 0.03, 46, 45, 48));
            Add(new PerformanceDataItem(18, 0.32, 0.54, 0.04, 42, 40, 43));
            Add(new PerformanceDataItem(19, 0.30, 0.51, 0.03, 45, 20, 15));
            Add(new PerformanceDataItem(20, 0.31, 0.45, 0.07, 48, 21, 19));
            Add(new PerformanceDataItem(21, 0.25, 0.40, 0.05, 48, 35, 25));
            Add(new PerformanceDataItem(22, 0.10, 0.43, 0.07, 49, 33, 27));
            Add(new PerformanceDataItem(23, 0.05, 0.45, 0.15, 49, 35, 30));
            Add(new PerformanceDataItem(24, 0.03, 0.44, 0.21, 51, 37, 32));
            Add(new PerformanceDataItem(25, 0.01, 0.42, 0.23, 55, 40, 37));
            Add(new PerformanceDataItem(26, 0.01, 0.45, 0.21, 57, 43, 39));
            Add(new PerformanceDataItem(27, 0.01, 0.43, 0.22, 59, 50, 43));
            Add(new PerformanceDataItem(28, 0.01, 0.39, 0.25, 62, 51, 42));
            Add(new PerformanceDataItem(29, 0.03, 0.27, 0.20, 42, 31, 23));
            Add(new PerformanceDataItem(30, 0.07, 0.25, 0.14, 25, 20, 17));
            Add(new PerformanceDataItem(31, 0.05, 0.12, 0.09, 35, 25, 20));
            Add(new PerformanceDataItem(32, 0.03, 0.10, 0.05, 41, 29, 24));
            Add(new PerformanceDataItem(33, 0.05, 0.08, 0.06, 48, 32, 26));
            Add(new PerformanceDataItem(34, 0.02, 0.09, 0.06, 55, 37, 28));
            Add(new PerformanceDataItem(35, 0.05, 0.11, 0.07, 59, 38, 28));
            Add(new PerformanceDataItem(36, 0.03, 0.13, 0.05, 63, 39, 30));
            Add(new PerformanceDataItem(37, 0.02, 0.15, 0.03, 67, 43, 31));
            Add(new PerformanceDataItem(38, 0.05, 0.12, 0.07, 71, 50, 32));
            Add(new PerformanceDataItem(39, 0.07, 0.16, 0.12, 65, 43, 31));
            Add(new PerformanceDataItem(40, 0.09, 0.25, 0.18, 61, 39, 30));
            Add(new PerformanceDataItem(41, 0.09, 0.23, 0.19, 60, 38, 30));
            Add(new PerformanceDataItem(42, 0.10, 0.25, 0.20, 63, 37, 31));
            Add(new PerformanceDataItem(43, 0.11, 0.22, 0.18, 64, 35, 32));
            Add(new PerformanceDataItem(44, 0.13, 0.31, 0.19, 60, 36, 30));
            Add(new PerformanceDataItem(45, 0.17, 0.33, 0.22, 58, 35, 31));
            Add(new PerformanceDataItem(46, 0.23, 0.30, 0.27, 63, 32, 33));
            Add(new PerformanceDataItem(47, 0.20, 0.25, 0.30, 58, 29, 31));
            Add(new PerformanceDataItem(48, 0.17, 0.23, 0.35, 62, 28, 32));
            Add(new PerformanceDataItem(49, 0.15, 0.25, 0.37, 60, 26, 30));
            Add(new PerformanceDataItem(50, 0.12, 0.22, 0.40, 55, 23, 27));
            Add(new PerformanceDataItem(51, 0.11, 0.20, 0.42, 57, 21, 31));
            Add(new PerformanceDataItem(52, 0.09, 0.18, 0.45, 60, 20, 35));
            Add(new PerformanceDataItem(53, 0.08, 0.17, 0.46, 65, 19, 45));
            Add(new PerformanceDataItem(54, 0.05, 0.10, 0.52, 77, 17, 43));
            Add(new PerformanceDataItem(55, 0.03, 0.12, 0.55, 81, 18, 40));
            Add(new PerformanceDataItem(56, 0.05, 0.09, 0.53, 75, 17, 15));
            Add(new PerformanceDataItem(57, 0.07, 0.12, 0.47, 67, 18, 16));
            Add(new PerformanceDataItem(58, 0.03, 0.09, 0.35, 60, 19, 12));
            Add(new PerformanceDataItem(59, 0.05, 0.12, 0.23, 41, 10, 5));
            Add(new PerformanceDataItem(60, 0.03, 0.07, 0.10, 33, 5, 3));
        }
        #endregion
    }
    class PerformanceDataItem {
        int second;
        double process1CpuUsage;
        double process2CpuUsage;
        double process3CpuUsage;
        double process1Memory;
        double process2Memory;
        double process3Memory;

        public int Second { get { return second; } }
        public double Process1CpuUsage { get { return process1CpuUsage; } }
        public double Process2CpuUsage { get { return process2CpuUsage; } }
        public double Process3CpuUsage { get { return process3CpuUsage; } }
        public double Process1Memory { get { return process1Memory; } }
        public double Process2Memory { get { return process2Memory; } }
        public double Process3Memory { get { return process3Memory; } }

        public PerformanceDataItem(int second, double process1CpuUsage, double process2CpuUsage, double process3CpuUsage,
                                               double process1Memory, double process2Memory, double process3Memory) {
            this.second = second;
            this.process1CpuUsage = process1CpuUsage;
            this.process2CpuUsage = process2CpuUsage;
            this.process3CpuUsage = process3CpuUsage;
            this.process1Memory = process1Memory;
            this.process2Memory = process2Memory;
            this.process3Memory = process3Memory;
        }
    }
}
