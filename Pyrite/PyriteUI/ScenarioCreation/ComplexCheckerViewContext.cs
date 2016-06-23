using PyriteCore.ScenarioCreation;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PyriteUI.ScenarioCreation
{
    public class ComplexCheckerViewContext : DependencyObject
    {
        //dependecy props impl

        public static readonly DependencyProperty IsFirstProperty;
        public static readonly DependencyProperty AllOperatorViewsProperty;

        static ComplexCheckerViewContext()
        {
            IsFirstProperty = DependencyProperty.Register("IsFirst", typeof(bool), typeof(ComplexCheckerViewContext),
                    new FrameworkPropertyMetadata()
                    {
                        PropertyChangedCallback = (o, e) =>
                        {
                            if ((bool)e.NewValue)
                            {
                                ((ComplexCheckerViewContext)o).AllOperatorViews =
                                    from @operator in new[] { Operator.And }
                                    from not in new[] { true, false }
                                    select new OperatorPairView(@operator, not, true);
                            }
                            else
                            {
                                ((ComplexCheckerViewContext)o).AllOperatorViews =
                                    from @operator in new[] { Operator.And, Operator.Or }
                                    from not in new[] { true, false }
                                    select new OperatorPairView(@operator, not, false);
                            }
                        }
                    }
                );
            AllOperatorViewsProperty = DependencyProperty.Register("AllOperatorViews", typeof(IEnumerable<OperatorPairView>), typeof(ComplexCheckerViewContext),
                new FrameworkPropertyMetadata()
                {
                    DefaultValue = from @operator in new[] { Operator.And, Operator.Or }
                                   from not in new[] { true, false }
                                   select new OperatorPairView(@operator, not, false)
                }
            );
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

        public ComplexCheckerViewContext(OperatorCheckerPair complexCheckerPair)
        {
            _operatorCheckerPair = complexCheckerPair;
        }

        public IEnumerable<OperatorCheckerPair> AllOperatorCheckerPairs
        {
            get
            {
                return ((ComplexChecker)_operatorCheckerPair.Checker).OperatorCheckers;
            }
        }

        private OperatorCheckerPair _operatorCheckerPair;

        public OperatorCheckerPair AddChecker()
        {
            if (_operatorCheckerPair == null)
                _operatorCheckerPair = new OperatorCheckerPair();
            if (_operatorCheckerPair.Checker == null)
                _operatorCheckerPair.Checker = new ComplexChecker();

            var checker = new NeverChecker();

            var pair = new OperatorCheckerPair()
            {
                Checker = checker,
                Operator = Operator.And
            };

            ((ComplexChecker)_operatorCheckerPair.Checker).OperatorCheckers.Add(pair);
            return pair;
        }

        public OperatorCheckerPair AddGroupChecker()
        {
            if (_operatorCheckerPair == null)
                _operatorCheckerPair = new OperatorCheckerPair();
            if (_operatorCheckerPair.Checker == null)
                _operatorCheckerPair.Checker = new ComplexChecker();

            var checker = new ComplexChecker();

            var pair = new OperatorCheckerPair()
            {
                Checker = checker,
                Operator = Operator.And
            };

            ((ComplexChecker)_operatorCheckerPair.Checker).OperatorCheckers.Add(pair);

            return pair;
        }

        public void RemoveChecker(OperatorCheckerPair pair)
        {
            ((ComplexChecker)_operatorCheckerPair.Checker).OperatorCheckers.Remove(pair);
        }

        public OperatorPairView OperatorPairView
        {
            get
            {
                if (_operatorCheckerPair == null)
                    _operatorCheckerPair = new OperatorCheckerPair();
                return new OperatorPairView(_operatorCheckerPair.Operator, _operatorCheckerPair.Not, IsFirst);
            }
            set
            {
                _operatorCheckerPair.Not = value.Not;
                _operatorCheckerPair.Operator = value.Operator;
            }
        }
    }
}
