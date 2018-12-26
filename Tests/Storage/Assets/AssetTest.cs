namespace JaniceIq.MetaEngine.Core.Tests.Storage.Assets
{
    using System;
    using System.ComponentModel;
    using JaniceIq.MetaEngine.Core.Storage.Assets;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Provides tests for the <see cref="Asset"/> class.
    /// </summary>
    [TestClass]
    public class AssetTest
    {
        #region Constants

        /// <summary>
        /// The sample property key.
        /// </summary>
        private const string SAMPLE_PROPERTY_KEY = "PropertyKey";

        /// <summary>
        /// The sample property value.
        /// </summary>
        private const string SAMPLE_PROPERTY_VALUE = "PropertyValue";

        #endregion

        #region Fields

        /// <summary>
        /// The sample unique identifier.
        /// </summary>
        private readonly Guid mSampleGuid = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e");

        #endregion

        #region Public Methods

        /// <summary>
        /// Whens an asset property is set, then a property changed notification is sent.
        /// </summary>
        [TestMethod]
        public void WhenAssetPropertyIsSet_ThenPropertyChangedNotificationIsSent()
        {
            // Arrange.
            IAsset asset = new Asset(mSampleGuid);
            bool wasCalled = false;
            PropertyChangedEventHandler propertyChangedEventHandler = (o, e) => { wasCalled = e.PropertyName == nameof(asset.Properties); };
            ((INotifyPropertyChanged)asset).PropertyChanged += propertyChangedEventHandler;

            // Act.
            asset.SetProperty(SAMPLE_PROPERTY_KEY, SAMPLE_PROPERTY_VALUE);

            // Assert.
            Assert.IsTrue(wasCalled);

            // Cleanup.
            ((INotifyPropertyChanged)asset).PropertyChanged -= propertyChangedEventHandler;
        }

        #endregion
    }
}
