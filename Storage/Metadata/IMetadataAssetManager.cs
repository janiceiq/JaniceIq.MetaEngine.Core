namespace JaniceIq.MetaEngine.Core.Storage.Metadata
{
    using System;
    using System.Collections.Generic;
    using JaniceIq.MetaEngine.Core.Storage.Assets;
    using JaniceIq.MetaEngine.Core.Storage.Metadata.Events;

    public interface IMetadataAssetManager
    {
        #region Public Events

        event EventHandler<AssetAddedEventArgs> AssetAdded;

        event EventHandler<AssetDeletedEventArgs> AssetDeleted;

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the root assets.
        /// </summary>
        /// <returns>A collection of top level assets.</returns>
        ICollection<IAsset> GetRootAssets();

        /// <summary>
        /// Gets the assets whose parent is identified by <see cref="parentAsset"/>.
        /// </summary>
        /// <param name="parentAsset">The parent asset.</param>
        /// <returns>A collection of child assets.</returns>
        ICollection<IAsset> GetAssets(IAsset parentAsset);

        #endregion
    }
}
