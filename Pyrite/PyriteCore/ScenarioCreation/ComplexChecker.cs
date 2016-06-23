using PyriteClientIntefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace PyriteCore.ScenarioCreation
{
    [Serializable]
    public class ComplexChecker : ICustomChecker, IHasCheckerAction
    {
        public ComplexChecker()
        {
            OperatorCheckers = new List<OperatorCheckerPair>();
        }

        public List<OperatorCheckerPair> OperatorCheckers { get; set; }

        [XmlIgnore]
        public bool AllowUserSettings
        {
            get
            {
                return true;
            }
        }

        [XmlIgnore]
        public bool IsCanDoNow
        {
            get
            {
                if (OperatorCheckers == null)
                    return false;

                var bools = OperatorCheckers.Select(x => new OperatorBoolPair()
                {
                    Operator = x.Operator,
                    Value = x.Not ? !x.Checker.IsCanDoNow : x.Checker.IsCanDoNow
                }).ToList();

                if (bools.Any())
                {
                    bools[0].Operator = Operator.Or;
                    for (int i = 0; i < bools.Count; i++)
                    {
                        if (bools[i].Operator == Operator.And)
                        {
                            bools[i - 1].Value &= bools[i].Value;
                            bools.Remove(bools[i]);
                            i--;
                        }
                    }

                    return bools.Select(x => x.Value).Aggregate((v1, v2) => v1 || v2);
                }
                return false;
            }
        }

        [XmlIgnore]
        public string Name
        {
            get
            {
                return "Сложная проверка";
            }
        }

        public bool BeginUserSettings()
        {
            return true;
        }

        public bool RemoveChecker(Type checkerType)
        {
            bool result = false;
            if (OperatorCheckers != null)
                foreach (var operatorPair in this.OperatorCheckers.ToList())
                {
                    if (operatorPair.Checker.GetType().Equals(checkerType))
                    {
                        if (this.OperatorCheckers.Remove(operatorPair))
                            result = true;
                    }
                    else if (operatorPair.Checker is IHasCheckerAction)
                    {
                        if (((IHasCheckerAction)operatorPair).RemoveChecker(checkerType))
                            result = true;
                    }
                }
            return result;
        }

        public bool RemoveAction(Type actionType)
        {
            return false;
        }

        public void ForAllActionAndChecker(Action<object> action)
        {
            if (OperatorCheckers != null)
                foreach (var operatorPair in this.OperatorCheckers.ToList())
                {
                    if (operatorPair.Checker is IHasCheckerAction)
                    {
                        ((IHasCheckerAction)operatorPair.Checker).ForAllActionAndChecker(action);
                    }
                    else
                    {
                        action(operatorPair.Checker);
                    }
                }
        }

        public void Refresh()
        {

        }
    }

    [Serializable]
    public class OperatorCheckerPair
    {
        public bool Not { get; set; }
        public Operator Operator { get; set; }
        [XmlIgnore]
        public ICustomChecker Checker { get; set; }

        //for xml serialization
        public object CheckerObj
        {
            get
            {
                return Checker;
            }
            set
            {
                Checker = (ICustomChecker)value;
            }
        }
    }

    internal class OperatorBoolPair
    {
        public Operator Operator { get; set; }
        public bool Value { get; set; }
    }

    public enum Operator
    {
        And = 0,
        Or = 1
    }
}
