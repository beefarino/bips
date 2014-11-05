using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS
{
    public class PrecedenceConstraintCollectionNodeFactory : CollectionNodeFactory<PrecedenceConstraint>, INewItem
    {
        private readonly PrecedenceConstraints _items;
        private readonly Executables _executables;

        public PrecedenceConstraintCollectionNodeFactory(string collectionName, PrecedenceConstraints items, Executables executables, Converter<PrecedenceConstraint, INodeFactory> adapter)
            : base(collectionName, items.Cast<PrecedenceConstraint>().ToList(), adapter)
        {
            _items = items;
            _executables = executables;
        }

        private const string Success = "Success";
        private const string Failure = "Failure";
        private const string Completion = "Completion";
        private const string Cancel = "Cancel";
        public IEnumerable<string> NewItemTypeNames
        {
            get
            {
                return new[]
                {
                    Success, Failure, Completion, Cancel     
                };
            }
        }
        public object NewItemParameters { get; private set; }
        public IPathNode NewItem(IContext context, string path, string itemTypeName, object newItemValue)
        {
            Executable from = null;
            Executable to = null;

            string fromName = null;
            string toName = null;

            var map = new Dictionary<string, DTSExecResult>(StringComparer.InvariantCultureIgnoreCase)
            {
                {Success, DTSExecResult.Success},
                {Failure, DTSExecResult.Failure},
                {Completion, DTSExecResult.Completion},
                {Cancel, DTSExecResult.Canceled}
            };

            if (! map.ContainsKey(itemTypeName))
            {
                throw new ArgumentException( String.Format( 
                    @"The specified item type '{0}' is not valid.  Please use one of the following values:
  {1}
  {2}
  {3}
  {4}
", 
                    itemTypeName, Success,Failure,Completion,Cancel));
            }

            var valueType = newItemValue.GetType();
            if (valueType == typeof (string))
            {
                GetConstraintTaskNames(newItemValue, out fromName, out toName);
            }
            else if (valueType == typeof (object[]))
            {
                var names = (object[]) newItemValue;
                fromName = names[0].ToString();
                toName = names[1].ToString();
            }

            var exes = _executables.Cast<TaskHost>().ToList();
            from = exes.FirstOrDefault(e => StringComparer.InvariantCultureIgnoreCase.Equals(fromName, e.Name));
            to = exes.FirstOrDefault(e => StringComparer.InvariantCultureIgnoreCase.Equals(toName, e.Name));

            if (null == from)
            {
                throw new ArgumentException( "The task name '" + fromName + "' could not be found in the executables for this component");
            }
            if (null == to)
            {
                throw new ArgumentException("The task name '" + toName + "' could not be found in the executables for this component");
            }

            var constraint = _items.Add(from, to);
            constraint.Name = path;

            constraint.Value = map[itemTypeName];
            var resolvedItem = context.ResolvePath(context.Path);
            return resolvedItem.GetNodeValue();
        }

        private static void GetConstraintTaskNames(object newItemValue, out string fromName, out string toName)
        {
            var drawing = newItemValue.ToString();
            // my from task -> my to task
            // task <- task
            // task to task
            // task from task
            var re = new Regex(@"(?<firstTaskName>[\w\s]+?)\s*((?<to>to|.?>)|(?<from>)from|<.?)\s*(?<secondTaskName>[\w\s]+)");

            var matches = re.Match(drawing);
            if (! matches.Success)
            {
                throw new ArgumentException(
                    @"The value parameter is in an incorrect format.  Please specify the constraint relationship between two tasks:
Predecence -> Constrained 
Constrained <- Predecence
Predecence to Constrained 
Constrained from Predecence
");
            }

            fromName = matches.Groups["firstTaskName"].Value;
            toName = matches.Groups["secondTaskName"].Value;

            if (matches.Groups["from"].Success)
            {
                var swap = toName;
                toName = fromName;
                fromName = swap;
            }
        }
    }
}