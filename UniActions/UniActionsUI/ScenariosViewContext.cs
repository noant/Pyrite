using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniActionsCore.ScenarioCreation;

namespace UniActionsUI
{
    public class ScenariosViewContext
    {
        public IEnumerable<ScenarioViewItem> AllScenarioViews
        {
            get
            {
                return App.Uni.TasksPool.Scenarios.Select(x => new ScenarioViewItem() { Scenario = x });
            }
        }

        public struct ScenarioViewItem
        {
            public Scenario Scenario { get; set; }
            public override string ToString()
            {
                return Scenario != null ? Scenario.Name : string.Empty;
            }
        }
    }
}
