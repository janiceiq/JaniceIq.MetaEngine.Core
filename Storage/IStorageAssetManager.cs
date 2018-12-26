namespace JaniceIq.MetaEngine.Core.Storage
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using JaniceIq.MetaEngine.Core.Storage.Assets;

    public interface IStorageAssetManager
    {
        #region Properties

        ReadOnlyObservableCollection<IAsset> MetadataAssets { get; }

        #endregion

        #region Public Methods

        IAsset CreateMetadataAsset();

        IAsset CreateMetadataAsset(Dictionary<string, object> properties);

        void SetMetadataAssetProperty(IAsset asset, string propertyKey, object propertyValue);

        #endregion
    }
}
