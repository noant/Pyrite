using PyriteClientIntefaces;
using PyriteCore.ScenarioCreation;
using System.Collections.Generic;

namespace PyriteUI.ScenarioCreation
{
    public class ComplexActionViewContext
    {
        public ComplexActionViewContext(ComplexAction action)
        {
            _action = action;
            if (_action == null)
                _action = new ComplexAction();
        }

        public ComplexActionViewContext() : this(null) { }

        private ComplexAction _action;

        public IEnumerable<ActionBag> AllActions
        {
            get
            {
                return _action.ActionBags;
            }
        }

        public ComplexAction Action
        {
            get
            {
                return _action;
            }
            set
            {
                _action = value;
            }
        }

        public ActionBag AddIfAction()
        {
            return AddAction<IfAction>();
        }

        public ActionBag AddAction()
        {
            return AddAction<DoNothingAction>();
        }

        public ActionBag AddWhileAction()
        {
            return AddAction<WhileAction>();
        }

        public ActionBag AddAction<T>() where T : ICustomAction, new()
        {
            var actionBag = new ActionBag()
            {
                Action = new T()
            };
            _action.ActionBags.Add(actionBag);
            return actionBag;
        }

        public void RemoveAction(ICustomAction action)
        {
            this._action.ActionBags.RemoveAll(x => x.Action.Equals(action));
        }
    }
}
