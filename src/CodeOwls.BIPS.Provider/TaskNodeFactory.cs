using System;
using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS
{
    public class TaskNodeFactory : NodeFactoryBase
    {
        private readonly Task _task;

        public TaskNodeFactory( Task task )
        {
            _task = task;
        }

        public override IPathNode GetNodeValue()
        {
            return new LeafPathNode( _task ,Name );
        }

        public override string Name
        {
            get { return _task.GetType().Name.Replace("Task", String.Empty); }
        }
    }
}