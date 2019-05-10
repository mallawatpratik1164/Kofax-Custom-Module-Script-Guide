using Kofax.Capture.SDK.CustomModule;
using Kofax.Capture.SDK.Data;
using MyCustomModule.Properties;

namespace MyCustomModule.Runtime
{
    internal class BatchProcessor
    {
        public void ProcessBatch(IBatch batch)
        {
            IACDataElement batchElement = GetBatchElementFromBatch(batch);
            IACDataElementCollection currentDocuments = GetDocumentsFromBatchElement(batchElement);
            IACDataElement customStorageStrings = GetCustomStorageStringsFromBatch(batch);
            IACDataElementCollection batchFields = GetElementsByName(batchElement, Resources.BATCH_FIELDS, Resources.BATCH_FIELD);

            for (int i = 0; i < currentDocuments.Count; i++)
            {
                IACDataElement currentDocument = currentDocuments[i + 1];
                IACDataElementCollection indexFields = GetElementsByName(currentDocument, Resources.INDEX_FIELDS, Resources.INDEX_FIELD);

                /*
                
                ##############################################################################

                - Access Custom Storage String Values -

                IACDataElement customStorageItem = customStorageStrings.FindChildElementByAttribute(Resources.BATCH_CLASS_CUSTOM_STORAGE_STRING, Resources.NAME, "myCustomStorageStringName");
                string customStorageItemValue = customStorageItem[Resources.VALUE];
                
                // ...

                ##############################################################################

                - Access Batchfield Values -

                foreach (IACDataElement batchField in batchFields)
                {
                    if (batchField[Resources.NAME] == "myBatchFieldName")
                    {
                        string batchFieldValue = batchField[Resources.VALUE];

                        // ...
                    }
                }
                
                ##############################################################################

                - Access Indexfield Values -
                
                foreach (IACDataElement indexField in indexFields)
                {
                    if (indexField[Resources.NAME] == "myIndexFieldName")
                    {
                        string indexFieldValue = indexField[Resources.VALUE];

                        // ...
                    }
                }
                
                ##############################################################################

                - Access Batch Variables -
                
                string batchVariableValue = batchElement["myBatchVariableName"];

                // ...
                
                ##############################################################################

                - Access Document Variables -
                
                string documentVariableValue = currentDocument["myDocumentVariableName"];

                // ...
                
                */

                string documentFilePath = currentDocument[Resources.PDF_GENERATION_FILE_NAME];

                // ...
            }

            batch.BatchClose(KfxDbState.KfxDbBatchReady, KfxDbQueue.KfxDbQueueNext, 0, "");
        }
        
        private IACDataElement GetBatchElementFromBatch(IBatch batch)
        {
            IACDataElement rootElement = batch.ExtractRuntimeACDataElement(0);
            return rootElement.FindChildElementByName(Resources.BATCH);
        }

        private IACDataElementCollection GetDocumentsFromBatchElement(IACDataElement batchElement)
        {
            return GetElementsByName(batchElement, Resources.DOCUMENTS, Resources.DOCUMENT);
        }

        private IACDataElement GetCustomStorageStringsFromBatch(IBatch batch)
        {
            IACDataElement setupElement = batch.ExtractSetupACDataElement(0);
            IACDataElementCollection batchClasses = GetElementsByName(setupElement, Resources.BATCH_CLASSES, Resources.BATCH_CLASS);
            IACDataElement batchClass = batchClasses[1];
            return batchClass.FindChildElementByName(Resources.BATCH_CLASS_CUSTOM_STORAGE_STRINGS);
        }

        private IACDataElementCollection GetElementsByName(IACDataElement dataElement, string rootName, string targetName)
        {
            return dataElement.FindChildElementByName(rootName).FindChildElementsByName(targetName);
        }
    }
}