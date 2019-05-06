using Kofax.Capture.SDK.CustomModule;
using Kofax.Capture.SDK.Data;

namespace MyCustomModule.Runtime
{
    internal class BatchProcessor
    {
        #region Constants

        /// <summary>
        ///
        /// </summary>
        private const string BATCH = "Batch";

        /// <summary>
        ///
        /// </summary>
        private const string BATCH_CLASSES = "BatchClasses";

        /// <summary>
        ///
        /// </summary>
        private const string BATCH_CLASS = "BatchClass";

        /// <summary>
        ///
        /// </summary>
        private const string BATCH_CLASS_CUSTOM_STORAGE_STRINGS = "BatchClassCustomStorageStrings";

        /// <summary>
        ///
        /// </summary>
        private const string BATCH_CLASS_CUSTOM_STORAGE_STRING = "BatchClassCustomStorageString";

        /// <summary>
        ///
        /// </summary>
        private const string DOCUMENTS = "Documents";

        /// <summary>
        ///
        /// </summary>
        private const string DOCUMENT = "Document";

        /// <summary>
        ///
        /// </summary>
        private const string PDF_GENERATION_FILE_NAME = "PDFGenerationFileName";

        /// <summary>
        ///
        /// </summary>
        private const string BATCH_FIELDS = "BatchFields";

        /// <summary>
        ///
        /// </summary>
        private const string BATCH_FIELD = "BatchField";

        /// <summary>
        ///
        /// </summary>
        private const string INDEX_FIELDS = "IndexFields";

        /// <summary>
        ///
        /// </summary>
        private const string INDEX_FIELD = "IndexField";

        #endregion Constants

        #region Methods

        /// <summary>
        /// Processes the documents of a batch
        /// </summary>
        /// <param name="batch">The batch to process</param>
        public void ProcessBatch(IBatch batch)
        {
            IACDataElement setupElement = batch.ExtractSetupACDataElement(0);
            IACDataElement rootElement = batch.ExtractRuntimeACDataElement(0);

            IACDataElementCollection batchClasses = GetElementsByName(setupElement, BATCH_CLASSES, BATCH_CLASS);
            IACDataElement batchClass = batchClasses[1]; // Kofax uses one based indices
            IACDataElement customStorageStrings = batchClass.FindChildElementByName(BATCH_CLASS_CUSTOM_STORAGE_STRINGS);

            IACDataElement batchElement = rootElement.FindChildElementByName(BATCH);

            IACDataElementCollection currentDocuments = GetElementsByName(batchElement, DOCUMENTS, DOCUMENT);

            for (int i = 0; i < currentDocuments.Count; i++)
            {
                int currentDocumentIndex = i + 1;
                IACDataElement currentDocument = currentDocuments[currentDocumentIndex];

                IACDataElementCollection batchFields = GetElementsByName(batchElement, BATCH_FIELDS, BATCH_FIELD);
                IACDataElementCollection indexFields = GetElementsByName(currentDocument, INDEX_FIELDS, INDEX_FIELD);

                // access BatchVariables (keys can be found in the CaptureSV directory at AcBatch.htm or AcDocs.htm)
                //string batchVariableValue = batchElement["fieldKey"];
                //string documentVariableValue = currentDocument["fieldKey"];

                // access settings
                // IACDataElement customStorageString = customStorageStrings.FindChildElementByAttribute(BATCH_CLASS_CUSTOM_STORAGE_STRING, "Name", "-- myKey --");
                // string customStorageStringValue = customStorageString["Value"];

                string documentFilePath = currentDocument[PDF_GENERATION_FILE_NAME];

                // ...
            }

            batch.BatchClose(KfxDbState.KfxDbBatchReady, KfxDbQueue.KfxDbQueueNext, 0, "");
        }

        /// <summary>
        /// Searches for items by their name in a given IACDataElementCollection
        /// </summary>
        /// <param name="dataElement">The item containing the target items as children</param>
        /// <param name="rootName">Top level name</param>
        /// <param name="targetName">Target level name</param>
        /// <returns>The target items</returns>
        private IACDataElementCollection GetElementsByName(IACDataElement dataElement, string rootName, string targetName)
        {
            return dataElement.FindChildElementByName(rootName).FindChildElementsByName(targetName);
        }

        #endregion Methods
    }
}