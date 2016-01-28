using HierarchicalData;
using System;
using UniActionsClientIntefaces;

namespace UniActionsCore
{
    public class ComplexChecker : ICustomChecker
    {
        public bool IsCanDoNow
        {
            get { 
                throw new NotImplementedException(); 
            }
        }

        public string Name
        {
            get { 
                return "Сложная проверка"; 
            }
        }

        public bool BeginUserSettings()
        {
            return true;
        }

        public bool AllowUserSettings
        {
            get { return true; }
        }

        public void Refresh()
        {
        }
    }

    public enum BoolOperation
    {
        And,
        Or
    }

    public class OperationPair
    {
        private static class Names
        {
            public static readonly string Operation = "operationType";
            public static readonly string Checker = "checker";
            public static readonly string Invert = "invert";
        }

        public BoolOperation Operation { get; set; }

        public bool InvertChecker { get; set; }

        public ICustomChecker Checker { get; set; }

        public HierarchicalObject HObject
        {
            get
            {
                var hobject = new HierarchicalObject();
                hobject[Names.Operation] = Operation;
                hobject[Names.Invert] = InvertChecker;
                hobject[Names.Checker] = HierarchicalData.Helper.CreateBySettingsAttribute(Checker);
                return hobject;
            }
            set
            {
                Operation = value[Names.Operation];
                InvertChecker = value[Names.Invert];
                HierarchicalData.Helper.SetToObject(Checker, value[Names.Checker]);
            }
        }

        public bool Evaluate
        {
            get
            {
                return InvertChecker ? Checker.IsCanDoNow : !Checker.IsCanDoNow; 
            }
        }
    }
}
