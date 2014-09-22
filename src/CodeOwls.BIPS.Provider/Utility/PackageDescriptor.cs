using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS.Utility
{
    public class PackageDescriptor
    {
        private readonly Package _package;

        public PackageDescriptor(Package package, string filePath )
        {
            Location = filePath;
            _package = package;
        }

        public PackageDescriptor(Package package, SsisDbProjectDescriptor dbPackageDescriptor, string path)
        {
            _package = package;
            Location = path;
            ProjectDescriptor = dbPackageDescriptor;
        }

        public string Location { get; private set; }
        public SsisDbProjectDescriptor ProjectDescriptor { get; set; }

        public DTSExecResult Validate(Connections connections, Variables variables, IDTSEvents events, IDTSLogging log)
        {
            return Package.Validate(connections, variables, events, log);
        }

        public DTSExecResult Execute(Connections connections, Variables variables, IDTSEvents events, IDTSLogging log,
                                     object transaction)
        {
            return Package.Execute(connections, variables, events, log, transaction);
        }

        public void SaveToXML(ref XmlDocument doc, XmlNode node, IDTSEvents events)
        {
            Package.SaveToXML(ref doc, node, events);
        }

        public void LoadFromXML(XmlNode node, IDTSEvents events)
        {
            Package.LoadFromXML(node, events);
        }

        public void SuspendExecution()
        {
            Package.SuspendExecution();
        }

        public void ResumeExecution()
        {
            Package.ResumeExecution();
        }

        public void AcceptBreakpointManager(BreakpointManager breakpointManager)
        {
            Package.AcceptBreakpointManager(breakpointManager);
        }

        public void Dispose()
        {
            Package.Dispose();
        }

        public bool IsDefaultLocaleID
        {
            get { return Package.IsDefaultLocaleID; }
        }

        public int LocaleID
        {
            get { return Package.LocaleID; }
            set { Package.LocaleID = value; }
        }

        public DateTime StartTime
        {
            get { return Package.StartTime; }
        }

        public DateTime StopTime
        {
            get { return Package.StopTime; }
        }

        public int ExecutionDuration
        {
            get { return Package.ExecutionDuration; }
        }

        public Variables Variables
        {
            get { return Package.Variables; }
        }

        public VariableDispenser VariableDispenser
        {
            get { return Package.VariableDispenser; }
        }

        public DTSExecStatus ExecutionStatus
        {
            get { return Package.ExecutionStatus; }
        }

        public DTSExecResult ExecutionResult
        {
            get { return Package.ExecutionResult; }
        }

        public bool Disable
        {
            get { return Package.Disable; }
            set { Package.Disable = value; }
        }

        public DTSForcedExecResult ForceExecutionResult
        {
            get { return Package.ForceExecutionResult; }
            set { Package.ForceExecutionResult = value; }
        }

        public bool ForceExecutionValue
        {
            get { return Package.ForceExecutionValue; }
            set { Package.ForceExecutionValue = value; }
        }

        public object ForcedExecutionValue
        {
            get { return Package.ForcedExecutionValue; }
            set { Package.ForcedExecutionValue = value; }
        }

        public bool FailParentOnFailure
        {
            get { return Package.FailParentOnFailure; }
            set { Package.FailParentOnFailure = value; }
        }

        public int MaximumErrorCount
        {
            get { return Package.MaximumErrorCount; }
            set { Package.MaximumErrorCount = value; }
        }

        public DtsContainer Parent
        {
            get { return Package.Parent; }
        }

        public IsolationLevel IsolationLevel
        {
            get { return Package.IsolationLevel; }
            set { Package.IsolationLevel = value; }
        }

        public DTSTransactionOption TransactionOption
        {
            get { return Package.TransactionOption; }
            set { Package.TransactionOption = value; }
        }

        public LoggingOptions LoggingOptions
        {
            get { return Package.LoggingOptions; }
        }

        public DTSLoggingMode LoggingMode
        {
            get { return Package.LoggingMode; }
            set { Package.LoggingMode = value; }
        }

        public bool DelayValidation
        {
            get { return Package.DelayValidation; }
            set { Package.DelayValidation = value; }
        }

        public LogEntryInfos LogEntryInfos
        {
            get { return Package.LogEntryInfos; }
        }

        public string ID
        {
            get { return Package.ID; }
        }

        public string Name
        {
            get { return Package.Name; }
            set { Package.Name = value; }
        }

        public string CreationName
        {
            get { return Package.CreationName; }
        }

        public string Description
        {
            get { return Package.Description; }
            set { Package.Description = value; }
        }

        public bool SuspendRequired
        {
            get { return Package.SuspendRequired; }
            set { Package.SuspendRequired = value; }
        }

        public bool DebugMode
        {
            get { return Package.DebugMode; }
            set { Package.DebugMode = value; }
        }

        public ISite Site
        {
            get { return Package.Site; }
            set { Package.Site = value; }
        }

        public event EventHandler Disposed
        {
            add { Package.Disposed += value; }
            remove { Package.Disposed -= value; }
        }

        public DtsEventHandlers EventHandlers
        {
            get { return Package.EventHandlers; }
        }

        public EventInfos EventInfos
        {
            get { return Package.EventInfos; }
        }

        public bool DisableEventHandlers
        {
            get { return Package.DisableEventHandlers; }
            set { Package.DisableEventHandlers = value; }
        }

        public DTSExecResult Execute()
        {
            return Package.Execute();
        }

        public void ExportConfigurationFile(string str)
        {
            Package.ExportConfigurationFile(str);
        }

        public void ImportConfigurationFile(string str)
        {
            Package.ImportConfigurationFile(str);
        }

        public BreakpointTargets GetBreakpointTargets(IDTSBreakpointSite bpSite, bool onlyEnabled)
        {
            return Package.GetBreakpointTargets(bpSite, onlyEnabled);
        }

        public DTSSignatureStatus CheckSignature()
        {
            return Package.CheckSignature();
        }

        public void LoadUserCertificateByName(string subjectName)
        {
            Package.LoadUserCertificateByName(subjectName);
        }

        public void LoadUserCertificateByHash(byte[] certHash)
        {
            Package.LoadUserCertificateByHash(certHash);
        }

        public void ProcessConfiguration(string sPath, object value)
        {
            Package.ProcessConfiguration(sPath, value);
        }

        public object GetObjectFromPackagePath(string packagePath, out DtsProperty property)
        {
            return Package.GetObjectFromPackagePath(packagePath, out property);
        }

        public EnumReferencedObjects FindReferencedObjects(object o)
        {
            return Package.FindReferencedObjects(o);
        }

        public void RegenerateID()
        {
            Package.RegenerateID();
        }

        public string GetExpression(string propertyName)
        {
            return Package.GetExpression(propertyName);
        }

        public void SetExpression(string propertyName, string expression)
        {
            Package.SetExpression(propertyName, expression);
        }

        public string GetPackagePath()
        {
            return Package.GetPackagePath();
        }

        public string GetExecutionPath()
        {
            return Package.GetExecutionPath();
        }

        public void AddDataTapPoint(string mainPipeIdentification, string pathIdentification, int maxRowCount, string fileName)
        {
            Package.AddDataTapPoint(mainPipeIdentification, pathIdentification, maxRowCount, fileName);
        }

        public void LoadFromXML(string packageXml, IDTSEvents events)
        {
            Package.LoadFromXML(packageXml, events);
        }

        public void SaveToXML(out string packageXml, IDTSEvents events)
        {
            Package.SaveToXML(out packageXml, events);
        }

        public void ComputeExpressions(bool recursive)
        {
            Package.ComputeExpressions(recursive);
        }

        public bool DumpOnAnyError
        {
            get { return Package.DumpOnAnyError; }
            set { Package.DumpOnAnyError = value; }
        }

        public string DumpDescriptor
        {
            get { return Package.DumpDescriptor; }
            set { Package.DumpDescriptor = value; }
        }

        public bool EnableDump
        {
            get { return Package.EnableDump; }
            set { Package.EnableDump = value; }
        }

        public DtsErrors Errors
        {
            get { return Package.Errors; }
        }

        public DtsWarnings Warnings
        {
            get { return Package.Warnings; }
        }

        public Connections Connections
        {
            get { return Package.Connections; }
        }

        public LogProviders LogProviders
        {
            get { return Package.LogProviders; }
        }

        public Configurations Configurations
        {
            get { return Package.Configurations; }
        }

        public Microsoft.SqlServer.Dts.Runtime.Wrapper.IDTSProject100 Project
        {
            get { return Package.Project; }
        }

        public DTSPackageType PackageType
        {
            get { return Package.PackageType; }
            set { Package.PackageType = value; }
        }

        public string CreatorName
        {
            get { return Package.CreatorName; }
            set { Package.CreatorName = value; }
        }

        public string CreatorComputerName
        {
            get { return Package.CreatorComputerName; }
            set { Package.CreatorComputerName = value; }
        }

        public DateTime CreationDate
        {
            get { return Package.CreationDate; }
            set { Package.CreationDate = value; }
        }

        public int MaxConcurrentExecutables
        {
            get { return Package.MaxConcurrentExecutables; }
            set { Package.MaxConcurrentExecutables = value; }
        }

        public DTSPriorityClass PackagePriorityClass
        {
            get { return Package.PackagePriorityClass; }
            set { Package.PackagePriorityClass = value; }
        }

        public ExtendedProperties ExtendedProperties
        {
            get { return Package.ExtendedProperties; }
        }

        public int VersionMajor
        {
            get { return Package.VersionMajor; }
            set { Package.VersionMajor = value; }
        }

        public int VersionMinor
        {
            get { return Package.VersionMinor; }
            set { Package.VersionMinor = value; }
        }

        public int VersionBuild
        {
            get { return Package.VersionBuild; }
            set { Package.VersionBuild = value; }
        }

        public string VersionComments
        {
            get { return Package.VersionComments; }
            set { Package.VersionComments = value; }
        }

        public string VersionGUID
        {
            get { return Package.VersionGUID; }
        }

        public bool EnableConfigurations
        {
            get { return Package.EnableConfigurations; }
            set { Package.EnableConfigurations = value; }
        }

        public X509Certificate CertificateObject
        {
            get { return Package.CertificateObject; }
            set { Package.CertificateObject = value; }
        }

        public long CertificateContext
        {
            get { return Package.CertificateContext; }
            set { Package.CertificateContext = value; }
        }

        public bool CheckSignatureOnLoad
        {
            get { return Package.CheckSignatureOnLoad; }
            set { Package.CheckSignatureOnLoad = value; }
        }

        public string CheckpointFileName
        {
            get { return Package.CheckpointFileName; }
            set { Package.CheckpointFileName = value; }
        }

        public bool EncryptCheckpoints
        {
            get { return Package.EncryptCheckpoints; }
            set { Package.EncryptCheckpoints = value; }
        }

        public bool SaveCheckpoints
        {
            get { return Package.SaveCheckpoints; }
            set { Package.SaveCheckpoints = value; }
        }

        public DTSCheckpointUsage CheckpointUsage
        {
            get { return Package.CheckpointUsage; }
            set { Package.CheckpointUsage = value; }
        }

        public DTSProtectionLevel ProtectionLevel
        {
            get { return Package.ProtectionLevel; }
            set { Package.ProtectionLevel = value; }
        }

        public string PackagePassword
        {
            set { Package.PackagePassword = value; }
        }

        public bool InteractiveMode
        {
            get { return Package.InteractiveMode; }
            set { Package.InteractiveMode = value; }
        }

        public bool OfflineMode
        {
            get { return Package.OfflineMode; }
            set { Package.OfflineMode = value; }
        }

        public bool IgnoreConfigurationsOnLoad
        {
            get { return Package.IgnoreConfigurationsOnLoad; }
            set { Package.IgnoreConfigurationsOnLoad = value; }
        }

        public bool SuppressConfigurationWarnings
        {
            get { return Package.SuppressConfigurationWarnings; }
            set { Package.SuppressConfigurationWarnings = value; }
        }

        public IDTSEvents DesignEvents
        {
            get { return Package.DesignEvents; }
            set { Package.DesignEvents = value; }
        }

        public bool UpdateObjects
        {
            get { return Package.UpdateObjects; }
            set { Package.UpdateObjects = value; }
        }

        public string DesignTimeProperties
        {
            get { return Package.DesignTimeProperties; }
            set { Package.DesignTimeProperties = value; }
        }

        public bool SafeRecursiveProjectPackageExecution
        {
            get { return Package.SafeRecursiveProjectPackageExecution; }
            set { Package.SafeRecursiveProjectPackageExecution = value; }
        }

        public Executables Executables
        {
            get { return Package.Executables; }
        }

        public PrecedenceConstraints PrecedenceConstraints
        {
            get { return Package.PrecedenceConstraints; }
        }

        public DtsProperties Properties
        {
            get { return Package.Properties; }
        }

        public bool HasExpressions
        {
            get { return Package.HasExpressions; }
        }

        public Parameters Parameters
        {
            get { return Package.Parameters; }
        }

        public bool FailPackageOnFailure
        {
            get { return Package.FailPackageOnFailure; }
            set { Package.FailPackageOnFailure = value; }
        }

        public PackageUpgradeOptions PackageUpgradeOptions
        {
            get { return Package.PackageUpgradeOptions; }
            set { Package.PackageUpgradeOptions = value; }
        }

        internal Package Package
        {
            get { return _package; }
        }
        
        public static implicit operator Package(PackageDescriptor p)
        {
            return p.Package;
        }
    }
}