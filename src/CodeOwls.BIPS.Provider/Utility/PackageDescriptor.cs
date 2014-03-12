using System;
using System.ComponentModel;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS.Utility
{
    public class PackageDescriptor
    {
        private readonly Package _package;

        public PackageDescriptor(Package package, string location )
        {
            Location = location;
            _package = package;
        }

        public string Location { get; private set; }

        public DTSExecResult Validate(Connections connections, Variables variables, IDTSEvents events, IDTSLogging log)
        {
            return _package.Validate(connections, variables, events, log);
        }

        public DTSExecResult Execute(Connections connections, Variables variables, IDTSEvents events, IDTSLogging log,
                                     object transaction)
        {
            return _package.Execute(connections, variables, events, log, transaction);
        }

        public void SaveToXML(ref XmlDocument doc, XmlNode node, IDTSEvents events)
        {
            _package.SaveToXML(ref doc, node, events);
        }

        public void LoadFromXML(XmlNode node, IDTSEvents events)
        {
            _package.LoadFromXML(node, events);
        }

        public void SuspendExecution()
        {
            _package.SuspendExecution();
        }

        public void ResumeExecution()
        {
            _package.ResumeExecution();
        }

        public void AcceptBreakpointManager(BreakpointManager breakpointManager)
        {
            _package.AcceptBreakpointManager(breakpointManager);
        }

        public void Dispose()
        {
            _package.Dispose();
        }

        public bool IsDefaultLocaleID
        {
            get { return _package.IsDefaultLocaleID; }
        }

        public int LocaleID
        {
            get { return _package.LocaleID; }
            set { _package.LocaleID = value; }
        }

        public DateTime StartTime
        {
            get { return _package.StartTime; }
        }

        public DateTime StopTime
        {
            get { return _package.StopTime; }
        }

        public int ExecutionDuration
        {
            get { return _package.ExecutionDuration; }
        }

        public Variables Variables
        {
            get { return _package.Variables; }
        }

        public VariableDispenser VariableDispenser
        {
            get { return _package.VariableDispenser; }
        }

        public DTSExecStatus ExecutionStatus
        {
            get { return _package.ExecutionStatus; }
        }

        public DTSExecResult ExecutionResult
        {
            get { return _package.ExecutionResult; }
        }

        public bool Disable
        {
            get { return _package.Disable; }
            set { _package.Disable = value; }
        }

        public DTSForcedExecResult ForceExecutionResult
        {
            get { return _package.ForceExecutionResult; }
            set { _package.ForceExecutionResult = value; }
        }

        public bool ForceExecutionValue
        {
            get { return _package.ForceExecutionValue; }
            set { _package.ForceExecutionValue = value; }
        }

        public object ForcedExecutionValue
        {
            get { return _package.ForcedExecutionValue; }
            set { _package.ForcedExecutionValue = value; }
        }

        public bool FailParentOnFailure
        {
            get { return _package.FailParentOnFailure; }
            set { _package.FailParentOnFailure = value; }
        }

        public int MaximumErrorCount
        {
            get { return _package.MaximumErrorCount; }
            set { _package.MaximumErrorCount = value; }
        }

        public DtsContainer Parent
        {
            get { return _package.Parent; }
        }

        public IsolationLevel IsolationLevel
        {
            get { return _package.IsolationLevel; }
            set { _package.IsolationLevel = value; }
        }

        public DTSTransactionOption TransactionOption
        {
            get { return _package.TransactionOption; }
            set { _package.TransactionOption = value; }
        }

        public LoggingOptions LoggingOptions
        {
            get { return _package.LoggingOptions; }
        }

        public DTSLoggingMode LoggingMode
        {
            get { return _package.LoggingMode; }
            set { _package.LoggingMode = value; }
        }

        public bool DelayValidation
        {
            get { return _package.DelayValidation; }
            set { _package.DelayValidation = value; }
        }

        public LogEntryInfos LogEntryInfos
        {
            get { return _package.LogEntryInfos; }
        }

        public string ID
        {
            get { return _package.ID; }
        }

        public string Name
        {
            get { return _package.Name; }
            set { _package.Name = value; }
        }

        public string CreationName
        {
            get { return _package.CreationName; }
        }

        public string Description
        {
            get { return _package.Description; }
            set { _package.Description = value; }
        }

        public bool SuspendRequired
        {
            get { return _package.SuspendRequired; }
            set { _package.SuspendRequired = value; }
        }

        public bool DebugMode
        {
            get { return _package.DebugMode; }
            set { _package.DebugMode = value; }
        }

        public ISite Site
        {
            get { return _package.Site; }
            set { _package.Site = value; }
        }

        public event EventHandler Disposed
        {
            add { _package.Disposed += value; }
            remove { _package.Disposed -= value; }
        }

        public DtsEventHandlers EventHandlers
        {
            get { return _package.EventHandlers; }
        }

        public EventInfos EventInfos
        {
            get { return _package.EventInfos; }
        }

        public bool DisableEventHandlers
        {
            get { return _package.DisableEventHandlers; }
            set { _package.DisableEventHandlers = value; }
        }

        public DTSExecResult Execute()
        {
            return _package.Execute();
        }

        public void ExportConfigurationFile(string str)
        {
            _package.ExportConfigurationFile(str);
        }

        public void ImportConfigurationFile(string str)
        {
            _package.ImportConfigurationFile(str);
        }

        public BreakpointTargets GetBreakpointTargets(IDTSBreakpointSite bpSite, bool onlyEnabled)
        {
            return _package.GetBreakpointTargets(bpSite, onlyEnabled);
        }

        public DTSSignatureStatus CheckSignature()
        {
            return _package.CheckSignature();
        }

        public void LoadUserCertificateByName(string subjectName)
        {
            _package.LoadUserCertificateByName(subjectName);
        }

        public void LoadUserCertificateByHash(byte[] certHash)
        {
            _package.LoadUserCertificateByHash(certHash);
        }

        public void ProcessConfiguration(string sPath, object value)
        {
            _package.ProcessConfiguration(sPath, value);
        }

        public object GetObjectFromPackagePath(string packagePath, out DtsProperty property)
        {
            return _package.GetObjectFromPackagePath(packagePath, out property);
        }

        public EnumReferencedObjects FindReferencedObjects(object o)
        {
            return _package.FindReferencedObjects(o);
        }

        public void RegenerateID()
        {
            _package.RegenerateID();
        }

        public string GetExpression(string propertyName)
        {
            return _package.GetExpression(propertyName);
        }

        public void SetExpression(string propertyName, string expression)
        {
            _package.SetExpression(propertyName, expression);
        }

        public string GetPackagePath()
        {
            return _package.GetPackagePath();
        }

        public string GetExecutionPath()
        {
            return _package.GetExecutionPath();
        }

        public void AddDataTapPoint(string mainPipeIdentification, string pathIdentification, int maxRowCount, string fileName)
        {
            _package.AddDataTapPoint(mainPipeIdentification, pathIdentification, maxRowCount, fileName);
        }

        public void LoadFromXML(string packageXml, IDTSEvents events)
        {
            _package.LoadFromXML(packageXml, events);
        }

        public void SaveToXML(out string packageXml, IDTSEvents events)
        {
            _package.SaveToXML(out packageXml, events);
        }

        public void ComputeExpressions(bool recursive)
        {
            _package.ComputeExpressions(recursive);
        }

        public bool DumpOnAnyError
        {
            get { return _package.DumpOnAnyError; }
            set { _package.DumpOnAnyError = value; }
        }

        public string DumpDescriptor
        {
            get { return _package.DumpDescriptor; }
            set { _package.DumpDescriptor = value; }
        }

        public bool EnableDump
        {
            get { return _package.EnableDump; }
            set { _package.EnableDump = value; }
        }

        public DtsErrors Errors
        {
            get { return _package.Errors; }
        }

        public DtsWarnings Warnings
        {
            get { return _package.Warnings; }
        }

        public Connections Connections
        {
            get { return _package.Connections; }
        }

        public LogProviders LogProviders
        {
            get { return _package.LogProviders; }
        }

        public Configurations Configurations
        {
            get { return _package.Configurations; }
        }

        public Microsoft.SqlServer.Dts.Runtime.Wrapper.IDTSProject100 Project
        {
            get { return _package.Project; }
        }

        public DTSPackageType PackageType
        {
            get { return _package.PackageType; }
            set { _package.PackageType = value; }
        }

        public string CreatorName
        {
            get { return _package.CreatorName; }
            set { _package.CreatorName = value; }
        }

        public string CreatorComputerName
        {
            get { return _package.CreatorComputerName; }
            set { _package.CreatorComputerName = value; }
        }

        public DateTime CreationDate
        {
            get { return _package.CreationDate; }
            set { _package.CreationDate = value; }
        }

        public int MaxConcurrentExecutables
        {
            get { return _package.MaxConcurrentExecutables; }
            set { _package.MaxConcurrentExecutables = value; }
        }

        public DTSPriorityClass PackagePriorityClass
        {
            get { return _package.PackagePriorityClass; }
            set { _package.PackagePriorityClass = value; }
        }

        public ExtendedProperties ExtendedProperties
        {
            get { return _package.ExtendedProperties; }
        }

        public int VersionMajor
        {
            get { return _package.VersionMajor; }
            set { _package.VersionMajor = value; }
        }

        public int VersionMinor
        {
            get { return _package.VersionMinor; }
            set { _package.VersionMinor = value; }
        }

        public int VersionBuild
        {
            get { return _package.VersionBuild; }
            set { _package.VersionBuild = value; }
        }

        public string VersionComments
        {
            get { return _package.VersionComments; }
            set { _package.VersionComments = value; }
        }

        public string VersionGUID
        {
            get { return _package.VersionGUID; }
        }

        public bool EnableConfigurations
        {
            get { return _package.EnableConfigurations; }
            set { _package.EnableConfigurations = value; }
        }

        public X509Certificate CertificateObject
        {
            get { return _package.CertificateObject; }
            set { _package.CertificateObject = value; }
        }

        public long CertificateContext
        {
            get { return _package.CertificateContext; }
            set { _package.CertificateContext = value; }
        }

        public bool CheckSignatureOnLoad
        {
            get { return _package.CheckSignatureOnLoad; }
            set { _package.CheckSignatureOnLoad = value; }
        }

        public string CheckpointFileName
        {
            get { return _package.CheckpointFileName; }
            set { _package.CheckpointFileName = value; }
        }

        public bool EncryptCheckpoints
        {
            get { return _package.EncryptCheckpoints; }
            set { _package.EncryptCheckpoints = value; }
        }

        public bool SaveCheckpoints
        {
            get { return _package.SaveCheckpoints; }
            set { _package.SaveCheckpoints = value; }
        }

        public DTSCheckpointUsage CheckpointUsage
        {
            get { return _package.CheckpointUsage; }
            set { _package.CheckpointUsage = value; }
        }

        public DTSProtectionLevel ProtectionLevel
        {
            get { return _package.ProtectionLevel; }
            set { _package.ProtectionLevel = value; }
        }

        public string PackagePassword
        {
            set { _package.PackagePassword = value; }
        }

        public bool InteractiveMode
        {
            get { return _package.InteractiveMode; }
            set { _package.InteractiveMode = value; }
        }

        public bool OfflineMode
        {
            get { return _package.OfflineMode; }
            set { _package.OfflineMode = value; }
        }

        public bool IgnoreConfigurationsOnLoad
        {
            get { return _package.IgnoreConfigurationsOnLoad; }
            set { _package.IgnoreConfigurationsOnLoad = value; }
        }

        public bool SuppressConfigurationWarnings
        {
            get { return _package.SuppressConfigurationWarnings; }
            set { _package.SuppressConfigurationWarnings = value; }
        }

        public IDTSEvents DesignEvents
        {
            get { return _package.DesignEvents; }
            set { _package.DesignEvents = value; }
        }

        public bool UpdateObjects
        {
            get { return _package.UpdateObjects; }
            set { _package.UpdateObjects = value; }
        }

        public string DesignTimeProperties
        {
            get { return _package.DesignTimeProperties; }
            set { _package.DesignTimeProperties = value; }
        }

        public bool SafeRecursiveProjectPackageExecution
        {
            get { return _package.SafeRecursiveProjectPackageExecution; }
            set { _package.SafeRecursiveProjectPackageExecution = value; }
        }

        public Executables Executables
        {
            get { return _package.Executables; }
        }

        public PrecedenceConstraints PrecedenceConstraints
        {
            get { return _package.PrecedenceConstraints; }
        }

        public DtsProperties Properties
        {
            get { return _package.Properties; }
        }

        public bool HasExpressions
        {
            get { return _package.HasExpressions; }
        }

        public Parameters Parameters
        {
            get { return _package.Parameters; }
        }

        public bool FailPackageOnFailure
        {
            get { return _package.FailPackageOnFailure; }
            set { _package.FailPackageOnFailure = value; }
        }

        public PackageUpgradeOptions PackageUpgradeOptions
        {
            get { return _package.PackageUpgradeOptions; }
            set { _package.PackageUpgradeOptions = value; }
        }

    }
}