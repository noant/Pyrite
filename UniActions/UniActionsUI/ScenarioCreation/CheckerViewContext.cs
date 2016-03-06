using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UniActionsClientIntefaces;
using UniActionsCore.ScenarioCreation;
using UniStandartActions.Checkers;

namespace UniActionsUI.ScenarioCreation
{
    public class CheckerViewContext : DependencyObject
    {
        // dependency props impl
        public static readonly DependencyProperty CheckerStringProperty;
        public static readonly DependencyProperty IsFirstProperty;
        public static readonly DependencyProperty AllOperatorViewsProperty;

        static CheckerViewContext()
        {
            CheckerStringProperty = DependencyProperty.Register("CheckerString", typeof(string), typeof(CheckerViewContext));
            IsFirstProperty = DependencyProperty.Register("IsFirst", typeof(bool), typeof(CheckerViewContext),
                    new FrameworkPropertyMetadata()
                    {
                        PropertyChangedCallback = (o, e) =>
                        {
                            if ((bool)e.NewValue)
                            {
                                ((CheckerViewContext)o).AllOperatorViews =
                                    from @operator in new[] { Operator.And }
                                    from not in new[] { true, false }
                                    select new OperatorPairView(@operator, not, true);
                            }
                            else
                            {
                                ((CheckerViewContext)o).AllOperatorViews =
                                    from @operator in new[] { Operator.And, Operator.Or }
                                    from not in new[] { true, false }
                                    select new OperatorPairView(@operator, not, false);
                            }
                        }
                    }
                );
            AllOperatorViewsProperty = DependencyProperty.Register("AllOperatorViews", typeof(IEnumerable<OperatorPairView>), typeof(CheckerViewContext),
                new FrameworkPropertyMetadata()
                {
                    DefaultValue = from @operator in new[] { Operator.And, Operator.Or }
                                   from not in new[] { true, false }
                                   select new OperatorPairView(@operator, not, false)
                }
            );
        }

        public string CheckerString
        {
            get
            {
                return (string)GetValue(CheckerStringProperty);
            }
            set
            {
                SetValue(CheckerStringProperty, value);
            }
        }

        public bool IsFirst
        {
            get
            {
                return (bool)GetValue(IsFirstProperty);
            }
            set
            {
                SetValue(IsFirstProperty, value);
            }
        }

        public IEnumerable<OperatorPairView> AllOperatorViews
        {
            get
            {
                return (IEnumerable<OperatorPairView>)GetValue(AllOperatorViewsProperty);
            }
            set
            {
                SetValue(AllOperatorViewsProperty, value);
            }
        }
        //

        public CheckerViewContext(OperatorCheckerPair operatorCheckerPair)
        {
            _operatorCheckerPair = operatorCheckerPair;

            var checkerString = Helper.CreateParamsViewString(Checker);
            if (!string.IsNullOrWhiteSpace(checkerString))
            {
                this.CheckerString = " (" + checkerString + ")";
            }
            else
            {
                this.CheckerString = string.Empty;
            }
        }

        public IEnumerable<CustomCheckerView> AllCustomCheckers
        {
            get
            {
                return App.Uni.ModulesControl.CustomCheckers.Select(x => new CustomCheckerView(x));
            }
        }

        private OperatorCheckerPair _operatorCheckerPair;

        private void CreateChecker(Type @typeof)
        {
            _operatorCheckerPair.Checker = (ICustomChecker)AllCustomCheckers
                       .Single(x => x.CheckerType.Equals(@typeof))
                       .CheckerType
                       .GetConstructor(new Type[0])
                       .Invoke(new object[0]);

            BeginCheckerUserSettings();
        }

        public void BeginCheckerUserSettings()
        {
            if (_operatorCheckerPair.Checker.AllowUserSettings)
                _operatorCheckerPair.Checker.BeginUserSettings();

            var checkerString = Helper.CreateParamsViewString(Checker);
            if (!string.IsNullOrWhiteSpace(checkerString))
            {
                this.CheckerString = " (" + checkerString + ")";
            }
            else
            {
                this.CheckerString = string.Empty;
            }
            RaiseChanged();
        }

        public OperatorPairView OperatorPairView
        {
            get
            {
                if (_operatorCheckerPair == null)
                    _operatorCheckerPair = new OperatorCheckerPair();
                if (_operatorCheckerPair.Checker == null)
                    CreateChecker(typeof(NeverChecker));
                return new OperatorPairView(_operatorCheckerPair.Operator, _operatorCheckerPair.Not, IsFirst);
            }
            set
            {
                _operatorCheckerPair.Not = value.Not;
                _operatorCheckerPair.Operator = value.Operator;
                RaiseChanged();
            }
        }

        public ICustomChecker Checker
        {
            get
            {
                if (_operatorCheckerPair == null)
                    _operatorCheckerPair = new OperatorCheckerPair();
                if (_operatorCheckerPair.Checker == null)
                    CreateChecker(typeof(NeverChecker));
                return _operatorCheckerPair.Checker;
            }
            set
            {
                if (_operatorCheckerPair == null)
                    _operatorCheckerPair = new OperatorCheckerPair();
                _operatorCheckerPair.Checker = value;
                RaiseChanged();
            }
        }

        public CustomCheckerView CheckerType
        {
            get
            {
                return new CustomCheckerView(Checker.GetType());
            }
            set
            {
                CreateChecker(value.CheckerType);
                RaiseChanged();
            }
        }

        public void RaiseChanged()
        {
            if (Changed != null)
                Changed(this, new EventArgs());
        }

        public event Action<object, EventArgs> Changed;
    }

    public struct OperatorPairView
    {
        public OperatorPairView(Operator @operator, bool not, bool isFirstInView)
        {
            Operator = @operator;
            Not = not;
            IsFirstInView = isFirstInView;
        }
        public Operator Operator { get; private set; }
        public bool Not { get; private set; }
        public bool IsFirstInView { get; private set; }
        public override string ToString()
        {
            var result = (!IsFirstInView ? (Operator == Operator.And ? "И" : "ИЛИ") : string.Empty) + (Not ? " НЕ" : string.Empty);
            if (string.IsNullOrEmpty(result))
                result = " ";
            return result;
        }
    }

    public struct CustomCheckerView
    {
        public CustomCheckerView(Type checkerType)
        {
            CheckerType = checkerType;
        }
        public Type CheckerType { get; private set; }

        public override string ToString()
        {
            return App.Uni.ModulesControl.GetViewName(CheckerType).Value;
        }
    }
}
