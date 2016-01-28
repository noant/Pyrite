using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UniActionsClientIntefaces;

namespace UniActionsCore
{
    public class ModulesControl
    {
        private static readonly string _uniActionsStandart = "UniStandartActions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        
        public Uni Uni { get; internal set; }

        private List<Type> _customActions;
        public IEnumerable<Type> CustomActions
        {
            get
            {
                return _customActions.ToArray();
            }
        }

        private List<Type> _customCheckers;
        public IEnumerable<Type> CustomCheckers
        {
            get
            {
                return _customCheckers.ToArray();
            }
        }

        public Result<string> GetViewName(Type type)
        {
            var result = new Result<string>();
            try
            {
                result.Value = type.GetProperty("Name").GetValue(type.GetConstructor(new Type[0]).Invoke(new object[0])).ToString();
            }
            catch (Exception e)
            {
                result.AddException(e);
            }
            return result;
        }

        public Result<ICustomAction> CreateActionInstance(Type type)
        {
            var result = new Result<ICustomAction>();
            try
            {
                var action = (ICustomAction)type.GetConstructor(new Type[0]).Invoke(new object[0]);
                if (action.BeginUserSettings())
                    result.Value = action;
            }
            catch (Exception e)
            {
                result.AddException(e);
            }

            return result;
        }
        public Result<ICustomChecker> CreateCheckerInstance(Type type)
        {
            var result = new Result<ICustomChecker>();
            try
            {
                var checker = (ICustomChecker)type.GetConstructor(new Type[0]).Invoke(new object[0]);
                if (checker.BeginUserSettings())
                    result.Value = checker;
            }
            catch (Exception e)
            {
                result.AddException(e);
            }
            return result;
        }

        internal VoidResult Initialize()
        {
            var result = new VoidResult();

            try
            {
                if (_customActions == null)
                    _customActions = new List<Type>();
                else _customActions.Clear();

                if (_customCheckers == null)
                    _customCheckers = new List<Type>();
                else _customCheckers.Clear();

                RegisterAction(new Uri(typeof(UniStandartActions.NotUsedClass).Assembly.CodeBase).LocalPath);
                RegisterChecker(new Uri(typeof(UniStandartActions.NotUsedClass).Assembly.CodeBase).LocalPath);
            }
            catch (Exception e)
            {
                result.AddException(e);
            }
            return result;
        }

        internal void Clear()
        {
            Initialize();
        }

        public bool IsStandart(Type type)
        {
            return type.Assembly.FullName == _uniActionsStandart;
        }

        public Result<IEnumerable<Type>> RegisterAction(string filename)
        {
            var result = new Result<IEnumerable<Type>>();
            IEnumerable<Type> types=null;
            try
            {
                var assembly = Assembly.LoadFrom(filename);
                types = assembly
                    .GetTypes()
                    .Where(x => x.GetInterfaces()
                        .Contains(typeof(ICustomAction)));
            }
            catch (Exception e)
            {
                result.AddException(e);
            }

            var addedTypes = types.Where(x => CanRegisterAction(x)).ToList();
            _customActions.AddRange(addedTypes);
            result.Value = addedTypes;
            return result;
        }

        public bool CanRegisterAction(Type actionType)
        {
            Exception exception = null;

            if (actionType.IsInterface)
                exception = new Exception("Cannot add interface");
            else if (actionType.IsAbstract)
                exception = new Exception("Cannot add abstract class");
            else if (!actionType.GetInterfaces().Contains(typeof(ICustomAction)))
                exception = new Exception("Type has no ICustomAction interface");
            else if (_customActions.Where(x => x.FullName == actionType.FullName).Count() != 0)
                exception = new Exception("Is exist");
                        
            return exception == null;
        }

        public Result<IEnumerable<Type>> RegisterChecker(string filename)
        {
            var result = new Result<IEnumerable<Type>>();
            IEnumerable<Type> types = null;
            try
            {
                var assembly = Assembly.LoadFrom(filename);
                types = assembly
                    .GetTypes()
                    .Where(x => x.GetInterfaces()
                        .Contains(typeof(ICustomChecker)));
            }
            catch (Exception e)
            {
                result.AddException(e);
            }

            var addedTypes = types.Where(x => CanRegisterChecker(x));
            _customCheckers.AddRange(addedTypes);
            result.Value = addedTypes;
            return result;
        }

        public bool CanRegisterChecker(Type checkerType)
        {
            Exception exception = null;

            if (checkerType.IsInterface)
                exception = new Exception("Cannot add interface");
            else if (checkerType.IsAbstract)
                exception = new Exception("Cannot add abstract class");
            else if (!checkerType.GetInterfaces().Contains(typeof(ICustomChecker)))
                exception = new Exception("Type has no ICustomAction interface");
            else if (_customActions.Where(x => x.FullName == checkerType.FullName).Count() != 0)
                exception = new Exception("Is exist");

            return exception == null;
        }

        public VoidResult RemoveChecker(Type checkerType)
        {
            var result = new VoidResult();
            try
            {
                lock (Uni.TasksPool.ActionItems)
                {
                    var assemblyName = checkerType.Assembly.FullName;
                    _customCheckers.RemoveAll(x => x.Assembly.FullName == assemblyName);
                    Uni.TasksPool.RemoveAll(x => x.Checker.GetType().Assembly.FullName == assemblyName);
                }
            }
            catch (Exception e)
            {
                result.AddException(e);
            }
            return result;
        }

        public VoidResult RemoveAction(Type actionType)
        {
            var result = new VoidResult();
            try
            {
                lock (Uni.TasksPool.ActionItems)
                {
                    var assemblyName = actionType.Assembly.FullName;
                    _customActions.RemoveAll(x => x.Assembly.FullName == assemblyName);
                    Uni.TasksPool.RemoveAll(x => x.Action.GetType().Assembly.FullName == assemblyName);
                }
            }
            catch (Exception e)
            {
                result.AddException(e);
            }
            return result;
        }

        public static ICustomAction Clone(ICustomAction action)
        {
            return (ICustomAction)HierarchicalData.Helper.CloneHierarchicalObject(action);
        }

        public static ICustomChecker Clone(ICustomChecker action)
        {
            return (ICustomChecker)HierarchicalData.Helper.CloneHierarchicalObject(action);
        }
    }
}
