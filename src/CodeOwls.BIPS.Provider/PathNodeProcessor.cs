using System;
using System.IO;
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

        private INodeFactory _root;

        public override System.Collections.Generic.IEnumerable<INodeFactory> ResolvePath(PowerShell.Provider.PathNodeProcessors.IContext context, string path)
        {
            if (null != context.Drive && !String.IsNullOrEmpty(context.Drive.Root))
            {
                string fileOrServerPath = Regex.Replace(path, @"^[^::]+::", String.Empty);

                var re = new Regex("^.*(" + Regex.Escape(context.Drive.Root) + ")(.*)$", RegexOptions.IgnoreCase);
                var matches = re.Match(path);
                fileOrServerPath = matches.Groups[1].Value;
                path = matches.Groups[2].Value;

                if (File.Exists(fileOrServerPath) || Directory.Exists(fileOrServerPath))
                {
                    _root = new BipsFileRootNodeFactory(_drive, fileOrServerPath);
                }
                else
                {
                    _root = new BipsRootNodeFactory(_drive); 
                }
            }
            
            return base.ResolvePath(context, path);
        }

        protected override INodeFactory Root
        {
            get
            {
                return _root;
            }
        }
    }
}