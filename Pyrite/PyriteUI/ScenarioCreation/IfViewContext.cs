using PyriteCore.ScenarioCreation;

namespace PyriteUI.ScenarioCreation
{
    public class IfViewContext
    {
        private IfAction _ifAction;

        public IfViewContext(IfAction ifAction)
        {
            _ifAction = ifAction;
            ProcessIfAction();
        }

        public void ProcessIfAction()
        {
            if (_ifAction == null)
                _ifAction = new IfAction();
            if (_ifAction.ActionIf == null)
                _ifAction.ActionIf = new ComplexAction();
            if (_ifAction.ActionElse == null)
                _ifAction.ActionElse = new ComplexAction();
            if (_ifAction.Checker == null)
                _ifAction.Checker = new ComplexChecker();
        }

        public ComplexAction If
        {
            get
            {
                ProcessIfAction();
                return _ifAction.ActionIf;
            }
        }

        public ComplexAction Else
        {
            get
            {
                ProcessIfAction();
                return _ifAction.ActionElse;
            }
        }

        public ComplexChecker Checker
        {
            get
            {
                ProcessIfAction();
                return _ifAction.Checker;
            }
        }
    }
}
