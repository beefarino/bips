using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CodeOwls.BIPS.Utility
{
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property)]
    public class XmlFileAttribute : ArgumentTransformationAttribute
    {
        public override object Transform(EngineIntrinsics engineIntrinsics, object inputData)
        {
            var pso = PSObject.AsPSObject(inputData);
            FileInfo file = pso.BaseObject as FileInfo;
            if (null == file)
            {
                string filePath = pso.BaseObject as string;
                if (null != filePath)
                {
                    file = new FileInfo(filePath);
                }
            }

            if (null == file || ! file.Exists )
            {
                return inputData;
            }

            var results = engineIntrinsics.InvokeCommand.InvokeScript("load-packageXml", false, PipelineResultTypes.None, new ArrayList{inputData}, null);
            return results.FirstOrDefault();
        }
    }
}
