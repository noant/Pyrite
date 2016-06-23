using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PyriteStandartActions.CoreActionsUI
{
    public partial class RunExistingScenarioActionView : Form
    {
        public RunExistingScenarioActionView(Dictionary<Guid, string> allScenarios, Guid selectedScenario)
        {
            InitializeComponent();
            foreach (var scenario in allScenarios)
            {
                listBox.Items.Add(scenario.Value);
            }

            listBox.SelectedIndexChanged += (o, e) =>
            {
                if (listBox.SelectedItem == null)
                    btSelect.Enabled = false;
                else
                {
                    btSelect.Enabled = true;
                    SelectedScenario = allScenarios.Single(x => x.Value == listBox.SelectedItem.ToString()).Key;
                }
            };

            listBox.DoubleClick += (o, e) =>
            {
                if (listBox.SelectedItem != null)
                {
                    SelectedScenario = allScenarios.Single(x => x.Value == listBox.SelectedItem.ToString()).Key;
                    DialogResult = DialogResult.OK;
                }
            };
        }

        public Guid SelectedScenario { get; private set; }
    }
}
