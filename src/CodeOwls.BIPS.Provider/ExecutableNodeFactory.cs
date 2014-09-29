using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Runtime.InteropServices;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS
{
      
    public class ExecutableNodeFactory : NodeFactoryBase, IRemoveItem
    {
        private readonly Executables _collection;
        private readonly DtsContainer _executable;
        private readonly PSObject _psExecutable;
        private readonly TaskHost _host;
        private readonly Sequence _seq;
        private readonly ForEachLoop _foreachloop;
        private readonly ForLoop _forloop;
        private readonly MainPipe _mainPipe;

        public ExecutableNodeFactory(Executable executable,Executables collection)
        {
            _collection = collection;
            _executable = (DtsContainer)executable;
            _host = _executable as TaskHost;
            _seq = _executable as Sequence;
            _foreachloop = _executable as ForEachLoop;
            _forloop = _executable as ForLoop;
            _psExecutable = PSObject.AsPSObject(_executable);

            if (null != _host)
            {
                _psExecutable.Properties.Add( new PSNoteProperty( "IsTaskHost", true ));
                _mainPipe = _host.InnerObject as MainPipe;
            }
            if (null != _mainPipe)
            {
                _psExecutable.Properties.Add(new PSNoteProperty("IsDataFlow", true));
            }
            if (null != _seq)
            {
                _psExecutable.Properties.Add(new PSNoteProperty("IsSequence", true));
            }
            if (null != _foreachloop)
            {
                _psExecutable.Properties.Add(new PSNoteProperty("IsForEachLoop", true));
            }
            if (null != _forloop)
            {
                _psExecutable.Properties.Add(new PSNoteProperty("IsForLoop", true));
            }
        }

        public override IEnumerable<INodeFactory> GetNodeChildren(IContext context)
        {
            var nodes = new List<INodeFactory>();
           
            if (null != _mainPipe)
            {
                var metadata = _mainPipe.ComponentMetaDataCollection.Cast<IDTSComponentMetaData100>();
                nodes.AddRange(metadata.ToList().ConvertAll(c => new DataFlowComponentNodeFactory(c, _mainPipe.ComponentMetaDataCollection)));
            }
            else if (null != _host)
            {
                nodes.Add( new TaskNodeFactory(_host.InnerObject as Task) );
            }
            else if (null != _seq)
            {
                nodes.AddRange(_seq.Executables.Cast<Executable>().ToList().ConvertAll( e=> new ExecutableNodeFactory(e,_seq.Executables)));
            }
            else if (null != _foreachloop)
            {
                nodes.AddRange(_foreachloop.Executables.Cast<Executable>().ToList().ConvertAll(e => new ExecutableNodeFactory(e,_foreachloop.Executables)));
            }
            else if (null != _forloop)
            {
                nodes.AddRange(_forloop.Executables.Cast<Executable>().ToList().ConvertAll(e => new ExecutableNodeFactory(e,_forloop.Executables)));
            }

            return nodes;
        }

        public override IPathNode GetNodeValue()
        {
            return new ContainerPathNode( _psExecutable, Name);
        }

        public override string Name
        {
            get { return _executable.Name; }
        }

        public object RemoveItemParameters { get; private set; }
        public void RemoveItem(IContext context, string path, bool recurse)
        {
            if (null == _collection)
            {
                return;
            }

            _collection.Remove( _executable );
        }
    }
}