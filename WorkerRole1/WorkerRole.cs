using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace WorkerRole1
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        private volatile bool busy = true;

        public override void Run()
        {
            Debug.WriteLine("WorkerRole1 is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at https://go.microsoft.com/fwlink/?LinkId=166357.

            bool result = base.OnStart();

            RoleEnvironment.TraceSource.Switch.Level = SourceLevels.Off;

            RoleEnvironment.Changed += RoleEnvironmentChanged;
            RoleEnvironment.Changing += RoleEnvironmentChanging;
            RoleEnvironment.SimultaneousChanged += RoleEnvironmentSimultaneousChanged;
            RoleEnvironment.SimultaneousChanging += RoleEnvironmentSimultaneousChanging;
            RoleEnvironment.Stopping += RoleEnvironmentStopping;

            Debug.WriteLine("WorkerRole1 has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("WorkerRole1 is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Debug.WriteLine("WorkerRole1 has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            await Task.Run(new Action(Server.Initialise), cancellationToken);

            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(10000);
            }
        }


        private void RoleEnvironmentChanged(object sender, RoleEnvironmentChangedEventArgs e)
        {
            try
            {
                var settingChanges = e.Changes.OfType<RoleEnvironmentConfigurationSettingChange>();

                foreach (var settingChange in settingChanges)
                {
                    var message = "Setting: " + settingChange.ConfigurationSettingName;
                    //Trace.WriteLine(message, "Information");
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Is called if the azure portal environment changes as a result of sysadmin manual activity on the webpage.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoleEnvironmentChangingEventArgs" /> instance containing the event data.</param>
        private void RoleEnvironmentChanging(object sender, RoleEnvironmentChangingEventArgs e)
        {
            try
            {
                //Trace.WriteLine("WorkerRole RoleEnvironmentChanging", "Information");

                // If a configuration setting is changing
                if (e.Changes.Any(change => change is RoleEnvironmentConfigurationSettingChange))
                {
                    // Set e.Cancel to true to restart this role instance
                    e.Cancel = true;
                }
            }
            catch (Exception)
            {
            }
        }

        private void RoleEnvironmentStatusCheck(object sender, RoleInstanceStatusCheckEventArgs e)
        {
            try
            {
                if (this.busy)
                {
                    Debug.WriteLine("WorkerRole Is Busy");
                    //e.SetBusy();
                }

                //TODO: Add code to decide what to do for a busy worker role
            }
            catch (Exception)
            {
            }
        }

        private void RoleEnvironmentSimultaneousChanged(object sender, SimultaneousChangedEventArgs e)
        {
            try
            {
                var topologyChanges = e.Changes.OfType<SimultaneousTopologyChange>();

                foreach (var change in topologyChanges)
                {
                    var message = "Topology change: " + change.RoleName;
                    //Trace.WriteLine(message, "Information");
                }
            }
            catch (Exception)
            {
            }
        }

        private void RoleEnvironmentSimultaneousChanging(object sender, SimultaneousChangingEventArgs e)
        {
            try
            {
                var topologyChanges = e.Changes.OfType<SimultaneousTopologyChange>();

                foreach (var change in topologyChanges)
                {
                    if (change.RoleName.Equals(RoleEnvironment.CurrentRoleInstance.Role.Name))
                    {
                        //TODO: Code to process topology changes for the role instance
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void RoleEnvironmentStopping(object sender, RoleEnvironmentStoppingEventArgs e)
        {
            try
            {
                Debug.WriteLine("AzureWorkerRole Stopping called");
            }
            catch (Exception)
            {
            }
        }
    }
}
