using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS
{
    public class SequenceNodeFactory : NodeFactoryBase
    {
        private readonly Sequence _sequence;

        public SequenceNodeFactory( Sequence sequence)
        {
            _sequence = sequence;
        }

        public override IEnumerable<INodeFactory> GetNodeChildren(IContext context)
        {
            var nodes = new List<INodeFactory>();
            var exes = _sequence.Executables.Cast<Executable>().ToList();

            nodes.AddRange(exes.ConvertAll(c => new ExecutableNodeFactory(c)));

            return nodes;
        }

        public override IPathNode GetNodeValue()
        {
            return new ContainerPathNode( _sequence, Name );
        }

        public override string Name
        {
            get { return _sequence.Name; }
        }
    }

    public class ExecutableNodeFactory : NodeFactoryBase
    {
        private readonly DtsContainer _executable;
        private readonly MainPipe _mainPipe;

        public ExecutableNodeFactory(Executable executable)
        {
            _executable = (DtsContainer)executable;
            
            var host = _executable as TaskHost;
            if (null != host)
            {
                _mainPipe = host.InnerObject as MainPipe;
            }
        }

        public override IEnumerable<INodeFactory> GetNodeChildren(IContext context)
        {
            var nodes = new List<INodeFactory>();
            var seq = _executable as Sequence;
            var foreachloop = _executable as ForEachLoop;
            var forloop = _executable as ForLoop;
            
            if (null != _mainPipe)
            {
                var metadata = _mainPipe.ComponentMetaDataCollection.Cast<IDTSComponentMetaData100>();
                nodes.AddRange(metadata.ToList().ConvertAll(c => new DataFlowComponentNodeFactory(c)));
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