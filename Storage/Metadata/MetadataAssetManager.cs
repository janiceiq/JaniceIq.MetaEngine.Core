namespace JaniceIq.MetaEngine.Core.Storage.Metadata
{
    using System;
    using System.Collections.Generic;
    using JaniceIq.MetaEngine.Core.Storage.Assets;
    using JaniceIq.MetaEngine.Core.Storage.Metadata.Events;

    public class MetadataAssetManager : IMetadataAssetManager
    {
        #region Fields

        private readonly IMetadataStorage mMetadataStorage;

        private readonly IDictionary<Guid, IAsset> mGuidAssetMap;

        #endregion

        #region Constructors

        public MetadataAssetManager(IMetadataStorage metadataStorage)
        {
            if (metadataStorage == null)
            {
                throw new ArgumentNullException(nameof(metadataStorage), "Metadata storage may not be null.");
            }

            mGuidAssetMap = new Dictionary<Guid, IAsset>();

            mMetadataStorage = metadataStorage;

            mMetadataStorage.EntityAdded += MetadataStorageEntityAddedHandler;
            mMetadataStorage.EntityDeleted += MetadataStorageEntityDeletedHandler;
            mMetadataStorage.EntityPropertyUpdated += MetadataStorageEntityPropertyUpdatedHandler;
        }

        #endregion

        #region Public Events

        public event EventHandler<AssetAddedEventArgs> AssetAdded;

        public event EventHandler<AssetDeletedEventArgs> AssetDeleted;

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the root assets.
        /// </summary>
        /// <returns>A collection of top level assets.</returns>
        public ICollection<IAsset> GetRootAssets()
        {
            ICollection<IAsset> rootAssets = new List<IAsset>();
            ICollection<Guid> rootGuids = mMetadataStorage.GetRootEntities();

            foreach (Guid rootGuid in rootGuids)
            {
                rootAssets.Add(GetAssetForGuid(rootGuid));
            }

            return rootAssets;
        }

        /// <summary>
        /// Gets the assets whose parent is identified by <see cref="parentAsset"/>.
        /// </summary>
        /// <param name="parentAsset">The parent asset.</param>
        /// <returns>A collection of child assets.</returns>
        public ICollection<IAsset> GetAssets(IAsset parentAsset)
        {
            if (parentAsset == null)
            {
                throw new ArgumentNullException(nameof(parentAsset), "Parent asset may not be null.");
            }

            ICollection<IAsset> assets = new List<IAsset>();
            ICollection<Guid> guids = mMetadataStorage.GetEntities(parentAsset.Guid);

            foreach (Guid guid in guids)
            {
                assets.Add(GetAssetForGuid(guid));
            }

            return assets;
        }

        #endregion

        #region Private Methods

        private IAsset GetAssetForGuid(Guid guid)
        {
            if (!mGuidAssetMap.ContainsKey(guid))
            {
                mGuidAssetMap[guid] = new Asset(guid);

                ICollection<string> propertyKeys = mMetadataStorage.GetEntityPropertyKeys(guid);

                foreach (string propertyKey in propertyKeys)
                {
                    mGuidAssetMap[guid].SetProperty(propertyKey, mMetadataStorage.GetEntityProperty(guid, propertyKey));
                }
            }

            return mGuidAssetMap[guid];
        }

        private void MetadataStorageEntityAddedHandler(object sender, EntityAddedEventArgs e)
        {
            EventHandler<AssetAddedEventArgs> handler = AssetAdded;

            if (handler != null)
            {
                if (e.ParentEntityGuid.HasValue)
                {
                    handler(this, new AssetAddedEventArgs(GetAssetForGuid(e.AddedEntityGuid), GetAssetForGuid(e.ParentEntityGuid.Value)));
                }
                else
                {
                    handler(this, new AssetAddedEventArgs(GetAssetForGuid(e.AddedEntityGuid)));
                }
            }
        }

        private void MetadataStorageEntityDeletedHandler(object sender, EntityDeletedEventArgs e)
        {
            EventHandler<AssetDeletedEventArgs> handler = AssetDeleted;

            if (handler != null)
            {
                IAsset deletedAsset = GetAssetForGuid(e.DeletedEntityGuid);
                mGuidAssetMap.Remove(e.DeletedEntityGuid);

                if (e.ParentEntityGuid.HasValue)
                {
                    handler(this, new AssetDeletedEventArgs(deletedAsset, GetAssetForGuid(e.ParentEntityGuid.Value)));
                }
                else
                {
                    handler(this, new AssetDeletedEventArgs(deletedAsset));
                }
            }
        }

        private void MetadataStorageEntityPropertyUpdatedHandler(object sender, EntityPropertyUpdatedEventArgs e)
        {
            GetAssetForGuid(e.UpdatedEntityGuid).SetProperty(e.UpdatedPropertyKey, mMetadataStorage.GetEntityProperty(e.UpdatedEntityGuid, e.UpdatedPropertyKey));
        }

        #endregion
    }
}
