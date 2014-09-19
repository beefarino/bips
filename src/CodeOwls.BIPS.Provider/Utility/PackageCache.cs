using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using Microsoft.SqlServer.Dts.Runtime;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Management.Automation;

namespace CodeOwls.BIPS.Utility
{
    class PackageCache
    {
        readonly IDictionary<string, PackageDescriptor> _cache;
        readonly BipsDrive _drive;

        public PackageCache(BipsDrive drive)
        {
            _drive = drive;
            _cache = new Dictionary<string, PackageDescriptor>();
        }

        public PackageDescriptor GetPackage(string path, IContext context)
        {
            return GetPackage(path, context, null);
        }

        public PackageDescriptor GetPackage(string path, IContext context, ProgressRecord progress)
        {
            path = path.ToLowerInvariant();
            if (!_cache.ContainsKey(path))
            {
                var events = new NullEvents();
                context.WriteDebug("Loading " + path);
                if (null != progress)
                {
                    progress.CurrentOperation = path;
                    context.WriteProgress(progress);
                }

                try
                {
                    var package = _drive.Application.LoadPackage(path, events, true);
                    var descriptor = new PackageDescriptor(package, path);
                    _cache.Add(path, descriptor);
                }
                catch(Exception e)
                {
                    /*if (!context.Force)
                    {
                        throw;
                    }*/

                    _cache.Add( path, null );
                    var errorRecord = new ErrorRecord( e, "PackageCache.GetPackage", ErrorCategory.ReadError, path);
                    context.WriteError( errorRecord );
                }
                finally
                {
                    context.WriteDebug(events.ToString());
                    context.WriteDebug("Done loading " + path);
                }
            }
            return _cache[path];
        }

        public string GetKey(FileInfo f)
        {
            return f.FullName;
        }

        class NullEvents : IDTSEvents
        {
            readonly StringBuilder _builder;
            public NullEvents()
            {
                _builder = new StringBuilder();
            }

            public override string ToString()
            {
                return _builder.ToString();
            }

            public void OnBreakpointHit(IDTSBreakpointSite breakpointSite, BreakpointTarget breakpointTarget)
            {
                
            }

            public void OnCustomEvent(TaskHost taskHost, string eventName, string eventText, ref object[] arguments, string subComponent, ref bool fireAgain)
            {
                fireAgain = false;
            }

            public bool OnError(DtsObject source, int errorCode, string subComponent, string description, string helpFile, int helpContext, string idofInterfaceWithError)
            {
                _builder.AppendLine(String.Format(
                    "[{3}] ERROR: [{0}] [{1}] [{2}]", errorCode, subComponent, description, DateTime.Now.ToLongTimeString()
                    ));

                return false;
            }

            public void OnExecutionStatusChanged(Executable exec, DTSExecStatus newStatus, ref bool fireAgain)
            {
                fireAgain = false;
            }

            public void OnInformation(DtsObject source, int informationCode, string subComponent, string description, string helpFile, int helpContext, string idofInterfaceWithError, ref bool fireAgain)
            {
                _builder.AppendLine(String.Format(
                    "[{3}] INFO: [{0}] [{1}] [{2}]", informationCode, subComponent, description, DateTime.Now.ToLongTimeString()
                    ));

            }

            public void OnPostExecute(Executable exec, ref bool fireAgain)
            {
                fireAgain = false;
            }

            public void OnPostValidate(Executable exec, ref bool fireAgain)
            {
                fireAgain = false;
            }

            public void OnPreExecute(Executable exec, ref bool fireAgain)
            {
                fireAgain = false;
            }

            public void OnPreValidate(Executable exec, ref bool fireAgain)
            {
                fireAgain = false;
            }

            public void OnProgress(TaskHost taskHost, string progressDescription, int percentComplete, int progressCountLow, int progressCountHigh, string subComponent, ref bool fireAgain)
            {
                _builder.AppendLine(String.Format(
                                    "[{2}] PROGRESS: [{0}]% [{1}]", percentComplete, subComponent, DateTime.Now.ToLongTimeString()
                                    ));
            }

            public bool OnQueryCancel()
            {
                return false;
            }

            public void OnTaskFailed(TaskHost taskHost)
            {
                
            }

            public void OnVariableValueChanged(DtsContainer DtsContainer, Variable variable, ref bool fireAgain)
            {
                fireAgain = false;
            }

            public void OnWarning(DtsObject source, int warningCode, string subComponent, string description, string helpFile, int helpContext, string idofInterfaceWithError)
            {
                _builder.AppendLine(String.Format(
                    "[{3}] WARNING: [{0}] [{1}] [{2}]", warningCode, subComponent, description
                    ));

            }
        }
    }
}
