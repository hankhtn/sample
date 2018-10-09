using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Threading;
using System.Linq;
using System.Diagnostics;
using System.Windows;

namespace DevExpress.StockMarketTrader.Model {

    public enum ActionPriority { Normal, High, Blocked }
    public enum ExecutorStatus { Enabled, Disabled, Offline }

    public class UserStateParams {
        public UserStateParams(Delegate callback, int actionID) {
            if (callback != null)
                this.Callback = callback;
            this.ActionID = actionID;
        }

        public Delegate Callback { get; private set; }
        public int ActionID { get; set; }
    }
    public class ExecutorAction {
        Delegate method;
        ActionPriority priority;
        object[] args;

        public ExecutorAction(Delegate method, ActionPriority priority, object[] args) {
            this.method = method;
            this.priority = priority;
            this.args = args;
        }

        public object[] Args { get { return args; } }
        public Delegate Method { get { return method; } }
        public ActionPriority Priority { get { return priority; } }

    }
    public class Executor {
        Stack<ExecutorAction> actions;
        ExecutorAction currentAction;
        ExecutorStatus status = ExecutorStatus.Disabled;
        int userID = 0, executingActions = 0;
        public NetworkMonitor2 NetMonitor { get; set; }

        public Executor() {
            actions = new Stack<ExecutorAction>();
        }

        public event EventHandler ExecuteFailed;
        public ExecutorStatus Status {
            get { return status; }
            set {
                status = value;
                if (value == ExecutorStatus.Enabled)
                    TryExecute(false);
                if (value == ExecutorStatus.Disabled && currentAction != null)
                    actions.Push(currentAction);
            }
        }
        public int UserID { get { return userID; } }
        public int ExecutingActions { get { return executingActions; } }

        public void AddAction(ExecutorAction action) {
            if (CanAdd(action.Priority)) {
                actions.Push(action);
            }
            TryExecute(false);
        }
        public void ExecuteCompleted() {
            TryExecute(false);
        }
        public void ChangeUserID() {
            actions.Clear();
            userID++;
        }
        public void ForceExecute() {
            TryExecute(true);
        }

        public void EndExecute(IAsyncResult result) {
            try {
                executingActions--;
                UserStateParams state = result.AsyncState as UserStateParams;
                if (state.ActionID == userID && state.Callback != null && status != ExecutorStatus.Disabled && CanExecuteByNetwork()) {
                    
                    state.Callback.DynamicInvoke(new object[] { result });
                }
            }
            catch {
                RaiseExecuteFailed();
            }
        }

        private bool CanExecuteByNetwork() {
            bool isAvailable = NetMonitor.IsInternetAvailable;
            return isAvailable || (!isAvailable && status == ExecutorStatus.Offline);
        }
        private void RaiseExecuteFailed() {
            this.ExecuteFailed(this, EventArgs.Empty);
        }
        private bool CanAdd(ActionPriority priority) {
            
            return true;
        }
        private void TryExecute(bool isForceExecute) {
            if (CanExecuteByNetwork() && actions.Count != 0 && (status != ExecutorStatus.Disabled && (actions.Peek().Priority != ActionPriority.Blocked || isForceExecute))) {
                currentAction = actions.Pop();
                BeginExecute(currentAction);
            }
            
            
            
            
        }
        private void BeginExecute(ExecutorAction action) {
            try {
                executingActions++;
                action.Method.DynamicInvoke(action.Args);
                
            }
            catch { RaiseExecuteFailed(); }
        }
        private void Completed(object sender, EventArgs e) {
            ExecuteCompleted();
        }
    }
}
