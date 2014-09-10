using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS
{
    public class TaskInfoNodeFactory : NodeFactoryBase
    {
        private readonly TaskInfo _taskInfo;

        public TaskInfoNodeFactory(TaskInfo taskInfo)
        {
            _taskInfo = taskInfo;
        }

        public override IPathNode GetNodeValue()
        {
            return new LeafPathNode( _taskInfo, Name );
        }

        public override string Name
        {
            get { return _taskInfo.Name; }
        }
    }
}