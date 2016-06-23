using PyriteCore.ScenarioCreation;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PyriteUI
{
    public class EditScenarioViewContext : DependencyObject
    {
        public static readonly DependencyProperty ScenarioNameProperty;
        public static readonly DependencyProperty IsScenarioActiveProperty;
        public static readonly DependencyProperty IsScenarioUsedInServerThreadingProperty;
        public static readonly DependencyProperty ScenarioCategoryProperty;
        public static readonly DependencyProperty IsScenarioUseOnOffStateProperty;
        public static readonly DependencyProperty ScenarioIndexProperty;

        static EditScenarioViewContext()
        {
            ScenarioNameProperty = DependencyProperty.Register("ScenarioName", typeof(string), typeof(EditScenarioViewContext),
                new FrameworkPropertyMetadata()
                {
                    PropertyChangedCallback = (o, e) => ((EditScenarioViewContext)o).Scenario.Name = (string)e.NewValue
                }
                );
            ScenarioCategoryProperty = DependencyProperty.Register("ScenarioCategory", typeof(string), typeof(EditScenarioViewContext),
                new FrameworkPropertyMetadata()
                {
                    PropertyChangedCallback = (o, e) => ((EditScenarioViewContext)o).Scenario.Category = (string)e.NewValue
                }
                );
            ScenarioIndexProperty = DependencyProperty.Register("ScenarioIndex", typeof(int), typeof(EditScenarioViewContext),
                new FrameworkPropertyMetadata()
                {
                    PropertyChangedCallback = (o, e) =>
                        ((EditScenarioViewContext)o).Scenario.Index = (int)e.NewValue
                });
            IsScenarioActiveProperty = DependencyProperty.Register("IsScenarioActive", typeof(bool), typeof(EditScenarioViewContext),
                new FrameworkPropertyMetadata()
                {
                    PropertyChangedCallback = (o, e) => ((EditScenarioViewContext)o).Scenario.IsActive = (bool)e.NewValue
                });
            IsScenarioUsedInServerThreadingProperty = DependencyProperty.Register("IsScenarioUsedInServerThreading", typeof(bool), typeof(EditScenarioViewContext),
                new FrameworkPropertyMetadata()
                {
                    PropertyChangedCallback = (o, e) => ((EditScenarioViewContext)o).Scenario.UseServerThreading = (bool)e.NewValue
                });
            IsScenarioUseOnOffStateProperty = DependencyProperty.Register("IsScenarioUseOnOffState", typeof(bool), typeof(EditScenarioViewContext),
                new FrameworkPropertyMetadata()
                {
                    PropertyChangedCallback = (o, e) => ((EditScenarioViewContext)o).Scenario.UseOnOffState = (bool)e.NewValue
                });
        }

        public EditScenarioViewContext(Scenario scenario)
        {
            if (scenario == null)
            {
                scenario = new Scenario();
                scenario.CurrentPyrite = App.Pyrite;
                scenario.AfterAction += (scen) => App.RaiseItemExecutedEvent(scen);
            }

            this._scenario = scenario;

            ScenarioName = this._scenario.Name;
            ScenarioCategory = this._scenario.Category;
            ScenarioIndex = this._scenario.Index;
            IsScenarioActive = this._scenario.IsActive;
            IsScenarioUsedInServerThreading = this._scenario.UseServerThreading;
            IsScenarioUseOnOffState = this._scenario.UseOnOffState;
        }

        public EditScenarioViewContext() : this(null) { }

        public string ScenarioName
        {
            get
            {
                return (string)GetValue(ScenarioNameProperty);
            }
            set
            {
                SetValue(ScenarioNameProperty, value);
            }
        }

        public string ScenarioCategory
        {
            get
            {
                return (string)GetValue(ScenarioCategoryProperty);
            }
            set
            {
                SetValue(ScenarioCategoryProperty, value);
            }
        }

        public int ScenarioIndex
        {
            get
            {
                return (int)GetValue(ScenarioIndexProperty);
            }
            set
            {
                SetValue(ScenarioIndexProperty, value);
            }
        }

        public bool IsScenarioActive
        {
            get
            {
                return (bool)GetValue(IsScenarioActiveProperty);
            }
            set
            {
                SetValue(IsScenarioActiveProperty, value);
            }
        }

        public bool IsScenarioUsedInServerThreading
        {
            get
            {
                return (bool)GetValue(IsScenarioUsedInServerThreadingProperty);
            }
            set
            {
                SetValue(IsScenarioUsedInServerThreadingProperty, value);
            }
        }

        public bool IsScenarioUseOnOffState
        {
            get
            {
                return (bool)GetValue(IsScenarioUseOnOffStateProperty);
            }
            set
            {
                SetValue(IsScenarioUseOnOffStateProperty, value);
            }
        }

        public Scenario Scenario
        {
            get
            {
                return _scenario;
            }
        }

        public IEnumerable<string> AllCategories
        {
            get
            {
                return App.Pyrite.ScenariosPool.Scenarios.
                    Where(x => !string.IsNullOrEmpty(x.Category)).
                    Select(x => x.Category).
                    Distinct().
                    OrderBy(x => x);
            }
        }

        private Scenario _scenario;
    }
}
