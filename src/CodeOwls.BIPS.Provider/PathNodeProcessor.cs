using System;
using System.Text.RegularExpressions;
using CodeOwls.PowerShell.Paths.Processors;
using CodeOwls.PowerShell.Provider.PathNodes;

namespace CodeOwls.BIPS
{
    public class PathNodeProcessor : PathNodeProcessorBase
    {
        private readonly BipsDrive _drive;

        public PathNodeProcessor(BipsDrive drive)
        {
            _drive = drive;
        }

        static readonly Regex ServerNameRegex = new Regex( @"^[^\\\/]+\\+");

        public override System.Collections.Generic.IEnumerable<INodeFactory> ResolvePath(PowerShell.Provider.PathNodeProcessors.IContext context, string path)
        {
            path = ServerNameRegex.Replace(path, String.Empty);
            return base.ResolvePath(context, path);
        }

        protected override INodeFactory Root
        {
            get
            {
                return new BipsRootNodeFactory(_drive);
            }
        }
    }
}