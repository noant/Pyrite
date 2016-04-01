using System;

namespace PyriteStandartActions.Checkers.Utils
{
    [Serializable]
    public class BetweenValuePartialImplementation
    {
        public decimal Value1 { get; set; }
        public decimal Value2 { get; set; }

        public decimal Max { get; set; }
        public decimal Min { get; set; }

        public int? DecimalPlaces { get; set; }

        public bool MoreThanOrEqualFirst { get; set; }
        public bool LessThanOrEqualSecond { get; set; }

        private bool IsFirstLessOrEqual(decimal val)
        {
            if (MoreThanOrEqualFirst)
                return Value1 <= val;
            else return Value1 < val;
        }

        private bool IsSecondMoreOrEqual(decimal val)
        {
            if (MoreThanOrEqualFirst)
                return Value2 >= val;
            else return Value2 > val;
        }

        public bool IsBetween(decimal value)
        {
            return IsFirstLessOrEqual(value) && IsSecondMoreOrEqual(value);
        }

        public string ValueName { get; set; }

        public bool BeginSettings()
        {
            var form = new BetweenValueView();
            form.ValueName = ValueName;
            form.FirstMoreOrEqual = MoreThanOrEqualFirst;
            form.SecondLessOrEqual = LessThanOrEqualSecond;
            form.Max = Max;
            form.Min = Min;
            form.Value1 = Value1;
            form.Value2 = Value2;
            form.DecimalPlaces = DecimalPlaces ?? form.DecimalPlaces;
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.Value1 = form.Value1;
                this.Value2 = form.Value2;
                this.MoreThanOrEqualFirst = form.FirstMoreOrEqual;
                this.LessThanOrEqualSecond = form.SecondLessOrEqual;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return Value1 + (!MoreThanOrEqualFirst ? "<" : "<=") + " ? " + (!LessThanOrEqualSecond ? "<" : "<=") + Value2;
        }
    }
}
