using System;

namespace PyriteStandartActions.Checkers.Utils
{
    [Serializable]
    public class ValueEqualityPartialImplementation
    {
        public Equality Equality { get; set; }

        public decimal Value { get; set; }

        public int? DecimalPlaces { get; set; }

        public string ValueName { get; set; }

        public decimal Max { get; set; }

        public decimal Min { get; set; }

        public bool IsPertain(decimal val)
        {
            if (Equality.Equals(Utils.Equality.Equal) && val.Equals(Value))
                return true;
            else if (Equality.Equals(Utils.Equality.LessOrEqualThan) && val <= Value)
                return true;
            else if (Equality.Equals(Utils.Equality.LessThan) && val < Value)
                return true;
            else if (Equality.Equals(Utils.Equality.MoreOrEqualThan) && val >= Value)
                return true;
            else if (Equality.Equals(Utils.Equality.MoreThan) && val > Value)
                return true;
            return false;
        }

        public bool BeginSettings()
        {
            var form = new ValueEqualityView();
            form.Max = Max;
            form.Min = Min;
            form.ValueName = ValueName;
            form.Value = Value;
            form.Equality = this.Equality;
            form.DecimalPlaces = DecimalPlaces ?? form.DecimalPlaces;
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.Value = form.Value;
                this.Equality = form.Equality;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            if (this.Equality.Equals(Utils.Equality.Equal))
                return "==" + Value;
            if (this.Equality.Equals(Utils.Equality.LessOrEqualThan))
                return "<=" + Value;
            if (this.Equality.Equals(Utils.Equality.LessThan))
                return "<" + Value;
            if (this.Equality.Equals(Utils.Equality.MoreOrEqualThan))
                return ">=" + Value;
            if (this.Equality.Equals(Utils.Equality.MoreThan))
                return ">" + Value;

            throw new Exception();
        }
    }

    public enum Equality
    {
        Equal = 0,
        MoreThan = 1,
        LessThan = 2,
        MoreOrEqualThan = 4,
        LessOrEqualThan = 8
    }
}
