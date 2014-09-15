using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS
{
    public class ExecutableNodeFactory : NodeFactoryBase
    {
        private readonly DtsContainer _executable;

        public ExecutableNodeFactory(Executable executable)
        {
            _executable = (DtsContainer)executable;
            
        }

        public override IEnumerable<INodeFactory> GetNodeChildren(IContext context)
        {
            var nodes = new List<INodeFactory>();

            var host = _executable as TaskHost;
            var seq = _executable as Sequence;
            var foreachloop = _executable as ForEachLoop;
            var forloop = _executable as ForLoop; 
            MainPipe mainPipe = null;

            if (null != host)
            {
                mainPipe = host.InnerObject as MainPipe;
            }
            
            if (null != mainPipe)
            {
                var metadata = mainPipe.ComponentMetaDataCollection.Cast<IDTSComponentMetaData100>();
                nodes.AddRange(metadata.ToList().ConvertAll(c => new DataFlowComponentNodeFactory(c)));
            }
            else if (null != host)
            {
                nodes.Add( new TaskNodeFactory(host.InnerObject as Task) );
            }
            else if (null != seq)
            {
                nodes.AddRange(seq.Executables.Cast<Executable>().ToList().ConvertAll( e=> new ExecutableNodeFactory(e)));
            }
            else if (null != foreachloop)
            {
                nodes.AddRange(foreachloop.Executables.Cast<Executable>().ToList().ConvertAll(e => new ExecutableNodeFactory(e)));
            }
            else if (null != forloop)
            {
                nodes.AddRange(forloop.Executables.Cast<Executable>().ToList().ConvertAll(e => new ExecutableNodeFactory(e)));
            }

            return nodes;
        }

        public override IPathNode GetNodeValue()
        {
            return new ContainerPathNode( _executable, Name);
        }

        public override string Name
        {
            get { return _executable.Name; }
        }
    }
}