using PyriteCore.ScenarioCreation;

namespace PyriteUI.ScenarioCreation
{
    public class WhileViewContext
    {
        private WhileAction _whileAction;

        public WhileViewContext(WhileAction whileAction)
        {
            _whileAction = whileAction;
            ProcessWhileAction();
        }

        public void ProcessWhileAction()
        {
            if (_whileAction == null)
                _whileAction = new WhileAction();
            if (_whileAction.Action == null)
                _whileAction.Action = new ComplexAction();
            if (_whileAction.Checker == null)
                _whileAction.Checker = new ComplexChecker();
        }

        public ComplexAction Action
        {
            get
            {
                ProcessWhileAction();
                return _whileAction.Action;
            }
        }

        public ComplexChecker Checker
        {
            get
            {
                ProcessWhileAction();
                return _whileAction.Checker;
            }
        }
    }
}
