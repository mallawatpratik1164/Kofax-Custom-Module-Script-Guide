using Kofax.Capture.SDK.CustomModule;
using Kofax.Capture.SDK.Data;
using System.Collections.Generic;

namespace MyCustomModule.Runtime
{
    internal class BatchProcessor
    {
        private const string BATCH = "Batch";
        private const string DOCUMENTS = "Documents";
        private const string DOCUMENT = "Document";
        private const string PDF_GENERATION_FILE_NAME = "PDFGenerationFileName";
        private const string BATCH_FIELDS = "BatchFields";
        private const string BATCH_FIELD = "BatchField";
        private const string INDEX_FIELDS = "IndexFields";
        private const string INDEX_FIELD = "IndexField";

        public void ProcessBatch(IBatch batch)
        {
            IACDataElement rootElement = batch.ExtractRuntimeACDataElement(0);
            IACDataElement batchElement = rootElement.FindChildElementByName(BATCH);

            IACDataElementCollection currentDocuments = GetElementsByName(batchElement, DOCUMENTS, DOCUMENT);

            for (int i = 0; i < currentDocuments.Count; i++)
            {
                int currentPageIndex = i + 1;
                IACDataElement currentDocument = currentDocuments[currentPageIndex];

                Dictionary<string, string> batchFields = GetKofaxFields(batchElement, BATCH_FIELDS, BATCH_FIELD);
                Dictionary<string, string> indexFields = GetKofaxFields(currentDocument, INDEX_FIELDS, INDEX_FIELD);
                // Dictionary<string, string> batchVariables = GetKofaxFields(batchElement, ...);

                string documentFilePath = currentDocument[PDF_GENERATION_FILE_NAME];

                // ...
            }

            batch.BatchClose(KfxDbState.KfxDbBatchReady, KfxDbQueue.KfxDbQueueNext, 0, "");
        }

        private IACDataElementCollection GetElementsByName(IACDataElement dataElement, string rootName, string targetName)
        {
            return dataElement.FindChildElementByName(rootName).FindChildElementsByName(targetName);
        }

        private Dictionary<string, string> GetKofaxFields(IACDataElement dataElement, string rootName, string targetName)
        {
            IACDataElementCollection fields = GetElementsByName(dataElement, rootName, targetName);
            Dictionary<string, string> fieldMap = new Dictionary<string, string>();

            foreach (IACDataElement field in fields)
            {
                string fieldKey = field["Name"];
                string fieldValue = field["Value"];
                fieldMap.Add(fieldKey, fieldValue);
            }

            return fieldMap;
        }
    }
}