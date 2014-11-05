using System;
using System.Collections.Generic;
using System.Linq;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS
{
    public class ExecutableCollectionNodeFactory : CollectionNodeFactory<Executable>, INewItem
    {
        private readonly Executables _items;

        public ExecutableCollectionNodeFactory(string collectionName, Executables items, Converter<Executable, INodeFactory> adapter) 
            : base(collectionName, items.Cast<Executable>().ToList(), adapter)
        {
            _items = items;
        }

        public IEnumerable<string> NewItemTypeNames
        {
            get
            {
                return new[]
                {
                    "ActiveXScript",
                    "BulkInsert",
                    "ExecuteProcess",
                    "ExecutePackage",
                    "Exec80Package",
                    "FileSystem",
                    "FTP",
                    "MSMQ",
                    "Pipeline",
                    "Script",
                    "SendMail",
                    "SQL",
                    "TransferStoredProcedures",
                    "TransferLogins",
                    "TransferErrorMessages",
                    "TransferJobs",
                    "TransferObjects",
                    "TransferDatabase",
                    "WebService",
                    "WmiDataReader",
                    "WmiEventWatcher",
                    "XML",
                    "CLSID",
                    "PROGID"
                };
            }
        }
        public object NewItemParameters { get { return null; } }
        public IPathNode NewItem(IContext context, string path, string itemTypeName, object newItemValue)
        {
            var moniker = "STOCK";
            var taskTypeId = itemTypeName + "Task";
            var monikers = new[] { "clsid", "progid" };
            if (monikers.Contains(itemTypeName.ToLowerInvariant()))
            {
                moniker = itemTypeName.ToUpperInvariant();
                taskTypeId = newItemValue.ToString();
            }


            var item = (TaskHost)_items.Add(moniker + ":" + taskTypeId);
            item.Name = path;
            
            var resolvedItem = context.ResolvePath(context.Path);
            return resolvedItem.GetNodeValue();
        }

    }
}