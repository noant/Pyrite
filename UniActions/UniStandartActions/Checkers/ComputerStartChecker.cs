using System;
using System.Xml.Serialization;
using UniActionsClientIntefaces;

namespace UniStandartActions.Checkers
{
    [Serializable]
    public class ComputerStartChecker : ICustomChecker
    {
        private bool _started;

        [XmlIgnore]
        public bool IsCanDoNow
        {
            get
            {
                if (!_started)
                {
                    _started = true;
                    return true;
                }
                else return false;
            }
        }

        [XmlIgnore]
        public bool AllowUserSettings { get { return false; } }

        [XmlIgnore]
        public string Name
        {
            get { return "При включении компьютера";  }
        }

        public bool BeginUserSettings()
        {
            return true;
        }

        public void Refresh() { }
    }
}
