using System.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Sockets;
using System.Windows.Forms;

namespace PyriteStandartActions.Actions
{
    public partial class RunRemoteScenarioActionView : Form
    {
        public RunRemoteScenarioActionView()
        {
            InitializeComponent();

            nudPort.Minimum = 0;
            nudPort.Maximum = ushort.MaxValue;

            this.listBox.SelectedIndexChanged += (o, e) =>
            {
                if (listBox.SelectedItem != null)
                {
                    btOk.Enabled = true;
                    ServerCommand = ((ClientScenarioView)listBox.SelectedItem).ServerCommand;
                }
                else btOk.Enabled = false;
            };

            this.listBox.DoubleClick += (o, e) =>
            {
                if (listBox.SelectedItem != null)
                {
                    ServerCommand = ((ClientScenarioView)listBox.SelectedItem).ServerCommand;
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            };

            this.btOk.Enabled = false;

            btRefresh.Click += (o, e) => RefreshHosts();
        }

        public string ServerCommand
        {
            get;
            set;
        }

        public bool BypassStatus
        {
            get
            {
                return cbBypassStatus.Checked;
            }
            set
            {
                cbBypassStatus.Checked = value;
            }
        }

        public string Host
        {
            get
            {
                return tbHost.Text;
            }
            set
            {
                tbHost.Text = value;
            }
        }

        public ushort Port
        {
            get
            {
                return (ushort)nudPort.Value;
            }
            set
            {
                nudPort.Value = value;
            }
        }

        public void RefreshHosts()
        {
            listBox.Items.Clear();
            try
            {
                using (var client = new TcpClient())
                {
                    client.ReceiveTimeout = 1000;
                    client.SendTimeout = 1000;
                    client.Connect(this.Host, RunRemoteScenarioAction.GetNextPyritePort(this.Host, this.Port));

                    var stream = client.GetStream();

                    RunRemoteScenarioAction.SendString(stream, RunRemoteScenarioAction.Constants.Command_GetStartCommands);

                    var count = int.Parse(RunRemoteScenarioAction.GetNextString(stream), CultureInfo.InvariantCulture);

                    bool fastActionsEnded = false;

                    var categories = new List<string>();

                    for (int i = 0; i <= count; i++)
                    {
                        var value = RunRemoteScenarioAction.GetNextString(stream);
                        if (value == RunRemoteScenarioAction.Constants.Command_EndFastActions)
                        {
                            fastActionsEnded = true;
                        }

                        if (!fastActionsEnded)
                        {
                            var clientScenarioView = new ClientScenarioView()
                            {
                                ServerCommand = value,
                                State = RunRemoteScenarioAction.GetNextString(stream)
                            };

                            listBox.Items.Add(clientScenarioView);
                            if (clientScenarioView.ServerCommand.Equals(this.ServerCommand))
                                listBox.SelectedItem = clientScenarioView;
                        }
                        else
                        {
                            categories.Add(value);
                        }
                    }
                    foreach (var category in categories)
                    {
                        using (var clientToGetCategoryScens = new TcpClient())
                        {
                            clientToGetCategoryScens.Connect(this.Host, RunRemoteScenarioAction.GetNextPyritePort(this.Host, this.Port));

                            var streamToGetCategoryScens = clientToGetCategoryScens.GetStream();

                            RunRemoteScenarioAction.SendString(streamToGetCategoryScens, RunRemoteScenarioAction.Constants.Command_GetCategoryCommands);
                            RunRemoteScenarioAction.SendString(streamToGetCategoryScens, category);

                            var categoryCommandsCount = int.Parse(RunRemoteScenarioAction.GetNextString(streamToGetCategoryScens), CultureInfo.InvariantCulture);

                            for (int j = 0; j < categoryCommandsCount; j++)
                            {
                                var clientScenarioView = new ClientScenarioView()
                                {
                                    ServerCommand = RunRemoteScenarioAction.GetNextString(streamToGetCategoryScens),
                                    State = RunRemoteScenarioAction.GetNextString(streamToGetCategoryScens),
                                    Category = category
                                };

                                listBox.Items.Add(clientScenarioView);
                                if (clientScenarioView.ServerCommand.Equals(this.ServerCommand))
                                    listBox.SelectedItem = clientScenarioView;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Не могу соединиться: " + e.Message);
            }
        }

        private class ClientScenarioView
        {
            public string ServerCommand { get; set; }
            public string Category { get; set; }
            public string State { get; set; }

            public override string ToString()
            {
                return !string.IsNullOrEmpty(Category) ? (Category + "  >  " + State) : State;
            }
        }
    }
}
