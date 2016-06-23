using PyriteClientIntefaces;
using PyriteCore.ScenarioCreation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PyriteUI.ScenarioCreation
{
    public class CheckerViewContext : DependencyObject
    {
        // dependency props impl
        public static readonly DependencyProperty CheckerStringProperty;
        public static readonly DependencyProperty IsFirstProperty;
        public static readonly DependencyProperty AllOperatorViewsProperty;
        public static readonly DependencyProperty ParamsVisibilityProperty;

        static CheckerViewContext()
        {
            ParamsVisibilityProperty = DependencyProperty.Register("ParamsVisibility", typeof(Visibility), typeof(CheckerViewContext));
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

        public Visibility ParamsVisibility
        {
            get
            {
                return (Visibility)GetValue(ParamsVisibilityProperty);
            }
            set
            {
                SetValue(ParamsVisibilityProperty, value);
            }
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

            var checkerString = Helper.CreateParamsViewString(this._operatorCheckerPair.Checker);
            if (!string.IsNullOrWhiteSpace(checkerString))
                this.CheckerString = "(" + checkerString + ")";

            this.ParamsVisibility = this._operatorCheckerPair.Checker.AllowUserSettings ? Visibility.Visible : Visibility.Collapsed;
        }

        public IEnumerable<CustomCheckerView> AllCustomCheckers
        {
            get
            {
                return App.Pyrite.ModulesControl.CustomCheckers.Select(x => new CustomCheckerView(x)).OrderBy(x => x.ToString());
            }
        }

        private OperatorCheckerPair _operatorCheckerPair;

        private void CreateChecker(Type @typeof)
        {
            _operatorCheckerPair.Checker = App.Pyrite.ModulesControl.CreateCheckerInstance(@typeof, false).Value;
            BeginCheckerUserSettings();
            _operatorCheckerPair.Checker.Refresh();
            this.ParamsVisibility = this._operatorCheckerPair.Checker.AllowUserSettings ? Visibility.Visible : Visibility.Collapsed;
        }

        public void BeginCheckerUserSettings()
        {
            if (_operatorCheckerPair.Checker.AllowUserSettings)
            {
                if (_operatorCheckerPair.Checker.BeginUserSettings())
                {
                    _operatorCheckerPair.Checker.Refresh();
                    RaiseChanged();
                }
                var checkerString = Helper.CreateParamsViewString(Checker);
                if (!string.IsNullOrWhiteSpace(checkerString))
                {
                    this.CheckerString = "(" + checkerString + ")";
                }
            }
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
                if (!this.IsFirst)
                    _operatorCheckerPair.Operator = value.Operator;
                else
                    _operatorCheckerPair.Operator = Operator.And; // crutch
                this.ParamsVisibility = this._operatorCheckerPair.Checker.AllowUserSettings ? Visibility.Visible : Visibility.Collapsed;
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
                this.ParamsVisibility = this._operatorCheckerPair.Checker.AllowUserSettings ? Visibility.Visible : Visibility.Collapsed;
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
            return App.Pyrite.ModulesControl.GetViewName(CheckerType).Value;
        }
    }
}
