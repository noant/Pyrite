using System;
using System.Xml.Serialization;
using UniActionsClientIntefaces;

namespace UniActionsCore.ScenarioCreating
{
    [Serializable]
    public class ComplexChecker : ICustomChecker, IHasChecker
    {
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
                throw new NotImplementedException();
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

        public bool HasChecker(Type checkerType)
        {
            return false;
        }

        public void Refresh()
        {

        }
    }
}
