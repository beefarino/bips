using System;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime.Wrapper;

namespace CodeOwls.BIPS
{
    public class BipsProxyIDTSBufferManager100
    {
        private readonly IDTSBufferManager100 _innerObject;

        internal BipsProxyIDTSBufferManager100(IDTSBufferManager100 innerObject)
        {
            _innerObject = innerObject;
        }

        public int RegisterBufferType(Int32 cCols, ref DTP_BUFFCOL rgCols, Int32 lMaxRows, UInt32 dwCreationFlags)
        {
            return _innerObject.RegisterBufferType(cCols, ref rgCols, lMaxRows, dwCreationFlags);
        }

        public void RegisterLineageIDs(Int32 hBufferType, Int32 cCols, ref Int32 lLineageIDs)
        {
            _innerObject.RegisterLineageIDs(hBufferType, cCols, ref lLineageIDs);
        }

        public void CreateVirtualBuffer(Int32 hSourceBuffer, Int32 lOutputID)
        {
            _innerObject.CreateVirtualBuffer(hSourceBuffer, lOutputID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSBuffer100 CreateFlatBuffer(Int32 lSize,
            IDTSComponentMetaData100 pOwner)
        {
            return _innerObject.CreateFlatBuffer(lSize, pOwner);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSBuffer100 CreateBuffer(Int32 hBufferType,
            IDTSComponentMetaData100 pOwner)
        {
            return _innerObject.CreateBuffer(hBufferType, pOwner);
        }

        public int FindColumnByLineageID(Int32 hBufferType, Int32 nLineageID)
        {
            return _innerObject.FindColumnByLineageID(hBufferType, nLineageID);
        }

        public void GetColumnInfo(Int32 hBufferType, Int32 hCol, ref DTP_BUFFCOL pCol)
        {
            _innerObject.GetColumnInfo(hBufferType, hCol, ref pCol);
        }

        public UInt32 GetColumnCount(Int32 hBufferType)
        {
            return _innerObject.GetColumnCount(hBufferType);
        }

        public int GetRowWidth(Int32 hBufferType)
        {
            return _innerObject.GetRowWidth(hBufferType);
        }

        public void GetBLOBObject(ref IDTSBLOBObject100 ppNewObject)
        {
            _innerObject.GetBLOBObject(ref ppNewObject);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSBuffer100 CreateFlatBuffer64(UInt64 lSize,
            IDTSComponentMetaData100 pOwner)
        {
            return _innerObject.CreateFlatBuffer64(lSize, pOwner);
        }
    }

    public class BipsProxyIDTSBuffer100
    {
        private readonly IDTSBuffer100 _innerObject;

        internal BipsProxyIDTSBuffer100(IDTSBuffer100 innerObject)
        {
            _innerObject = innerObject;
        }

        public void DirectRow(Int32 hRow, Int32 lOutputID)
        {
            _innerObject.DirectRow(hRow, lOutputID);
        }

        public void MoveRow(Int32 hFrom, Int32 hTo)
        {
            _innerObject.MoveRow(hFrom, hTo);
        }

        public void SwapRows(Int32 hOne, Int32 hOther)
        {
            _innerObject.SwapRows(hOne, hOther);
        }

        public int GetBufferType()
        {
            return _innerObject.GetType();
        }

        public void LockData()
        {
            _innerObject.LockData();
        }

        public void UnlockData()
        {
            _innerObject.UnlockData();
        }

        public UInt32 GetRowCount()
        {
            return _innerObject.GetRowCount();
        }

        public UInt32 GetColumnCount()
        {
            return _innerObject.GetColumnCount();
        }

        public void GetColumnInfo(Int32 hCol, ref DTP_BUFFCOL pCol)
        {
            _innerObject.GetColumnInfo(hCol, ref pCol);
        }

        public void GetBoundaryInfo(out UInt32 pdwCols, out UInt32 pdwMaxRows)
        {
            _innerObject.GetBoundaryInfo(out pdwCols, out pdwMaxRows);
        }

        public int AddRow(IntPtr ppRowStart)
        {
            return _innerObject.AddRow(ppRowStart);
        }

        public void RemoveRow(Int32 hRow)
        {
            _innerObject.RemoveRow(hRow);
        }

        public int GetID()
        {
            return _innerObject.GetID();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSBufferManager100 GetManager()
        {
            return _innerObject.GetManager();
        }

        public Microsoft.SqlServer.Dts.Runtime.Wrapper.DTP_VARIANT GetData(Int32 hRow, Int32 hCol)
        {
            return _innerObject.GetData(hRow, hCol);
        }

        public Microsoft.SqlServer.Dts.Runtime.Wrapper.DTP_VARIANT GetDataByRef(Int32 hRow, Int32 hCol)
        {
            return _innerObject.GetDataByRef(hRow, hCol);
        }

        public void SetData(Int32 hRow, Int32 hCol, ref DTP_VARIANT pData)
        {
            _innerObject.SetData(hRow, hCol, ref pData);
        }

        public void GetBLOBLength(Int32 hRow, Int32 hCol, out UInt32 pdwBytes)
        {
            _innerObject.GetBLOBLength(hRow, hCol, out pdwBytes);
        }

        public void AddBLOBData(Int32 hRow, Int32 hCol, ref Byte lpData, UInt32 dwLength)
        {
            _innerObject.AddBLOBData(hRow, hCol, ref lpData, dwLength);
        }

        public void GetBLOBData(Int32 hRow, Int32 hCol, UInt32 dwOffset, ref Byte lpPointer, UInt32 dwLength,
            out UInt32 lpdwWritten)
        {
            _innerObject.GetBLOBData(hRow, hCol, dwOffset, ref lpPointer, dwLength, out lpdwWritten);
        }

        public Microsoft.SqlServer.Dts.Runtime.Wrapper.IStream GetBLOBStream(Int32 hRow, Int32 hCol)
        {
            return _innerObject.GetBLOBStream(hRow, hCol);
        }

        public void SetBLOBFromStream(Int32 hRow, Int32 hCol, ISequentialStream pIStream)
        {
            _innerObject.SetBLOBFromStream(hRow, hCol, pIStream);
        }

        public void ResetBLOBData(Int32 hRow, Int32 hCol)
        {
            _innerObject.ResetBLOBData(hRow, hCol);
        }

        public void SetBLOBFromObject(Int32 hRow, Int32 hCol, IDTSBLOBObject100 pIDTSBLOBObject)
        {
            _innerObject.SetBLOBFromObject(hRow, hCol, pIDTSBLOBObject);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSBLOBObject100 GetBLOBObject(Int32 hRow, Int32 hCol)
        {
            return _innerObject.GetBLOBObject(hRow, hCol);
        }

        public void IsNull(Int32 hRow, Int32 hCol, ref Boolean pfNull)
        {
            _innerObject.IsNull(hRow, hCol, ref pfNull);
        }

        public void SetStatus(Int32 hRow, Int32 hCol, UInt32 dbStatus)
        {
            _innerObject.SetStatus(hRow, hCol, dbStatus);
        }

        public void GetStatus(Int32 hRow, Int32 hCol, out UInt32 pDBStatus)
        {
            _innerObject.GetStatus(hRow, hCol, out pDBStatus);
        }

        public void PrepareDataStatusForInsert(Int32 hRow)
        {
            _innerObject.PrepareDataStatusForInsert(hRow);
        }

        public void SetEndOfRowset()
        {
            _innerObject.SetEndOfRowset();
        }

        public bool IsEndOfRowset()
        {
            return _innerObject.IsEndOfRowset();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSBuffer100 Clone(IDTSComponentMetaData100 pOwner)
        {
            return _innerObject.Clone(pOwner);
        }

        public void GetRowDataBytes(Int32 hRow, out Int32 plCB, IntPtr pData)
        {
            _innerObject.GetRowDataBytes(hRow, out plCB, pData);
        }

        public void GetRowStarts(UInt32 dwRowsRequested, IntPtr pbRowStarts)
        {
            _innerObject.GetRowStarts(dwRowsRequested, pbRowStarts);
        }

        public void DirectErrorRow(Int32 hRow, Int32 lOutputID, Int32 lErrorCode, Int32 lErrorColumn)
        {
            _innerObject.DirectErrorRow(hRow, lOutputID, lErrorCode, lErrorColumn);
        }

        public void SetErrorInfo(Int32 hRow, Int32 lOutputID, Int32 lErrorCode, Int32 lErrorColumn)
        {
            _innerObject.SetErrorInfo(hRow, lOutputID, lErrorCode, lErrorColumn);
        }

        public System.IntPtr GetFlatMemory()
        {
            return _innerObject.GetFlatMemory();
        }
    }

    public class BipsProxyIDTSComponentMetaData100
    {
        private readonly IDTSComponentMetaData100 _innerObject;

        internal BipsProxyIDTSComponentMetaData100(IDTSComponentMetaData100 innerObject)
        {
            _innerObject = innerObject;
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.CManagedComponentWrapper Instantiate()
        {
            return _innerObject.Instantiate();
        }

        public void PostLogMessage(String bstrEventName, String bstrSourceName, String bstrMessageText,
            DateTime dateStartTime, DateTime dateEndTime, Int32 lDataCode, ref Byte[] psaDataBytes)
        {
            _innerObject.PostLogMessage(bstrEventName, bstrSourceName, bstrMessageText, dateStartTime, dateEndTime,
                lDataCode, ref psaDataBytes);
        }

        public void RemoveInvalidInputColumns()
        {
            _innerObject.RemoveInvalidInputColumns();
        }

        public string GetErrorDescription(Int32 hrError)
        {
            return _innerObject.GetErrorDescription(hrError);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentView100 GetComponentView()
        {
            return _innerObject.GetComponentView();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSValidationStatus Validate()
        {
            return _innerObject.Validate();
        }

        public void IncrementPipelinePerfCounter(UInt32 dwCounterID, UInt32 dwDifference)
        {
            _innerObject.IncrementPipelinePerfCounter(dwCounterID, dwDifference);
        }

        public void FireWarning(Int32 WarningCode, String SubComponent, String Description, String HelpFile,
            Int32 HelpContext)
        {
            _innerObject.FireWarning(WarningCode, SubComponent, Description, HelpFile, HelpContext);
        }

        public void FireInformation(Int32 InformationCode, String SubComponent, String Description, String HelpFile,
            Int32 HelpContext, ref Boolean pbFireAgain)
        {
            _innerObject.FireInformation(InformationCode, SubComponent, Description, HelpFile, HelpContext,
                ref pbFireAgain);
        }

        public void FireError(Int32 ErrorCode, String SubComponent, String Description, String HelpFile,
            Int32 HelpContext, out Boolean pbCancel)
        {
            _innerObject.FireError(ErrorCode, SubComponent, Description, HelpFile, HelpContext, out pbCancel);
        }

        public void FireCustomEvent(String EventName, String EventText, ref Object[] ppsaArguments, String SubComponent,
            ref Boolean pbFireAgain)
        {
            _innerObject.FireCustomEvent(EventName, EventText, ref ppsaArguments, SubComponent, ref pbFireAgain);
        }

        public void FireProgress(String bstrProgressDescription, Int32 lPercentComplete, Int32 lProgressCountLow,
            Int32 lProgressCountHigh, String bstrSubComponent, ref Boolean pbFireAgain)
        {
            _innerObject.FireProgress(bstrProgressDescription, lPercentComplete, lProgressCountLow, lProgressCountHigh,
                bstrSubComponent, ref pbFireAgain);
        }

        public int ID
        {
            get { return _innerObject.ID; }
            set { _innerObject.ID = value; }
        }

        public string Description
        {
            get { return _innerObject.Description; }
            set { _innerObject.Description = value; }
        }

        public string Name
        {
            get { return _innerObject.Name; }
            set { _innerObject.Name = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSObjectType ObjectType
        {
            get { return _innerObject.ObjectType; }
        }

        public string IdentificationString
        {
            get { return _innerObject.IdentificationString; }
        }

        public string ComponentClassID
        {
            get { return _innerObject.ComponentClassID; }
            set { _innerObject.ComponentClassID = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSInputCollection100 InputCollection
        {
            get { return _innerObject.InputCollection; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutputCollection100 OutputCollection
        {
            get { return _innerObject.OutputCollection; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomPropertyCollection100 CustomPropertyCollection
        {
            get { return _innerObject.CustomPropertyCollection; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSRuntimeConnectionCollection100 RuntimeConnectionCollection
        {
            get { return _innerObject.RuntimeConnectionCollection; }
        }

        public bool AreInputColumnsValid
        {
            get { return _innerObject.AreInputColumnsValid; }
        }

        public int LocaleID
        {
            get { return _innerObject.LocaleID; }
            set { _innerObject.LocaleID = value; }
        }

        public bool IsDefaultLocale
        {
            get { return _innerObject.IsDefaultLocale; }
        }

        public bool UsesDispositions
        {
            get { return _innerObject.UsesDispositions; }
            set { _innerObject.UsesDispositions = value; }
        }

        public bool ValidateExternalMetadata
        {
            get { return _innerObject.ValidateExternalMetadata; }
            set { _innerObject.ValidateExternalMetadata = value; }
        }

        public int Version
        {
            get { return _innerObject.Version; }
            set { _innerObject.Version = value; }
        }

        public int PipelineVersion
        {
            get { return _innerObject.PipelineVersion; }
            set { _innerObject.PipelineVersion = value; }
        }

        public string ContactInfo
        {
            get { return _innerObject.ContactInfo; }
            set { _innerObject.ContactInfo = value; }
        }
    }

    public class BipsProxyIDTSBLOBObject100
    {
        private readonly IDTSBLOBObject100 _innerObject;

        internal BipsProxyIDTSBLOBObject100(IDTSBLOBObject100 innerObject)
        {
            _innerObject = innerObject;
        }

        public void ResetData()
        {
            _innerObject.ResetData();
        }

        public void AddData(ref Byte lpData, UInt32 dwLength)
        {
            _innerObject.AddData(ref lpData, dwLength);
        }

        public void GetData(UInt32 dwOffset, ref Byte lpPointer, UInt32 dwLength, out UInt32 lpdwWritten)
        {
            _innerObject.GetData(dwOffset, ref lpPointer, dwLength, out lpdwWritten);
        }

        public void PutData(UInt32 dwOffset, ref Byte lpPointer, UInt32 dwLength, out UInt32 lpdwRead)
        {
            _innerObject.PutData(dwOffset, ref lpPointer, dwLength, out lpdwRead);
        }

        public Microsoft.SqlServer.Dts.Runtime.Wrapper.IStream GetStream()
        {
            return _innerObject.GetStream();
        }

        public void SetFromStream(ISequentialStream pISequentialStream)
        {
            _innerObject.SetFromStream(pISequentialStream);
        }

        public int Length
        {
            get { return _innerObject.Length; }
        }

        public int SpoolThreshold
        {
            get { return _innerObject.SpoolThreshold; }
            set { _innerObject.SpoolThreshold = value; }
        }
    }

    public class BipsProxyDTSBufferManager
    {
        private readonly DTSBufferManager _innerObject;

        internal BipsProxyDTSBufferManager(DTSBufferManager innerObject)
        {
            _innerObject = innerObject;
        }


    }

    public class BipsProxyIDTSObjectModel100
    {
        private readonly IDTSObjectModel100 _innerObject;

        internal BipsProxyIDTSObjectModel100(IDTSObjectModel100 innerObject)
        {
            _innerObject = innerObject;
        }

        public void New()
        {
            _innerObject.New();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSObject100 GetObjectByID(Int32 lID)
        {
            return _innerObject.GetObjectByID(lID);
        }

        public void UpdateCacheOnInputColumns()
        {
            _innerObject.UpdateCacheOnInputColumns();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaDataCollection100 ComponentMetaDataCollection
        {
            get { return _innerObject.ComponentMetaDataCollection; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSPathCollection100 PathCollection
        {
            get { return _innerObject.PathCollection; }
        }

        public bool AutoGenerateIDForNewObjects
        {
            get { return _innerObject.AutoGenerateIDForNewObjects; }
            set { _innerObject.AutoGenerateIDForNewObjects = value; }
        }

        public bool EnableCacheUpdate
        {
            get { return _innerObject.EnableCacheUpdate; }
            set { _innerObject.EnableCacheUpdate = value; }
        }

        public bool EnableDisconnectedColumns
        {
            get { return _innerObject.EnableDisconnectedColumns; }
            set { _innerObject.EnableDisconnectedColumns = value; }
        }

        public bool IsSavingXml
        {
            get { return _innerObject.IsSavingXml; }
            set { _innerObject.IsSavingXml = value; }
        }
    }

    public class BipsProxyIDTSComponentMetaDataCollection100
    {
        private readonly IDTSComponentMetaDataCollection100 _innerObject;

        internal BipsProxyIDTSComponentMetaDataCollection100(IDTSComponentMetaDataCollection100 innerObject)
        {
            _innerObject = innerObject;
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _innerObject.GetEnumerator();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100 New()
        {
            return _innerObject.New();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100 NewAt(Int32 lIndex)
        {
            return _innerObject.NewAt(lIndex);
        }

        public void RemoveObjectByIndex(Object Index)
        {
            _innerObject.RemoveObjectByIndex(Index);
        }

        public void RemoveObjectByID(Int32 lID)
        {
            _innerObject.RemoveObjectByID(lID);
        }

        public void RemoveAll()
        {
            _innerObject.RemoveAll();
        }

        public void SetIndex(Int32 lOldIndex, Int32 lNewIndex)
        {
            _innerObject.SetIndex(lOldIndex, lNewIndex);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100 GetObjectByID(Int32 lID)
        {
            return _innerObject.GetObjectByID(lID);
        }

        public int GetObjectIndexByID(Int32 lID)
        {
            return _innerObject.GetObjectIndexByID(lID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100 FindObjectByID(Int32 lID)
        {
            return _innerObject.FindObjectByID(lID);
        }

        public int FindObjectIndexByID(Int32 lID)
        {
            return _innerObject.FindObjectIndexByID(lID);
        }

        public int Count
        {
            get { return _innerObject.Count; }
        }
    }

    public class BipsProxyIDTSPathCollection100
    {
        private readonly IDTSPathCollection100 _innerObject;

        internal BipsProxyIDTSPathCollection100(IDTSPathCollection100 innerObject)
        {
            _innerObject = innerObject;
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _innerObject.GetEnumerator();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSPath100 New()
        {
            return _innerObject.New();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSPath100 NewAt(Int32 lIndex)
        {
            return _innerObject.NewAt(lIndex);
        }

        public void RemoveObjectByIndex(Object Index)
        {
            _innerObject.RemoveObjectByIndex(Index);
        }

        public void RemoveObjectByID(Int32 lID)
        {
            _innerObject.RemoveObjectByID(lID);
        }

        public void RemoveAll()
        {
            _innerObject.RemoveAll();
        }

        public void SetIndex(Int32 lOldIndex, Int32 lNewIndex)
        {
            _innerObject.SetIndex(lOldIndex, lNewIndex);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSPath100 GetObjectByID(Int32 lID)
        {
            return _innerObject.GetObjectByID(lID);
        }

        public int GetObjectIndexByID(Int32 lID)
        {
            return _innerObject.GetObjectIndexByID(lID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSPath100 FindObjectByID(Int32 lID)
        {
            return _innerObject.FindObjectByID(lID);
        }

        public int FindObjectIndexByID(Int32 lID)
        {
            return _innerObject.FindObjectIndexByID(lID);
        }

        public int Count
        {
            get { return _innerObject.Count; }
        }
    }

    public class BipsProxyIDTSObject100
    {
        private readonly IDTSObject100 _innerObject;

        internal BipsProxyIDTSObject100(IDTSObject100 innerObject)
        {
            _innerObject = innerObject;
        }

        public int ID
        {
            get { return _innerObject.ID; }
            set { _innerObject.ID = value; }
        }

        public string Description
        {
            get { return _innerObject.Description; }
            set { _innerObject.Description = value; }
        }

        public string Name
        {
            get { return _innerObject.Name; }
            set { _innerObject.Name = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSObjectType ObjectType
        {
            get { return _innerObject.ObjectType; }
        }

        public string IdentificationString
        {
            get { return _innerObject.IdentificationString; }
        }
    }

    public class BipsProxyIDTSPipeline100
    {
        private readonly IDTSPipeline100 _innerObject;

        internal BipsProxyIDTSPipeline100(IDTSPipeline100 innerObject)
        {
            _innerObject = innerObject;
        }

        public void New()
        {
            _innerObject.New();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSObject100 GetObjectByID(Int32 lID)
        {
            return _innerObject.GetObjectByID(lID);
        }

        public void UpdateCacheOnInputColumns()
        {
            _innerObject.UpdateCacheOnInputColumns();
        }

        public int GetNextPasteID()
        {
            return _innerObject.GetNextPasteID();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaDataCollection100 ComponentMetaDataCollection
        {
            get { return _innerObject.ComponentMetaDataCollection; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSPathCollection100 PathCollection
        {
            get { return _innerObject.PathCollection; }
        }

        public bool AutoGenerateIDForNewObjects
        {
            get { return _innerObject.AutoGenerateIDForNewObjects; }
            set { _innerObject.AutoGenerateIDForNewObjects = value; }
        }

        public bool EnableCacheUpdate
        {
            get { return _innerObject.EnableCacheUpdate; }
            set { _innerObject.EnableCacheUpdate = value; }
        }

        public bool EnableDisconnectedColumns
        {
            get { return _innerObject.EnableDisconnectedColumns; }
            set { _innerObject.EnableDisconnectedColumns = value; }
        }

        public bool IsSavingXml
        {
            get { return _innerObject.IsSavingXml; }
            set { _innerObject.IsSavingXml = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSBufferManager100 BufferManager
        {
            get { return _innerObject.BufferManager; }
        }

        public int DefaultBufferMaxRows
        {
            get { return _innerObject.DefaultBufferMaxRows; }
            set { _innerObject.DefaultBufferMaxRows = value; }
        }

        public Microsoft.SqlServer.Dts.Runtime.Wrapper.IDTSComponentEvents100 Events
        {
            set { _innerObject.Events = value; }
        }

        public Microsoft.SqlServer.Dts.Runtime.Wrapper.IDTSVariableDispenser100 VariableDispenser
        {
            set { _innerObject.VariableDispenser = value; }
        }

        public int DefaultBufferSize
        {
            get { return _innerObject.DefaultBufferSize; }
            set { _innerObject.DefaultBufferSize = value; }
        }

        public string BLOBTempStoragePath
        {
            get { return _innerObject.BLOBTempStoragePath; }
            set { _innerObject.BLOBTempStoragePath = value; }
        }

        public string BufferTempStoragePath
        {
            get { return _innerObject.BufferTempStoragePath; }
            set { _innerObject.BufferTempStoragePath = value; }
        }

        public bool RunInOptimizedMode
        {
            get { return _innerObject.RunInOptimizedMode; }
            set { _innerObject.RunInOptimizedMode = value; }
        }

        public int EngineThreads
        {
            get { return _innerObject.EngineThreads; }
            set { _innerObject.EngineThreads = value; }
        }
    }

    public class BipsProxyIDTSCustomPropertyCollection100
    {
        private readonly IDTSCustomPropertyCollection100 _innerObject;

        internal BipsProxyIDTSCustomPropertyCollection100(IDTSCustomPropertyCollection100 innerObject)
        {
            _innerObject = innerObject;
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _innerObject.GetEnumerator();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100 New()
        {
            return _innerObject.New();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100 NewAt(Int32 lIndex)
        {
            return _innerObject.NewAt(lIndex);
        }

        public void RemoveObjectByIndex(Object Index)
        {
            _innerObject.RemoveObjectByIndex(Index);
        }

        public void RemoveObjectByID(Int32 lID)
        {
            _innerObject.RemoveObjectByID(lID);
        }

        public void RemoveAll()
        {
            _innerObject.RemoveAll();
        }

        public void SetIndex(Int32 lOldIndex, Int32 lNewIndex)
        {
            _innerObject.SetIndex(lOldIndex, lNewIndex);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100 GetObjectByID(Int32 lID)
        {
            return _innerObject.GetObjectByID(lID);
        }

        public int GetObjectIndexByID(Int32 lID)
        {
            return _innerObject.GetObjectIndexByID(lID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100 FindObjectByID(Int32 lID)
        {
            return _innerObject.FindObjectByID(lID);
        }

        public int FindObjectIndexByID(Int32 lID)
        {
            return _innerObject.FindObjectIndexByID(lID);
        }

        public int Count
        {
            get { return _innerObject.Count; }
        }
    }

    public class BipsProxyMainPipe
    {
        private readonly MainPipe _innerObject;

        internal BipsProxyMainPipe(MainPipe innerObject)
        {
            _innerObject = innerObject;
        }


    }

    public class BipsProxyIDTSDesigntimeComponent100
    {
        private readonly IDTSDesigntimeComponent100 _innerObject;

        internal BipsProxyIDTSDesigntimeComponent100(IDTSDesigntimeComponent100 innerObject)
        {
            _innerObject = innerObject;
        }

        public void ProvideComponentProperties()
        {
            _innerObject.ProvideComponentProperties();
        }

        public void ReinitializeMetaData()
        {
            _innerObject.ReinitializeMetaData();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutputColumn100 InsertOutputColumnAt(Int32 lOutputID,
            Int32 lOutputColumnIndex, String bstrName, String bstrDescription)
        {
            return _innerObject.InsertOutputColumnAt(lOutputID, lOutputColumnIndex, bstrName, bstrDescription);
        }

        public void DeleteOutputColumn(Int32 lOutputID, Int32 lOutputColumnID)
        {
            _innerObject.DeleteOutputColumn(lOutputID, lOutputColumnID);
        }

        public void OnDeletingInputColumn(Int32 lInputID, Int32 lInputColumnID)
        {
            _innerObject.OnDeletingInputColumn(lInputID, lInputColumnID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSInputColumn100 SetUsageType(Int32 lInputID,
            IDTSVirtualInput100 pIDTSVirtualInputObject, Int32 lLineageID, DTSUsageType eUsageType)
        {
            return _innerObject.SetUsageType(lInputID, pIDTSVirtualInputObject, lLineageID, eUsageType);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100 SetComponentProperty(String PropertyName,
            Object vValue)
        {
            return _innerObject.SetComponentProperty(PropertyName, vValue);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100 SetInputProperty(Int32 lInputID,
            String PropertyName, Object vValue)
        {
            return _innerObject.SetInputProperty(lInputID, PropertyName, vValue);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100 SetOutputProperty(Int32 lOutputID,
            String PropertyName, Object vValue)
        {
            return _innerObject.SetOutputProperty(lOutputID, PropertyName, vValue);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100 SetInputColumnProperty(Int32 lInputID,
            Int32 lInputColumnID, String PropertyName, Object vValue)
        {
            return _innerObject.SetInputColumnProperty(lInputID, lInputColumnID, PropertyName, vValue);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100 SetOutputColumnProperty(Int32 lOutputID,
            Int32 lOutputColumnID, String PropertyName, Object vValue)
        {
            return _innerObject.SetOutputColumnProperty(lOutputID, lOutputColumnID, PropertyName, vValue);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSInput100 InsertInput(DTSInsertPlacement eInsertPlacement,
            Int32 lInputID)
        {
            return _innerObject.InsertInput(eInsertPlacement, lInputID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutput100 InsertOutput(DTSInsertPlacement eInsertPlacement,
            Int32 lOutputID)
        {
            return _innerObject.InsertOutput(eInsertPlacement, lOutputID);
        }

        public void DeleteInput(Int32 lInputID)
        {
            _innerObject.DeleteInput(lInputID);
        }

        public void DeleteOutput(Int32 lOutputID)
        {
            _innerObject.DeleteOutput(lOutputID);
        }

        public void OnInputPathDetached(Int32 lInputID)
        {
            _innerObject.OnInputPathDetached(lInputID);
        }

        public void OnInputPathAttached(Int32 lInputID)
        {
            _innerObject.OnInputPathAttached(lInputID);
        }

        public void OnOutputPathAttached(Int32 lOutputID)
        {
            _innerObject.OnOutputPathAttached(lOutputID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSValidationStatus Validate()
        {
            return _innerObject.Validate();
        }

        public void AcquireConnections(Object pTransaction)
        {
            _innerObject.AcquireConnections(pTransaction);
        }

        public void ReleaseConnections()
        {
            _innerObject.ReleaseConnections();
        }

        public void SetOutputColumnDataTypeProperties(Int32 lOutputID, Int32 lOutputColumnID, DataType eDataType,
            Int32 lLength, Int32 lPrecision, Int32 lScale, Int32 lCodePage)
        {
            _innerObject.SetOutputColumnDataTypeProperties(lOutputID, lOutputColumnID, eDataType, lLength, lPrecision,
                lScale, lCodePage);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSExternalMetadataColumn100 MapInputColumn(Int32 lInputID,
            Int32 lInputColumnID, Int32 lExternalMetadataColumnID)
        {
            return _innerObject.MapInputColumn(lInputID, lInputColumnID, lExternalMetadataColumnID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSExternalMetadataColumn100 MapOutputColumn(Int32 lOutputID,
            Int32 lOutputColumnID, Int32 lExternalMetadataColumnID, Boolean bMatch)
        {
            return _innerObject.MapOutputColumn(lOutputID, lOutputColumnID, lExternalMetadataColumnID, bMatch);
        }

        public string DescribeRedirectedErrorCode(Int32 hrErrorCode)
        {
            return _innerObject.DescribeRedirectedErrorCode(hrErrorCode);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSFriendlyEnumCollection100 GetEnumerationCollection(
            String bstrEnumName)
        {
            return _innerObject.GetEnumerationCollection(bstrEnumName);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSExternalMetadataColumn100 InsertExternalMetadataColumnAt(
            Int32 lID, Int32 lExternalMetadataColumnIndex, String bstrName, String bstrDescription)
        {
            return _innerObject.InsertExternalMetadataColumnAt(lID, lExternalMetadataColumnIndex, bstrName,
                bstrDescription);
        }

        public void DeleteExternalMetadataColumn(Int32 lID, Int32 lExternalMetadataColumnID)
        {
            _innerObject.DeleteExternalMetadataColumn(lID, lExternalMetadataColumnID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100 SetExternalMetadataColumnProperty(
            Int32 lID, Int32 lExternalMetadataColumnID, String bstrPropertyName, Object vtValue)
        {
            return _innerObject.SetExternalMetadataColumnProperty(lID, lExternalMetadataColumnID, bstrPropertyName,
                vtValue);
        }

        public void SetExternalMetadataColumnDataTypeProperties(Int32 lID, Int32 lExternalMetadataColumnID,
            DataType eDataType, Int32 lLength, Int32 lPrecision, Int32 lScale, Int32 lCodePage)
        {
            _innerObject.SetExternalMetadataColumnDataTypeProperties(lID, lExternalMetadataColumnID, eDataType, lLength,
                lPrecision, lScale, lCodePage);
        }
    }

    public class BipsProxyIDTSOutputColumn100
    {
        private readonly IDTSOutputColumn100 _innerObject;

        internal BipsProxyIDTSOutputColumn100(IDTSOutputColumn100 innerObject)
        {
            _innerObject = innerObject;
        }

        public void SetDataTypeProperties(DataType eDataType, Int32 lLength, Int32 lPrecision, Int32 lScale,
            Int32 lCodePage)
        {
            _innerObject.SetDataTypeProperties(eDataType, lLength, lPrecision, lScale, lCodePage);
        }

        public int ID
        {
            get { return _innerObject.ID; }
            set { _innerObject.ID = value; }
        }

        public string Description
        {
            get { return _innerObject.Description; }
            set { _innerObject.Description = value; }
        }

        public string Name
        {
            get { return _innerObject.Name; }
            set { _innerObject.Name = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSObjectType ObjectType
        {
            get { return _innerObject.ObjectType; }
        }

        public string IdentificationString
        {
            get { return _innerObject.IdentificationString; }
        }

        public Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType DataType
        {
            get { return _innerObject.DataType; }
        }

        public int Length
        {
            get { return _innerObject.Length; }
        }

        public int Precision
        {
            get { return _innerObject.Precision; }
        }

        public int Scale
        {
            get { return _innerObject.Scale; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomPropertyCollection100 CustomPropertyCollection
        {
            get { return _innerObject.CustomPropertyCollection; }
        }

        public int LineageID
        {
            get { return _innerObject.LineageID; }
            set { _innerObject.LineageID = value; }
        }

        public int MappedColumnID
        {
            get { return _innerObject.MappedColumnID; }
            set { _innerObject.MappedColumnID = value; }
        }

        public int CodePage
        {
            get { return _innerObject.CodePage; }
        }

        public int SortKeyPosition
        {
            get { return _innerObject.SortKeyPosition; }
            set { _innerObject.SortKeyPosition = value; }
        }

        public int ComparisonFlags
        {
            get { return _innerObject.ComparisonFlags; }
            set { _innerObject.ComparisonFlags = value; }
        }

        public int SpecialFlags
        {
            get { return _innerObject.SpecialFlags; }
            set { _innerObject.SpecialFlags = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSRowDisposition ErrorRowDisposition
        {
            get { return _innerObject.ErrorRowDisposition; }
            set { _innerObject.ErrorRowDisposition = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSRowDisposition TruncationRowDisposition
        {
            get { return _innerObject.TruncationRowDisposition; }
            set { _innerObject.TruncationRowDisposition = value; }
        }

        public string ErrorOrTruncationOperation
        {
            get { return _innerObject.ErrorOrTruncationOperation; }
            set { _innerObject.ErrorOrTruncationOperation = value; }
        }

        public int ExternalMetadataColumnID
        {
            get { return _innerObject.ExternalMetadataColumnID; }
            set { _innerObject.ExternalMetadataColumnID = value; }
        }
    }

    public class BipsProxyIDTSInputColumn100
    {
        private readonly IDTSInputColumn100 _innerObject;

        internal BipsProxyIDTSInputColumn100(IDTSInputColumn100 innerObject)
        {
            _innerObject = innerObject;
        }

        public string DescribeRedirectedErrorCode(Int32 hrErrorCode)
        {
            return _innerObject.DescribeRedirectedErrorCode(hrErrorCode);
        }

        public int ID
        {
            get { return _innerObject.ID; }
            set { _innerObject.ID = value; }
        }

        public string Description
        {
            get { return _innerObject.Description; }
            set { _innerObject.Description = value; }
        }

        public string Name
        {
            get { return _innerObject.Name; }
            set { _innerObject.Name = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSObjectType ObjectType
        {
            get { return _innerObject.ObjectType; }
        }

        public string IdentificationString
        {
            get { return _innerObject.IdentificationString; }
        }

        public Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType DataType
        {
            get { return _innerObject.DataType; }
        }

        public int Length
        {
            get { return _innerObject.Length; }
        }

        public int Precision
        {
            get { return _innerObject.Precision; }
        }

        public int Scale
        {
            get { return _innerObject.Scale; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSUsageType UsageType
        {
            get { return _innerObject.UsageType; }
            set { _innerObject.UsageType = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomPropertyCollection100 CustomPropertyCollection
        {
            get { return _innerObject.CustomPropertyCollection; }
        }

        public int LineageID
        {
            get { return _innerObject.LineageID; }
            set { _innerObject.LineageID = value; }
        }

        public int MappedColumnID
        {
            get { return _innerObject.MappedColumnID; }
            set { _innerObject.MappedColumnID = value; }
        }

        public int CodePage
        {
            get { return _innerObject.CodePage; }
        }

        public bool IsValid
        {
            get { return _innerObject.IsValid; }
        }

        public int SortKeyPosition
        {
            get { return _innerObject.SortKeyPosition; }
        }

        public int ComparisonFlags
        {
            get { return _innerObject.ComparisonFlags; }
        }

        public string UpstreamComponentName
        {
            get { return _innerObject.UpstreamComponentName; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSRowDisposition ErrorRowDisposition
        {
            get { return _innerObject.ErrorRowDisposition; }
            set { _innerObject.ErrorRowDisposition = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSRowDisposition TruncationRowDisposition
        {
            get { return _innerObject.TruncationRowDisposition; }
            set { _innerObject.TruncationRowDisposition = value; }
        }

        public string ErrorOrTruncationOperation
        {
            get { return _innerObject.ErrorOrTruncationOperation; }
            set { _innerObject.ErrorOrTruncationOperation = value; }
        }

        public int ExternalMetadataColumnID
        {
            get { return _innerObject.ExternalMetadataColumnID; }
            set { _innerObject.ExternalMetadataColumnID = value; }
        }

        public bool IsAssociatedWithOutputColumn
        {
            get { return _innerObject.IsAssociatedWithOutputColumn; }
        }
    }

    public class BipsProxyIDTSVirtualInput100
    {
        private readonly IDTSVirtualInput100 _innerObject;

        internal BipsProxyIDTSVirtualInput100(IDTSVirtualInput100 innerObject)
        {
            _innerObject = innerObject;
        }

        public int SetUsageType(Int32 lLineageID, DTSUsageType eUsageType)
        {
            return _innerObject.SetUsageType(lLineageID, eUsageType);
        }

        public int ID
        {
            get { return _innerObject.ID; }
            set { _innerObject.ID = value; }
        }

        public string Description
        {
            get { return _innerObject.Description; }
            set { _innerObject.Description = value; }
        }

        public string Name
        {
            get { return _innerObject.Name; }
            set { _innerObject.Name = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSObjectType ObjectType
        {
            get { return _innerObject.ObjectType; }
        }

        public string IdentificationString
        {
            get { return _innerObject.IdentificationString; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSVirtualInputColumnCollection100 VirtualInputColumnCollection
        {
            get { return _innerObject.VirtualInputColumnCollection; }
        }

        public bool IsSorted
        {
            get { return _innerObject.IsSorted; }
        }

        public int SourceLocale
        {
            get { return _innerObject.SourceLocale; }
        }
    }

    public class BipsProxyIDTSCustomProperty100
    {
        private readonly IDTSCustomProperty100 _innerObject;

        internal BipsProxyIDTSCustomProperty100(IDTSCustomProperty100 innerObject)
        {
            _innerObject = innerObject;
        }

        public int ID
        {
            get { return _innerObject.ID; }
            set { _innerObject.ID = value; }
        }

        public string Description
        {
            get { return _innerObject.Description; }
            set { _innerObject.Description = value; }
        }

        public string Name
        {
            get { return _innerObject.Name; }
            set { _innerObject.Name = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSObjectType ObjectType
        {
            get { return _innerObject.ObjectType; }
        }

        public string IdentificationString
        {
            get { return _innerObject.IdentificationString; }
        }

        public System.Object Value
        {
            get { return _innerObject.Value; }
            set { _innerObject.Value = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSPersistState State
        {
            get { return _innerObject.State; }
            set { _innerObject.State = value; }
        }

        public bool EncryptionRequired
        {
            get { return _innerObject.EncryptionRequired; }
            set { _innerObject.EncryptionRequired = value; }
        }

        public string TypeConverter
        {
            get { return _innerObject.TypeConverter; }
            set { _innerObject.TypeConverter = value; }
        }

        public string UITypeEditor
        {
            get { return _innerObject.UITypeEditor; }
            set { _innerObject.UITypeEditor = value; }
        }

        public bool ContainsID
        {
            get { return _innerObject.ContainsID; }
            set { _innerObject.ContainsID = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSCustomPropertyExpressionType ExpressionType
        {
            get { return _innerObject.ExpressionType; }
            set { _innerObject.ExpressionType = value; }
        }
    }

    public class BipsProxyIDTSInput100
    {
        private readonly IDTSInput100 _innerObject;

        internal BipsProxyIDTSInput100(IDTSInput100 innerObject)
        {
            _innerObject = innerObject;
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSVirtualInput100 GetVirtualInput()
        {
            return _innerObject.GetVirtualInput();
        }

        public void SuggestNameBasedLineageIDMappings(ref Int32[] ppsaOldIDs, ref Int32[] ppsaNewIDs)
        {
            _innerObject.SuggestNameBasedLineageIDMappings(ref ppsaOldIDs, ref ppsaNewIDs);
        }

        public int ID
        {
            get { return _innerObject.ID; }
            set { _innerObject.ID = value; }
        }

        public string Description
        {
            get { return _innerObject.Description; }
            set { _innerObject.Description = value; }
        }

        public string Name
        {
            get { return _innerObject.Name; }
            set { _innerObject.Name = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSObjectType ObjectType
        {
            get { return _innerObject.ObjectType; }
        }

        public string IdentificationString
        {
            get { return _innerObject.IdentificationString; }
        }

        public bool HasSideEffects
        {
            get { return _innerObject.HasSideEffects; }
            set { _innerObject.HasSideEffects = value; }
        }

        public int Buffer
        {
            get { return _innerObject.Buffer; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomPropertyCollection100 CustomPropertyCollection
        {
            get { return _innerObject.CustomPropertyCollection; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSInputColumnCollection100 InputColumnCollection
        {
            get { return _innerObject.InputColumnCollection; }
        }

        public bool Dangling
        {
            get { return _innerObject.Dangling; }
            set { _innerObject.Dangling = value; }
        }

        public bool IsAttached
        {
            get { return _innerObject.IsAttached; }
        }

        public bool IsSorted
        {
            get { return _innerObject.IsSorted; }
        }

        public int SourceLocale
        {
            get { return _innerObject.SourceLocale; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSRowDisposition ErrorRowDisposition
        {
            get { return _innerObject.ErrorRowDisposition; }
            set { _innerObject.ErrorRowDisposition = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSRowDisposition TruncationRowDisposition
        {
            get { return _innerObject.TruncationRowDisposition; }
            set { _innerObject.TruncationRowDisposition = value; }
        }

        public string ErrorOrTruncationOperation
        {
            get { return _innerObject.ErrorOrTruncationOperation; }
            set { _innerObject.ErrorOrTruncationOperation = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSExternalMetadataColumnCollection100
            ExternalMetadataColumnCollection
        {
            get { return _innerObject.ExternalMetadataColumnCollection; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100 Component
        {
            get { return _innerObject.Component; }
        }

        public int BufferBase
        {
            get { return _innerObject.BufferBase; }
        }

        public bool AreInputColumnsAssociatedWithOutputColumns
        {
            get { return _innerObject.AreInputColumnsAssociatedWithOutputColumns; }
        }
    }

    public class BipsProxyIDTSOutput100
    {
        private readonly IDTSOutput100 _innerObject;

        internal BipsProxyIDTSOutput100(IDTSOutput100 innerObject)
        {
            _innerObject = innerObject;
        }

        public int ID
        {
            get { return _innerObject.ID; }
            set { _innerObject.ID = value; }
        }

        public string Description
        {
            get { return _innerObject.Description; }
            set { _innerObject.Description = value; }
        }

        public string Name
        {
            get { return _innerObject.Name; }
            set { _innerObject.Name = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSObjectType ObjectType
        {
            get { return _innerObject.ObjectType; }
        }

        public string IdentificationString
        {
            get { return _innerObject.IdentificationString; }
        }

        public int ExclusionGroup
        {
            get { return _innerObject.ExclusionGroup; }
            set { _innerObject.ExclusionGroup = value; }
        }

        public int SynchronousInputID
        {
            get { return _innerObject.SynchronousInputID; }
            set { _innerObject.SynchronousInputID = value; }
        }

        public int Buffer
        {
            get { return _innerObject.Buffer; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomPropertyCollection100 CustomPropertyCollection
        {
            get { return _innerObject.CustomPropertyCollection; }
        }

        public bool DeleteOutputOnPathDetached
        {
            get { return _innerObject.DeleteOutputOnPathDetached; }
            set { _innerObject.DeleteOutputOnPathDetached = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutputColumnCollection100 OutputColumnCollection
        {
            get { return _innerObject.OutputColumnCollection; }
        }

        public bool HasSideEffects
        {
            get { return _innerObject.HasSideEffects; }
            set { _innerObject.HasSideEffects = value; }
        }

        public bool IsErrorOut
        {
            get { return _innerObject.IsErrorOut; }
            set { _innerObject.IsErrorOut = value; }
        }

        public bool IsAttached
        {
            get { return _innerObject.IsAttached; }
        }

        public bool IsSorted
        {
            get { return _innerObject.IsSorted; }
            set { _innerObject.IsSorted = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSRowDisposition ErrorRowDisposition
        {
            get { return _innerObject.ErrorRowDisposition; }
            set { _innerObject.ErrorRowDisposition = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSRowDisposition TruncationRowDisposition
        {
            get { return _innerObject.TruncationRowDisposition; }
            set { _innerObject.TruncationRowDisposition = value; }
        }

        public string ErrorOrTruncationOperation
        {
            get { return _innerObject.ErrorOrTruncationOperation; }
            set { _innerObject.ErrorOrTruncationOperation = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSExternalMetadataColumnCollection100
            ExternalMetadataColumnCollection
        {
            get { return _innerObject.ExternalMetadataColumnCollection; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100 Component
        {
            get { return _innerObject.Component; }
        }

        public bool Dangling
        {
            get { return _innerObject.Dangling; }
            set { _innerObject.Dangling = value; }
        }
    }

    public class BipsProxyIDTSExternalMetadataColumn100
    {
        private readonly IDTSExternalMetadataColumn100 _innerObject;

        internal BipsProxyIDTSExternalMetadataColumn100(IDTSExternalMetadataColumn100 innerObject)
        {
            _innerObject = innerObject;
        }

        public int ID
        {
            get { return _innerObject.ID; }
            set { _innerObject.ID = value; }
        }

        public string Description
        {
            get { return _innerObject.Description; }
            set { _innerObject.Description = value; }
        }

        public string Name
        {
            get { return _innerObject.Name; }
            set { _innerObject.Name = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSObjectType ObjectType
        {
            get { return _innerObject.ObjectType; }
        }

        public string IdentificationString
        {
            get { return _innerObject.IdentificationString; }
        }

        public Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType DataType
        {
            get { return _innerObject.DataType; }
            set { _innerObject.DataType = value; }
        }

        public int Length
        {
            get { return _innerObject.Length; }
            set { _innerObject.Length = value; }
        }

        public int Precision
        {
            get { return _innerObject.Precision; }
            set { _innerObject.Precision = value; }
        }

        public int Scale
        {
            get { return _innerObject.Scale; }
            set { _innerObject.Scale = value; }
        }

        public int CodePage
        {
            get { return _innerObject.CodePage; }
            set { _innerObject.CodePage = value; }
        }

        public int MappedColumnID
        {
            get { return _innerObject.MappedColumnID; }
            set { _innerObject.MappedColumnID = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomPropertyCollection100 CustomPropertyCollection
        {
            get { return _innerObject.CustomPropertyCollection; }
        }
    }

    public class BipsProxyIDTSFriendlyEnumCollection100
    {
        private readonly IDTSFriendlyEnumCollection100 _innerObject;

        internal BipsProxyIDTSFriendlyEnumCollection100(IDTSFriendlyEnumCollection100 innerObject)
        {
            _innerObject = innerObject;
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _innerObject.GetEnumerator();
        }

        public int Count
        {
            get { return _innerObject.Count; }
        }

        public bool IsFlags
        {
            get { return _innerObject.IsFlags; }
        }
    }

    public class BipsProxyCManagedComponentWrapper
    {
        private readonly CManagedComponentWrapper _innerObject;

        internal BipsProxyCManagedComponentWrapper(CManagedComponentWrapper innerObject)
        {
            _innerObject = innerObject;
        }


    }

    public class BipsProxyIDTSRuntimeComponent100
    {
        private readonly IDTSRuntimeComponent100 _innerObject;

        internal BipsProxyIDTSRuntimeComponent100(IDTSRuntimeComponent100 innerObject)
        {
            _innerObject = innerObject;
        }

        public void PrepareForExecute()
        {
            _innerObject.PrepareForExecute();
        }

        public void PreExecute()
        {
            _innerObject.PreExecute();
        }

        public void PrimeOutput(Int32 lOutputs, ref Int32 lOutputIDs, ref IDTSBuffer100 pIDTSBufferOutputs)
        {
            _innerObject.PrimeOutput(lOutputs, ref lOutputIDs, ref pIDTSBufferOutputs);
        }

        public void ProcessInput(Int32 lInputID, IDTSBuffer100 pIDTSBufferInput)
        {
            _innerObject.ProcessInput(lInputID, pIDTSBufferInput);
        }

        public void PostExecute()
        {
            _innerObject.PostExecute();
        }

        public void Cleanup()
        {
            _innerObject.Cleanup();
        }

        public void PerformUpgrade(Int32 lPipelineVersion)
        {
            _innerObject.PerformUpgrade(lPipelineVersion);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100 ComponentMetaData
        {
            set { _innerObject.ComponentMetaData = value; }
        }

        public Microsoft.SqlServer.Dts.Runtime.Wrapper.IDTSVariableDispenser100 VariableDispenser
        {
            set { _innerObject.VariableDispenser = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSBufferManager100 BufferManager
        {
            set { _innerObject.BufferManager = value; }
        }

        public Microsoft.SqlServer.Dts.Runtime.Wrapper.IDTSEventInfos100 EventInfos
        {
            set { _innerObject.EventInfos = value; }
        }

        public Microsoft.SqlServer.Dts.Runtime.Wrapper.IDTSObjectReferenceTracker100 ReferenceTracker
        {
            set { _innerObject.ReferenceTracker = value; }
        }

        public Microsoft.SqlServer.Dts.Runtime.Wrapper.IDTSLogEntryInfos100 LogEntryInfos
        {
            set { _innerObject.LogEntryInfos = value; }
        }

        public Microsoft.SqlServer.Dts.Runtime.Wrapper.DTSProductLevel RequiredProductLevel
        {
            get { return _innerObject.RequiredProductLevel; }
        }
    }

    public class BipsProxyIDTSInputCollection100
    {
        private readonly IDTSInputCollection100 _innerObject;

        internal BipsProxyIDTSInputCollection100(IDTSInputCollection100 innerObject)
        {
            _innerObject = innerObject;
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _innerObject.GetEnumerator();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSInput100 New()
        {
            return _innerObject.New();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSInput100 NewAt(Int32 lIndex)
        {
            return _innerObject.NewAt(lIndex);
        }

        public void RemoveObjectByIndex(Object Index)
        {
            _innerObject.RemoveObjectByIndex(Index);
        }

        public void RemoveObjectByID(Int32 lID)
        {
            _innerObject.RemoveObjectByID(lID);
        }

        public void RemoveAll()
        {
            _innerObject.RemoveAll();
        }

        public void SetIndex(Int32 lOldIndex, Int32 lNewIndex)
        {
            _innerObject.SetIndex(lOldIndex, lNewIndex);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSInput100 GetObjectByID(Int32 lID)
        {
            return _innerObject.GetObjectByID(lID);
        }

        public int GetObjectIndexByID(Int32 lID)
        {
            return _innerObject.GetObjectIndexByID(lID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSInput100 FindObjectByID(Int32 lID)
        {
            return _innerObject.FindObjectByID(lID);
        }

        public int FindObjectIndexByID(Int32 lID)
        {
            return _innerObject.FindObjectIndexByID(lID);
        }

        public int Count
        {
            get { return _innerObject.Count; }
        }
    }

    public class BipsProxyIDTSOutputCollection100
    {
        private readonly IDTSOutputCollection100 _innerObject;

        internal BipsProxyIDTSOutputCollection100(IDTSOutputCollection100 innerObject)
        {
            _innerObject = innerObject;
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _innerObject.GetEnumerator();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutput100 New()
        {
            return _innerObject.New();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutput100 NewAt(Int32 lIndex)
        {
            return _innerObject.NewAt(lIndex);
        }

        public void RemoveObjectByIndex(Object Index)
        {
            _innerObject.RemoveObjectByIndex(Index);
        }

        public void RemoveObjectByID(Int32 lID)
        {
            _innerObject.RemoveObjectByID(lID);
        }

        public void RemoveAll()
        {
            _innerObject.RemoveAll();
        }

        public void SetIndex(Int32 lOldIndex, Int32 lNewIndex)
        {
            _innerObject.SetIndex(lOldIndex, lNewIndex);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutput100 GetObjectByID(Int32 lID)
        {
            return _innerObject.GetObjectByID(lID);
        }

        public int GetObjectIndexByID(Int32 lID)
        {
            return _innerObject.GetObjectIndexByID(lID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutput100 FindObjectByID(Int32 lID)
        {
            return _innerObject.FindObjectByID(lID);
        }

        public int FindObjectIndexByID(Int32 lID)
        {
            return _innerObject.FindObjectIndexByID(lID);
        }

        public int Count
        {
            get { return _innerObject.Count; }
        }
    }

    public class BipsProxyIDTSRuntimeConnectionCollection100
    {
        private readonly IDTSRuntimeConnectionCollection100 _innerObject;

        internal BipsProxyIDTSRuntimeConnectionCollection100(IDTSRuntimeConnectionCollection100 innerObject)
        {
            _innerObject = innerObject;
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _innerObject.GetEnumerator();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSRuntimeConnection100 New()
        {
            return _innerObject.New();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSRuntimeConnection100 NewAt(Int32 lIndex)
        {
            return _innerObject.NewAt(lIndex);
        }

        public void RemoveObjectByIndex(Object Index)
        {
            _innerObject.RemoveObjectByIndex(Index);
        }

        public void RemoveObjectByID(Int32 lID)
        {
            _innerObject.RemoveObjectByID(lID);
        }

        public void RemoveAll()
        {
            _innerObject.RemoveAll();
        }

        public void SetIndex(Int32 lOldIndex, Int32 lNewIndex)
        {
            _innerObject.SetIndex(lOldIndex, lNewIndex);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSRuntimeConnection100 GetObjectByID(Int32 lID)
        {
            return _innerObject.GetObjectByID(lID);
        }

        public int GetObjectIndexByID(Int32 lID)
        {
            return _innerObject.GetObjectIndexByID(lID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSRuntimeConnection100 FindObjectByID(Int32 lID)
        {
            return _innerObject.FindObjectByID(lID);
        }

        public int FindObjectIndexByID(Int32 lID)
        {
            return _innerObject.FindObjectIndexByID(lID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSRuntimeConnection100 GetRuntimeConnectionByName(
            String bstrName)
        {
            return _innerObject.GetRuntimeConnectionByName(bstrName);
        }

        public int Count
        {
            get { return _innerObject.Count; }
        }
    }

    public class BipsProxyIDTSComponentView100
    {
        private readonly IDTSComponentView100 _innerObject;

        internal BipsProxyIDTSComponentView100(IDTSComponentView100 innerObject)
        {
            _innerObject = innerObject;
        }

        public void Commit()
        {
            _innerObject.Commit();
        }

        public void Cancel()
        {
            _innerObject.Cancel();
        }
    }

    public class BipsProxyIDTSPath100
    {
        private readonly IDTSPath100 _innerObject;

        internal BipsProxyIDTSPath100(IDTSPath100 innerObject)
        {
            _innerObject = innerObject;
        }

        public void AttachPathAndPropagateNotifications(IDTSOutput100 pIDTSOutput, IDTSInput100 pIDTSInput)
        {
            _innerObject.AttachPathAndPropagateNotifications(pIDTSOutput, pIDTSInput);
        }

        public int ID
        {
            get { return _innerObject.ID; }
            set { _innerObject.ID = value; }
        }

        public string Description
        {
            get { return _innerObject.Description; }
            set { _innerObject.Description = value; }
        }

        public string Name
        {
            get { return _innerObject.Name; }
            set { _innerObject.Name = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSObjectType ObjectType
        {
            get { return _innerObject.ObjectType; }
        }

        public string IdentificationString
        {
            get { return _innerObject.IdentificationString; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutput100 StartPoint
        {
            get { return _innerObject.StartPoint; }
            set { _innerObject.StartPoint = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSInput100 EndPoint
        {
            get { return _innerObject.EndPoint; }
            set { _innerObject.EndPoint = value; }
        }

        public bool Visualized
        {
            set { _innerObject.Visualized = value; }
        }
    }

    public class BipsProxyIDTSVirtualInputColumnCollection100
    {
        private readonly IDTSVirtualInputColumnCollection100 _innerObject;

        internal BipsProxyIDTSVirtualInputColumnCollection100(IDTSVirtualInputColumnCollection100 innerObject)
        {
            _innerObject = innerObject;
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _innerObject.GetEnumerator();
        }

        public void SetIndex(Int32 lOldIndex, Int32 lNewIndex)
        {
            _innerObject.SetIndex(lOldIndex, lNewIndex);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSVirtualInputColumn100 GetObjectByID(Int32 lID)
        {
            return _innerObject.GetObjectByID(lID);
        }

        public int GetObjectIndexByID(Int32 lID)
        {
            return _innerObject.GetObjectIndexByID(lID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSVirtualInputColumn100 FindObjectByID(Int32 lID)
        {
            return _innerObject.FindObjectByID(lID);
        }

        public int FindObjectIndexByID(Int32 lID)
        {
            return _innerObject.FindObjectIndexByID(lID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSVirtualInputColumn100 GetVirtualInputColumnByLineageID(
            Int32 lLineageID)
        {
            return _innerObject.GetVirtualInputColumnByLineageID(lLineageID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSVirtualInputColumn100 GetVirtualInputColumnByName(
            String bstrComponentName, String bstrName)
        {
            return _innerObject.GetVirtualInputColumnByName(bstrComponentName, bstrName);
        }

        public int Count
        {
            get { return _innerObject.Count; }
        }
    }

    public class BipsProxyIDTSInputColumnCollection100
    {
        private readonly IDTSInputColumnCollection100 _innerObject;

        internal BipsProxyIDTSInputColumnCollection100(IDTSInputColumnCollection100 innerObject)
        {
            _innerObject = innerObject;
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _innerObject.GetEnumerator();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSInputColumn100 New()
        {
            return _innerObject.New();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSInputColumn100 NewAt(Int32 lIndex)
        {
            return _innerObject.NewAt(lIndex);
        }

        public void RemoveObjectByIndex(Object Index)
        {
            _innerObject.RemoveObjectByIndex(Index);
        }

        public void RemoveObjectByID(Int32 lID)
        {
            _innerObject.RemoveObjectByID(lID);
        }

        public void RemoveAll()
        {
            _innerObject.RemoveAll();
        }

        public void SetIndex(Int32 lOldIndex, Int32 lNewIndex)
        {
            _innerObject.SetIndex(lOldIndex, lNewIndex);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSInputColumn100 GetObjectByID(Int32 lID)
        {
            return _innerObject.GetObjectByID(lID);
        }

        public int GetObjectIndexByID(Int32 lID)
        {
            return _innerObject.GetObjectIndexByID(lID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSInputColumn100 FindObjectByID(Int32 lID)
        {
            return _innerObject.FindObjectByID(lID);
        }

        public int FindObjectIndexByID(Int32 lID)
        {
            return _innerObject.FindObjectIndexByID(lID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSInputColumn100 GetInputColumnByLineageID(Int32 lLineageID)
        {
            return _innerObject.GetInputColumnByLineageID(lLineageID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSInputColumn100 GetInputColumnByName(
            String bstrComponentName, String bstrName)
        {
            return _innerObject.GetInputColumnByName(bstrComponentName, bstrName);
        }

        public int Count
        {
            get { return _innerObject.Count; }
        }
    }

    public class BipsProxyIDTSExternalMetadataColumnCollection100
    {
        private readonly IDTSExternalMetadataColumnCollection100 _innerObject;

        internal BipsProxyIDTSExternalMetadataColumnCollection100(IDTSExternalMetadataColumnCollection100 innerObject)
        {
            _innerObject = innerObject;
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _innerObject.GetEnumerator();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSExternalMetadataColumn100 New()
        {
            return _innerObject.New();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSExternalMetadataColumn100 NewAt(Int32 lIndex)
        {
            return _innerObject.NewAt(lIndex);
        }

        public void RemoveObjectByIndex(Object Index)
        {
            _innerObject.RemoveObjectByIndex(Index);
        }

        public void RemoveObjectByID(Int32 lID)
        {
            _innerObject.RemoveObjectByID(lID);
        }

        public void RemoveAll()
        {
            _innerObject.RemoveAll();
        }

        public void SetIndex(Int32 lOldIndex, Int32 lNewIndex)
        {
            _innerObject.SetIndex(lOldIndex, lNewIndex);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSExternalMetadataColumn100 GetObjectByID(Int32 lID)
        {
            return _innerObject.GetObjectByID(lID);
        }

        public int GetObjectIndexByID(Int32 lID)
        {
            return _innerObject.GetObjectIndexByID(lID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSExternalMetadataColumn100 FindObjectByID(Int32 lID)
        {
            return _innerObject.FindObjectByID(lID);
        }

        public int FindObjectIndexByID(Int32 lID)
        {
            return _innerObject.FindObjectIndexByID(lID);
        }

        public int Count
        {
            get { return _innerObject.Count; }
        }

        public bool IsUsed
        {
            get { return _innerObject.IsUsed; }
            set { _innerObject.IsUsed = value; }
        }
    }

    public class BipsProxyIDTSOutputColumnCollection100
    {
        private readonly IDTSOutputColumnCollection100 _innerObject;

        internal BipsProxyIDTSOutputColumnCollection100(IDTSOutputColumnCollection100 innerObject)
        {
            _innerObject = innerObject;
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _innerObject.GetEnumerator();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutputColumn100 New()
        {
            return _innerObject.New();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutputColumn100 NewAt(Int32 lIndex)
        {
            return _innerObject.NewAt(lIndex);
        }

        public void RemoveObjectByIndex(Object Index)
        {
            _innerObject.RemoveObjectByIndex(Index);
        }

        public void RemoveObjectByID(Int32 lID)
        {
            _innerObject.RemoveObjectByID(lID);
        }

        public void RemoveAll()
        {
            _innerObject.RemoveAll();
        }

        public void SetIndex(Int32 lOldIndex, Int32 lNewIndex)
        {
            _innerObject.SetIndex(lOldIndex, lNewIndex);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutputColumn100 GetObjectByID(Int32 lID)
        {
            return _innerObject.GetObjectByID(lID);
        }

        public int GetObjectIndexByID(Int32 lID)
        {
            return _innerObject.GetObjectIndexByID(lID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutputColumn100 FindObjectByID(Int32 lID)
        {
            return _innerObject.FindObjectByID(lID);
        }

        public int FindObjectIndexByID(Int32 lID)
        {
            return _innerObject.FindObjectIndexByID(lID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutputColumn100 GetOutputColumnByLineageID(Int32 lLineageID)
        {
            return _innerObject.GetOutputColumnByLineageID(lLineageID);
        }

        public int Count
        {
            get { return _innerObject.Count; }
        }
    }

    public class BipsProxyIDTSFriendlyEnum100
    {
        private readonly IDTSFriendlyEnum100 _innerObject;

        internal BipsProxyIDTSFriendlyEnum100(IDTSFriendlyEnum100 innerObject)
        {
            _innerObject = innerObject;
        }

        public string Name
        {
            get { return _innerObject.Name; }
        }

        public int Value
        {
            get { return _innerObject.Value; }
        }
    }

    public class BipsProxyIDTSInputColumnCachedInfo100
    {
        private readonly IDTSInputColumnCachedInfo100 _innerObject;

        internal BipsProxyIDTSInputColumnCachedInfo100(IDTSInputColumnCachedInfo100 innerObject)
        {
            _innerObject = innerObject;
        }

        public string CachedName
        {
            get { return _innerObject.CachedName; }
            set { _innerObject.CachedName = value; }
        }

        public Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType CachedDataType
        {
            get { return _innerObject.CachedDataType; }
            set { _innerObject.CachedDataType = value; }
        }

        public int CachedLength
        {
            get { return _innerObject.CachedLength; }
            set { _innerObject.CachedLength = value; }
        }

        public int CachedPrecision
        {
            get { return _innerObject.CachedPrecision; }
            set { _innerObject.CachedPrecision = value; }
        }

        public int CachedScale
        {
            get { return _innerObject.CachedScale; }
            set { _innerObject.CachedScale = value; }
        }

        public int CachedCodePage
        {
            get { return _innerObject.CachedCodePage; }
            set { _innerObject.CachedCodePage = value; }
        }

        public int CachedComparisonFlags
        {
            get { return _innerObject.CachedComparisonFlags; }
            set { _innerObject.CachedComparisonFlags = value; }
        }

        public int CachedSortKeyPosition
        {
            get { return _innerObject.CachedSortKeyPosition; }
            set { _innerObject.CachedSortKeyPosition = value; }
        }
    }

    public class BipsProxyIDTSVirtualInputColumn100
    {
        private readonly IDTSVirtualInputColumn100 _innerObject;

        internal BipsProxyIDTSVirtualInputColumn100(IDTSVirtualInputColumn100 innerObject)
        {
            _innerObject = innerObject;
        }

        public string DescribeRedirectedErrorCode(Int32 hrErrorCode)
        {
            return _innerObject.DescribeRedirectedErrorCode(hrErrorCode);
        }

        public int ID
        {
            get { return _innerObject.ID; }
            set { _innerObject.ID = value; }
        }

        public string Description
        {
            get { return _innerObject.Description; }
            set { _innerObject.Description = value; }
        }

        public string Name
        {
            get { return _innerObject.Name; }
            set { _innerObject.Name = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSObjectType ObjectType
        {
            get { return _innerObject.ObjectType; }
        }

        public string IdentificationString
        {
            get { return _innerObject.IdentificationString; }
        }

        public Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType DataType
        {
            get { return _innerObject.DataType; }
        }

        public int Length
        {
            get { return _innerObject.Length; }
        }

        public int Precision
        {
            get { return _innerObject.Precision; }
        }

        public int Scale
        {
            get { return _innerObject.Scale; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSUsageType UsageType
        {
            get { return _innerObject.UsageType; }
        }

        public int LineageID
        {
            get { return _innerObject.LineageID; }
        }

        public string SourceComponent
        {
            get { return _innerObject.SourceComponent; }
        }

        public string NewName
        {
            get { return _innerObject.NewName; }
            set { _innerObject.NewName = value; }
        }

        public string NewDescription
        {
            get { return _innerObject.NewDescription; }
            set { _innerObject.NewDescription = value; }
        }

        public int CodePage
        {
            get { return _innerObject.CodePage; }
        }

        public int SortKeyPosition
        {
            get { return _innerObject.SortKeyPosition; }
        }

        public int ComparisonFlags
        {
            get { return _innerObject.ComparisonFlags; }
        }

        public string UpstreamComponentName
        {
            get { return _innerObject.UpstreamComponentName; }
        }

        public bool IsAssociatedWithOutputColumn
        {
            get { return _innerObject.IsAssociatedWithOutputColumn; }
        }
    }

    public class BipsProxyIDTSRuntimeConnection100
    {
        private readonly IDTSRuntimeConnection100 _innerObject;

        internal BipsProxyIDTSRuntimeConnection100(IDTSRuntimeConnection100 innerObject)
        {
            _innerObject = innerObject;
        }

        public void ReleaseConnectionManager()
        {
            _innerObject.ReleaseConnectionManager();
        }

        public int ID
        {
            get { return _innerObject.ID; }
            set { _innerObject.ID = value; }
        }

        public string Description
        {
            get { return _innerObject.Description; }
            set { _innerObject.Description = value; }
        }

        public string Name
        {
            get { return _innerObject.Name; }
            set { _innerObject.Name = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSObjectType ObjectType
        {
            get { return _innerObject.ObjectType; }
        }

        public string IdentificationString
        {
            get { return _innerObject.IdentificationString; }
        }

        public string ConnectionManagerID
        {
            get { return _innerObject.ConnectionManagerID; }
            set { _innerObject.ConnectionManagerID = value; }
        }

        public Microsoft.SqlServer.Dts.Runtime.Wrapper.IDTSConnectionManager100 ConnectionManager
        {
            get { return _innerObject.ConnectionManager; }
            set { _innerObject.ConnectionManager = value; }
        }
    }

    public class BipsProxyIDTSManagedComponentWrapper100
    {
        private readonly IDTSManagedComponentWrapper100 _innerObject;

        internal BipsProxyIDTSManagedComponentWrapper100(IDTSManagedComponentWrapper100 innerObject)
        {
            _innerObject = innerObject;
        }

        public void AddBLOBData(IDTSBuffer100 pIDTSBuffer, Int32 hRow, Int32 hCol, ref Byte[] ppsaData)
        {
            _innerObject.AddBLOBData(pIDTSBuffer, hRow, hCol, ref ppsaData);
        }

        public void AddBLOBDataLen(IDTSBuffer100 pIDTSBuffer, Int32 hRow, Int32 hCol, ref Byte[] ppsaData,
            UInt32 dwCount)
        {
            _innerObject.AddBLOBDataLen(pIDTSBuffer, hRow, hCol, ref ppsaData, dwCount);
        }

        public byte[] GetBLOBData(IDTSBuffer100 pIDTSBuffer, Int32 hRow, Int32 hCol, UInt32 dwOffset, UInt32 dwCount)
        {
            return _innerObject.GetBLOBData(pIDTSBuffer, hRow, hCol, dwOffset, dwCount);
        }

        public void WrapperProvideComponentProperties()
        {
            _innerObject.WrapperProvideComponentProperties();
        }

        public void WrapperReinitializeMetaData()
        {
            _innerObject.WrapperReinitializeMetaData();
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutputColumn100 WrapperInsertOutputColumnAt(Int32 lOutputID,
            Int32 lOutputColumnIndex, String bstrName, String bstrDescription)
        {
            return _innerObject.WrapperInsertOutputColumnAt(lOutputID, lOutputColumnIndex, bstrName, bstrDescription);
        }

        public void WrapperDeleteOutputColumn(Int32 lOutputID, Int32 lOutputColumnID)
        {
            _innerObject.WrapperDeleteOutputColumn(lOutputID, lOutputColumnID);
        }

        public void WrapperOnDeletingInputColumn(Int32 lInputID, Int32 lInputColumnID)
        {
            _innerObject.WrapperOnDeletingInputColumn(lInputID, lInputColumnID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSInputColumn100 WrapperSetUsageType(Int32 lInputID,
            IDTSVirtualInput100 pIDTSVirtualInputObject, Int32 lLineageID, DTSUsageType eUsageType)
        {
            return _innerObject.WrapperSetUsageType(lInputID, pIDTSVirtualInputObject, lLineageID, eUsageType);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100 WrapperSetComponentProperty(
            String PropertyName, Object vValue)
        {
            return _innerObject.WrapperSetComponentProperty(PropertyName, vValue);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100 WrapperSetInputProperty(Int32 lInputID,
            String PropertyName, Object vValue)
        {
            return _innerObject.WrapperSetInputProperty(lInputID, PropertyName, vValue);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100 WrapperSetOutputProperty(Int32 lOutputID,
            String PropertyName, Object vValue)
        {
            return _innerObject.WrapperSetOutputProperty(lOutputID, PropertyName, vValue);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100 WrapperSetInputColumnProperty(
            Int32 lInputID, Int32 lInputColumnID, String PropertyName, Object vValue)
        {
            return _innerObject.WrapperSetInputColumnProperty(lInputID, lInputColumnID, PropertyName, vValue);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100 WrapperSetOutputColumnProperty(
            Int32 lOutputID, Int32 lOutputColumnID, String PropertyName, Object vValue)
        {
            return _innerObject.WrapperSetOutputColumnProperty(lOutputID, lOutputColumnID, PropertyName, vValue);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSInput100 WrapperInsertInput(
            DTSInsertPlacement eInsertPlacement, Int32 lInputID)
        {
            return _innerObject.WrapperInsertInput(eInsertPlacement, lInputID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutput100 WrapperInsertOutput(
            DTSInsertPlacement eInsertPlacement, Int32 lOutputID)
        {
            return _innerObject.WrapperInsertOutput(eInsertPlacement, lOutputID);
        }

        public void WrapperDeleteInput(Int32 lInputID)
        {
            _innerObject.WrapperDeleteInput(lInputID);
        }

        public void WrapperDeleteOutput(Int32 lOutputID)
        {
            _innerObject.WrapperDeleteOutput(lOutputID);
        }

        public void WrapperOnInputPathDetached(Int32 lInputID)
        {
            _innerObject.WrapperOnInputPathDetached(lInputID);
        }

        public void WrapperOnInputPathAttached(Int32 lInputID)
        {
            _innerObject.WrapperOnInputPathAttached(lInputID);
        }

        public void WrapperOnOutputPathAttached(Int32 lOutputID)
        {
            _innerObject.WrapperOnOutputPathAttached(lOutputID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSValidationStatus WrapperValidate()
        {
            return _innerObject.WrapperValidate();
        }

        public void WrapperAcquireConnections(Object pTransaction)
        {
            _innerObject.WrapperAcquireConnections(pTransaction);
        }

        public void WrapperReleaseConnections()
        {
            _innerObject.WrapperReleaseConnections();
        }

        public void WrapperSetOutputColumnDataTypeProperties(Int32 lOutputID, Int32 lOutputColumnID, DataType eDataType,
            Int32 lLength, Int32 lPrecision, Int32 lScale, Int32 lCodePage)
        {
            _innerObject.WrapperSetOutputColumnDataTypeProperties(lOutputID, lOutputColumnID, eDataType, lLength,
                lPrecision, lScale, lCodePage);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSExternalMetadataColumn100 WrapperMapInputColumn(
            Int32 lInputID, Int32 lInputColumnID, Int32 lExternalMetadataColumnID)
        {
            return _innerObject.WrapperMapInputColumn(lInputID, lInputColumnID, lExternalMetadataColumnID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSExternalMetadataColumn100 WrapperMapOutputColumn(
            Int32 lOutputID, Int32 lOutputColumnID, Int32 lExternalMetadataColumnID, Boolean bMatch)
        {
            return _innerObject.WrapperMapOutputColumn(lOutputID, lOutputColumnID, lExternalMetadataColumnID, bMatch);
        }

        public string WrapperDescribeRedirectedErrorCode(Int32 hrErrorCode)
        {
            return _innerObject.WrapperDescribeRedirectedErrorCode(hrErrorCode);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSExternalMetadataColumn100
            WrapperInsertExternalMetadataColumnAt(Int32 lID, Int32 lExternalMetadataColumnIndex, String bstrName,
                String bstrDescription)
        {
            return _innerObject.WrapperInsertExternalMetadataColumnAt(lID, lExternalMetadataColumnIndex, bstrName,
                bstrDescription);
        }

        public void WrapperDeleteExternalMetadataColumn(Int32 lID, Int32 lExternalMetadataColumnID)
        {
            _innerObject.WrapperDeleteExternalMetadataColumn(lID, lExternalMetadataColumnID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100 WrapperSetExternalMetadataColumnProperty(
            Int32 lID, Int32 lExternalMetadataColumnID, String bstrPropertyName, Object vtValue)
        {
            return _innerObject.WrapperSetExternalMetadataColumnProperty(lID, lExternalMetadataColumnID,
                bstrPropertyName, vtValue);
        }

        public void WrapperSetExternalMetadataColumnDataTypeProperties(Int32 lID, Int32 lExternalMetadataColumnID,
            DataType eDataType, Int32 lLength, Int32 lPrecision, Int32 lScale, Int32 lCodePage)
        {
            _innerObject.WrapperSetExternalMetadataColumnDataTypeProperties(lID, lExternalMetadataColumnID, eDataType,
                lLength, lPrecision, lScale, lCodePage);
        }
    }

    public class BipsProxyIDTSManagedComponentHost100
    {
        private readonly IDTSManagedComponentHost100 _innerObject;

        internal BipsProxyIDTSManagedComponentHost100(IDTSManagedComponentHost100 innerObject)
        {
            _innerObject = innerObject;
        }

        public void Instantiate(String bstrUserComponentTypeName, IDTSComponentMetaData100 pComponentMetaData,
            IDTSVariableDispenser100 pVariableDispenser, IDTSBufferManager100 pBufferManager,
            IDTSEventInfos100 pEventInfos, IDTSObjectReferenceTracker100 pRefTracker,
            IDTSLogEntryInfos100 pLogEntryInfos, out DTSProductLevel peProductLevel)
        {
            _innerObject.Instantiate(bstrUserComponentTypeName, pComponentMetaData, pVariableDispenser, pBufferManager,
                pEventInfos, pRefTracker, pLogEntryInfos, out peProductLevel);
        }

        public void HostPrepareForExecute(IDTSManagedComponentWrapper100 pWrapper)
        {
            _innerObject.HostPrepareForExecute(pWrapper);
        }

        public void HostPreExecute(IDTSManagedComponentWrapper100 pWrapper)
        {
            _innerObject.HostPreExecute(pWrapper);
        }

        public void HostPrimeOutput(IDTSManagedComponentWrapper100 pWrapper, Int32 lOutputs, Int32[] psaOutputIDs,
            IDTSBuffer100[] psaBuffers, IntPtr ppBufferPacket)
        {
            _innerObject.HostPrimeOutput(pWrapper, lOutputs, psaOutputIDs, psaBuffers, ppBufferPacket);
        }

        public void HostProcessInput(IDTSManagedComponentWrapper100 pWrapper, Int32 lInputID, IDTSBuffer100 pDTSBuffer,
            IntPtr pBufferPacket)
        {
            _innerObject.HostProcessInput(pWrapper, lInputID, pDTSBuffer, pBufferPacket);
        }

        public void HostPostExecute(IDTSManagedComponentWrapper100 pWrapper)
        {
            _innerObject.HostPostExecute(pWrapper);
        }

        public void HostCleanup(IDTSManagedComponentWrapper100 pWrapper)
        {
            _innerObject.HostCleanup(pWrapper);
        }

        public void HostCheckAndPerformUpgrade(IDTSManagedComponentWrapper100 pWrapper, Int32 lPipelineVersion)
        {
            _innerObject.HostCheckAndPerformUpgrade(pWrapper, lPipelineVersion);
        }

        public void HostProvideComponentProperties(IDTSManagedComponentWrapper100 pWrapper)
        {
            _innerObject.HostProvideComponentProperties(pWrapper);
        }

        public void HostReinitializeMetaData(IDTSManagedComponentWrapper100 pWrapper)
        {
            _innerObject.HostReinitializeMetaData(pWrapper);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutputColumn100 HostInsertOutputColumnAt(
            IDTSManagedComponentWrapper100 pWrapper, Int32 lOutputID, Int32 lOutputColumnIndex, String bstrName,
            String bstrDescription)
        {
            return _innerObject.HostInsertOutputColumnAt(pWrapper, lOutputID, lOutputColumnIndex, bstrName,
                bstrDescription);
        }

        public void HostDeleteOutputColumn(IDTSManagedComponentWrapper100 pWrapper, Int32 lOutputID,
            Int32 lOutputColumnID)
        {
            _innerObject.HostDeleteOutputColumn(pWrapper, lOutputID, lOutputColumnID);
        }

        public void HostOnDeletingInputColumn(IDTSManagedComponentWrapper100 pWrapper, Int32 lInputID,
            Int32 lInputColumnID)
        {
            _innerObject.HostOnDeletingInputColumn(pWrapper, lInputID, lInputColumnID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSInputColumn100 HostSetUsageType(
            IDTSManagedComponentWrapper100 pWrapper, Int32 lInputID, IDTSVirtualInput100 pIDTSVirtualInputObject,
            Int32 lLineageID, DTSUsageType eUsageType)
        {
            return _innerObject.HostSetUsageType(pWrapper, lInputID, pIDTSVirtualInputObject, lLineageID, eUsageType);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100 HostSetComponentProperty(
            IDTSManagedComponentWrapper100 pWrapper, String PropertyName, Object vValue)
        {
            return _innerObject.HostSetComponentProperty(pWrapper, PropertyName, vValue);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100 HostSetInputProperty(
            IDTSManagedComponentWrapper100 pWrapper, Int32 lInputID, String PropertyName, Object vValue)
        {
            return _innerObject.HostSetInputProperty(pWrapper, lInputID, PropertyName, vValue);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100 HostSetOutputProperty(
            IDTSManagedComponentWrapper100 pWrapper, Int32 lOutputID, String PropertyName, Object vValue)
        {
            return _innerObject.HostSetOutputProperty(pWrapper, lOutputID, PropertyName, vValue);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100 HostSetInputColumnProperty(
            IDTSManagedComponentWrapper100 pWrapper, Int32 lInputID, Int32 lInputColumnID, String PropertyName,
            Object vValue)
        {
            return _innerObject.HostSetInputColumnProperty(pWrapper, lInputID, lInputColumnID, PropertyName, vValue);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100 HostSetOutputColumnProperty(
            IDTSManagedComponentWrapper100 pWrapper, Int32 lOutputID, Int32 lOutputColumnID, String PropertyName,
            Object vValue)
        {
            return _innerObject.HostSetOutputColumnProperty(pWrapper, lOutputID, lOutputColumnID, PropertyName, vValue);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSInput100 HostInsertInput(
            IDTSManagedComponentWrapper100 pWrapper, DTSInsertPlacement eInsertPlacement, Int32 lInputID)
        {
            return _innerObject.HostInsertInput(pWrapper, eInsertPlacement, lInputID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutput100 HostInsertOutput(
            IDTSManagedComponentWrapper100 pWrapper, DTSInsertPlacement eInsertPlacement, Int32 lOutputID)
        {
            return _innerObject.HostInsertOutput(pWrapper, eInsertPlacement, lOutputID);
        }

        public void HostDeleteInput(IDTSManagedComponentWrapper100 pWrapper, Int32 lInputID)
        {
            _innerObject.HostDeleteInput(pWrapper, lInputID);
        }

        public void HostDeleteOutput(IDTSManagedComponentWrapper100 pWrapper, Int32 lOutputID)
        {
            _innerObject.HostDeleteOutput(pWrapper, lOutputID);
        }

        public void HostOnInputPathDetached(IDTSManagedComponentWrapper100 pWrapper, Int32 lInputID)
        {
            _innerObject.HostOnInputPathDetached(pWrapper, lInputID);
        }

        public void HostOnInputPathAttached(IDTSManagedComponentWrapper100 pWrapper, Int32 lInputID)
        {
            _innerObject.HostOnInputPathAttached(pWrapper, lInputID);
        }

        public void HostOnOutputPathAttached(IDTSManagedComponentWrapper100 pWrapper, Int32 lOutputID)
        {
            _innerObject.HostOnOutputPathAttached(pWrapper, lOutputID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.DTSValidationStatus HostValidate(
            IDTSManagedComponentWrapper100 pWrapper)
        {
            return _innerObject.HostValidate(pWrapper);
        }

        public void HostAcquireConnections(IDTSManagedComponentWrapper100 pWrapper, Object pTransaction)
        {
            _innerObject.HostAcquireConnections(pWrapper, pTransaction);
        }

        public void HostReleaseConnections(IDTSManagedComponentWrapper100 pWrapper)
        {
            _innerObject.HostReleaseConnections(pWrapper);
        }

        public void HostSetOutputColumnDataTypeProperties(IDTSManagedComponentWrapper100 pWrapper, Int32 lOutputID,
            Int32 lOutputColumnID, DataType eDataType, Int32 lLength, Int32 lPrecision, Int32 lScale, Int32 lCodePage)
        {
            _innerObject.HostSetOutputColumnDataTypeProperties(pWrapper, lOutputID, lOutputColumnID, eDataType, lLength,
                lPrecision, lScale, lCodePage);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSExternalMetadataColumn100 HostMapInputColumn(
            IDTSManagedComponentWrapper100 pWrapper, Int32 lInputID, Int32 lInputColumnID,
            Int32 lExternalMetadataColumnID)
        {
            return _innerObject.HostMapInputColumn(pWrapper, lInputID, lInputColumnID, lExternalMetadataColumnID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSExternalMetadataColumn100 HostMapOutputColumn(
            IDTSManagedComponentWrapper100 pWrapper, Int32 lOutputID, Int32 lOutputColumnID,
            Int32 lExternalMetadataColumnID, Boolean bMatch)
        {
            return _innerObject.HostMapOutputColumn(pWrapper, lOutputID, lOutputColumnID, lExternalMetadataColumnID,
                bMatch);
        }

        public string HostDescribeRedirectedErrorCode(IDTSManagedComponentWrapper100 pWrapper, Int32 hrErrorCode)
        {
            return _innerObject.HostDescribeRedirectedErrorCode(pWrapper, hrErrorCode);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSExternalMetadataColumn100 HostInsertExternalMetadataColumnAt
            (IDTSManagedComponentWrapper100 pWrapper, Int32 lID, Int32 lExternalMetadataColumnIndex, String bstrName,
                String bstrDescription)
        {
            return _innerObject.HostInsertExternalMetadataColumnAt(pWrapper, lID, lExternalMetadataColumnIndex, bstrName,
                bstrDescription);
        }

        public void HostDeleteExternalMetadataColumn(IDTSManagedComponentWrapper100 pWrapper, Int32 lID,
            Int32 lExternalMetadataColumnID)
        {
            _innerObject.HostDeleteExternalMetadataColumn(pWrapper, lID, lExternalMetadataColumnID);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSCustomProperty100 HostSetExternalMetadataColumnProperty(
            IDTSManagedComponentWrapper100 pWrapper, Int32 lID, Int32 lExternalMetadataColumnID, String bstrPropertyName,
            Object vtValue)
        {
            return _innerObject.HostSetExternalMetadataColumnProperty(pWrapper, lID, lExternalMetadataColumnID,
                bstrPropertyName, vtValue);
        }

        public void HostSetExternalMetadataColumnDataTypeProperties(IDTSManagedComponentWrapper100 pWrapper, Int32 lID,
            Int32 lExternalMetadataColumnID, DataType eDataType, Int32 lLength, Int32 lPrecision, Int32 lScale,
            Int32 lCodePage)
        {
            _innerObject.HostSetExternalMetadataColumnDataTypeProperties(pWrapper, lID, lExternalMetadataColumnID,
                eDataType, lLength, lPrecision, lScale, lCodePage);
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100 HostComponentMetaData
        {
            set { _innerObject.HostComponentMetaData = value; }
        }

        public Microsoft.SqlServer.Dts.Runtime.Wrapper.IDTSVariableDispenser100 HostVariableDispenser
        {
            set { _innerObject.HostVariableDispenser = value; }
        }

        public Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSBufferManager100 HostBufferManager
        {
            set { _innerObject.HostBufferManager = value; }
        }

        public Microsoft.SqlServer.Dts.Runtime.Wrapper.IDTSEventInfos100 HostEventInfos
        {
            set { _innerObject.HostEventInfos = value; }
        }

        public Microsoft.SqlServer.Dts.Runtime.Wrapper.IDTSObjectReferenceTracker100 HostReferenceTracker
        {
            set { _innerObject.HostReferenceTracker = value; }
        }

        public Microsoft.SqlServer.Dts.Runtime.Wrapper.IDTSLogEntryInfos100 HostLogEntryInfos
        {
            set { _innerObject.HostLogEntryInfos = value; }
        }
    }

    public class BipsProxyIDTSManagedComponent100
    {
        private readonly IDTSManagedComponent100 _innerObject;

        internal BipsProxyIDTSManagedComponent100(IDTSManagedComponent100 innerObject)
        {
            _innerObject = innerObject;
        }

        public System.Object InnerObject
        {
            get { return _innerObject.InnerObject; }
        }
    }

    public class BipsProxyIDTSBufferManagerInitialize100
    {
        private readonly IDTSBufferManagerInitialize100 _innerObject;

        internal BipsProxyIDTSBufferManagerInitialize100(IDTSBufferManagerInitialize100 innerObject)
        {
            _innerObject = innerObject;
        }

        public void SetEventInterfaces(IDTSInfoEvents100 pEvents, IDTSLogging100 pLogging)
        {
            _innerObject.SetEventInterfaces(pEvents, pLogging);
        }

        public void SetTempStorageLocations(String bstrBlobStorage, String bstrTempStorage)
        {
            _innerObject.SetTempStorageLocations(bstrBlobStorage, bstrTempStorage);
        }
    }

    public class BipsProxyIDTSPersistenceComponent100
    {
        private readonly IDTSPersistenceComponent100 _innerObject;

        internal BipsProxyIDTSPersistenceComponent100(IDTSPersistenceComponent100 innerObject)
        {
            _innerObject = innerObject;
        }

        public void Load(IDTSObjectModel100 pIDTSObjectModel,
            IDTSCustomPropertyCollection100 pIDTSCustomPropertyCollection)
        {
            _innerObject.Load(pIDTSObjectModel, pIDTSCustomPropertyCollection);
        }

        public void Save(IDTSObjectModel100 pIDTSObjectModel,
            IDTSCustomPropertyCollection100 pIDTSCustomPropertyCollection)
        {
            _innerObject.Save(pIDTSObjectModel, pIDTSCustomPropertyCollection);
        }

        public void ProvidePersistenceProperties(IDTSCustomPropertyCollection100 pIDTSCustomPropertyCollection)
        {
            _innerObject.ProvidePersistenceProperties(pIDTSCustomPropertyCollection);
        }
    }

    public class BipsProxyIDTSExpressionEvaluatorEx100
    {
        private readonly IDTSExpressionEvaluatorEx100 _innerObject;

        internal BipsProxyIDTSExpressionEvaluatorEx100(IDTSExpressionEvaluatorEx100 innerObject)
        {
            _innerObject = innerObject;
        }

        public void Parse(String bstrExpression, IDTSVariableDispenser100 pVariableDispenser,
            IDTSInputColumnCollection100 pColumnCollection)
        {
            _innerObject.Parse(bstrExpression, pVariableDispenser, pColumnCollection);
        }
    }

    public class BipsProxyIDTSMultiInputComponent100
    {
        private readonly IDTSMultiInputComponent100 _innerObject;

        internal BipsProxyIDTSMultiInputComponent100(IDTSMultiInputComponent100 innerObject)
        {
            _innerObject = innerObject;
        }

        public void CanProcess(Int32 inputCount, ref Int32 pInputIDs, ref Boolean pCanProcess)
        {
            _innerObject.CanProcess(inputCount, ref pInputIDs, ref pCanProcess);
        }

        public void GetDependencies(Int32 blockedInputID, ref Int32 pDependencyCount, ref Int32 pDependentInputIDs)
        {
            _innerObject.GetDependencies(blockedInputID, ref pDependencyCount, ref pDependentInputIDs);
        }
    }

    public class BipsProxyIDTSMultiInputComponentHost100
    {
        private readonly IDTSMultiInputComponentHost100 _innerObject;

        internal BipsProxyIDTSMultiInputComponentHost100(IDTSMultiInputComponentHost100 innerObject)
        {
            _innerObject = innerObject;
        }

        public void HostCanProcess(IDTSManagedComponentWrapper100 pWrapper, Int32 inputCount, Int32[] inputIDs,
            ref Boolean[] pCanProcess)
        {
            _innerObject.HostCanProcess(pWrapper, inputCount, inputIDs, ref pCanProcess);
        }

        public void HostGetDependencies(IDTSManagedComponentWrapper100 pWrapper, Int32 blockedInputID,
            ref Int32 pDependencyCount, ref Int32[] pDependentInputIDs)
        {
            _innerObject.HostGetDependencies(pWrapper, blockedInputID, ref pDependencyCount, ref pDependentInputIDs);
        }
    }

    public class BipsProxyIDTSSupportBackPressure100
    {
        private readonly IDTSSupportBackPressure100 _innerObject;

        internal BipsProxyIDTSSupportBackPressure100(IDTSSupportBackPressure100 innerObject)
        {
            _innerObject = innerObject;
        }

        public void GetSupport(ref Boolean pSupport)
        {
            _innerObject.GetSupport(ref pSupport);
        }
    }

    public class BipsProxyIDTSBufferTapConfiguration100
    {
        private readonly IDTSBufferTapConfiguration100 _innerObject;

        internal BipsProxyIDTSBufferTapConfiguration100(IDTSBufferTapConfiguration100 innerObject)
        {
            _innerObject = innerObject;
        }

        public void AddTapPoint(String pathIdentification, Int32 maxRowCount, String fileName)
        {
            _innerObject.AddTapPoint(pathIdentification, maxRowCount, fileName);
        }
    }

    public class BipsProxyIDTSDataFileCreator100
    {
        private readonly IDTSDataFileCreator100 _innerObject;

        internal BipsProxyIDTSDataFileCreator100(IDTSDataFileCreator100 innerObject)
        {
            _innerObject = innerObject;
        }

        public void CreateDataFile()
        {
            _innerObject.CreateDataFile();
        }
    }

    public class BipsProxyIDTSLookupDesign100
    {
        private readonly IDTSLookupDesign100 _innerObject;

        internal BipsProxyIDTSLookupDesign100(IDTSLookupDesign100 innerObject)
        {
            _innerObject = innerObject;
        }

        public int CacheRowSize
        {
            get { return _innerObject.CacheRowSize; }
        }
    }

    public class BipsProxyIDTSPivotDesign100
    {
        private readonly IDTSPivotDesign100 _innerObject;

        internal BipsProxyIDTSPivotDesign100(IDTSPivotDesign100 innerObject)
        {
            _innerObject = innerObject;
        }

        public void CreateNewOutputColumnsFromPivotKeyValueList(Int32 lPivotValueColumnID, String bstrPivotKeyValueList)
        {
            _innerObject.CreateNewOutputColumnsFromPivotKeyValueList(lPivotValueColumnID, bstrPivotKeyValueList);
        }
    }
}