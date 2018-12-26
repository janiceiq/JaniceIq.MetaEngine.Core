namespace JaniceIq.MetaEngine.Core.Storage
{
    using System.Collections.ObjectModel;
    using JaniceIq.MetaEngine.Core.Storage.Assets;

    public interface IStorageAssetManager
    {
        #region Properties

        ReadOnlyObservableCollection<IAsset> MetadataAssets { get; }

        #endregion
    }
}
