namespace JaniceIq.MetaEngine.Core.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using JaniceIq.MetaEngine.Core.Storage.Assets;
    using JaniceIq.MetaEngine.Core.Storage.Metadata;
    using JaniceIq.MetaEngine.Core.Storage.Metadata.Events;

    public class StorageAssetManager : IStorageAssetManager
    {
        #region Fields

        private readonly IMetadataAssetManager mMetadataAssetManager;

        private readonly ObservableCollection<IAsset> mMetadataAssets;

        private readonly Dictionary<IAsset, IAsset> mChildParentAssetMapping;

        #endregion

        #region Constructors

        public StorageAssetManager(IMetadataAssetManager metadataAssetManager)
        {
            if (metadataAssetManager == null)
            {
                throw new ArgumentNullException(nameof(metadataAssetManager), "Metadata asset manager may not be null.");
            }

            mMetadataAssets = new ObservableCollection<IAsset>();
            MetadataAssets = new ReadOnlyObservableCollection<IAsset>(mMetadataAssets);

            mChildParentAssetMapping = new Dictionary<IAsset, IAsset>();

            mMetadataAssetManager = metadataAssetManager;

            mMetadataAssetManager.AssetAdded += MetadataAssetManagerAssetAdded;
            mMetadataAssetManager.AssetDeleted += MetadataAssetManagerAssetDeleted;

            InitializeMetadataAssets();
        }

        #endregion

        #region Properties

        public ReadOnlyObservableCollection<IAsset> MetadataAssets { get; }

        #endregion

        #region Public Methods

        public IAsset CreateMetadataAsset()
        {
            return mMetadataAssetManager.CreateAsset();
        }

        public IAsset CreateMetadataAsset(Dictionary<string, object> properties)
        {
            IAsset newAsset = CreateMetadataAsset();

            foreach (KeyValuePair<string, object> property in properties)
            {
                SetMetadataAssetProperty(newAsset, property.Key, property.Value);
            }

            return newAsset;
        }

        public void SetMetadataAssetProperty(IAsset asset, string propertyKey, object propertyValue)
        {
            mMetadataAssetManager.SetAssetProperty(asset, propertyKey, propertyValue);
        }

        #endregion

        #region Private Methods

        private void DeleteAssetRecursively(IAsset assetToDelete)
        {
            foreach (IAsset childAsset in assetToDelete.Children)
            {
                DeleteAssetRecursively(childAsset);
                assetToDelete.RemoveChild(childAsset);
            }

            mChildParentAssetMapping.Remove(assetToDelete);
        }

        private void MetadataAssetManagerAssetAdded(object sender, AssetAddedEventArgs e)
        {
            if (e.ParentAsset == null)
            {
                // This is a top level asset.
                mMetadataAssets.Add(e.AddedAsset);
            }
            else
            {
                mChildParentAssetMapping[e.ParentAsset].AddChild(e.AddedAsset);
                mChildParentAssetMapping[e.AddedAsset] = e.ParentAsset;
            }
        }

        private void MetadataAssetManagerAssetDeleted(object sender, AssetDeletedEventArgs e)
        {
            DeleteAssetRecursively(e.DeletedAsset);

            if (e.ParentAsset == null)
            {
                // This is a top level asset.
                mMetadataAssets.Remove(e.DeletedAsset);
            }
            else
            {
                e.ParentAsset.RemoveChild(e.DeletedAsset);
            }
        }

        private void InitializeMetadataAssets()
        {
            ICollection<IAsset> rootAssets = mMetadataAssetManager.GetRootAssets();

            foreach (IAsset rootAsset in rootAssets)
            {
                mMetadataAssets.Add(rootAsset);
                InitializeChildAssets(rootAsset);
            }
        }

        private void InitializeChildAssets(IAsset parentAsset)
        {
            ICollection<IAsset> childAssets = mMetadataAssetManager.GetAssets(parentAsset);

            foreach (IAsset childAsset in childAssets)
            {
                mChildParentAssetMapping[childAsset] = parentAsset;
                parentAsset.AddChild(childAsset);

                InitializeChildAssets(childAsset);
            }
        }

        #endregion
    }
}
