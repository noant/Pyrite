using System.Collections.Generic;
using System.Linq;
using UniActionsCore.ScenarioCreation;

namespace UniActionsUI
{
    public class ScenariosViewContext
    {
        public IEnumerable<ScenarioViewItem> AllScenarioViews
        {
            get
            {
                return App.Uni.ScenariosPool.Scenarios.Select(x => new ScenarioViewItem() { Scenario = x });
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
