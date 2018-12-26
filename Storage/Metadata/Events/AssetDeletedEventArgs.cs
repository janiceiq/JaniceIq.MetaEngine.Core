namespace JaniceIq.MetaEngine.Core.Storage.Metadata.Events
{
    using System;
    using System.ComponentModel;
    using JaniceIq.MetaEngine.Core.Storage.Assets;

    [ImmutableObject(true)]
    public class AssetDeletedEventArgs : EventArgs
    {
        #region Constructors

        public AssetDeletedEventArgs(IAsset deletedAsset)
        {
            if (deletedAsset == null)
            {
                throw new ArgumentNullException(nameof(deletedAsset), "Deleted asset may not be null.");
            }

            DeletedAsset = deletedAsset;
            ParentAsset = null;
        }

        public AssetDeletedEventArgs(IAsset deletedAsset, IAsset parentAsset)
        {
            if (deletedAsset == null)
            {
                throw new ArgumentNullException(nameof(deletedAsset), "Deleted asset may not be null.");
            }

            DeletedAsset = deletedAsset;
            ParentAsset = parentAsset;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the deleted asset.
        /// </summary>
        /// <value>
        /// The deleted asset.
        /// </value>
        public IAsset DeletedAsset { get; }

        /// <summary>
        /// Gets the parent asset. This is <c>null</c> should the deleted asset be a top level asset.
        /// </summary>
        /// <value>
        /// The parent asset.
        /// </value>
        public IAsset ParentAsset { get; }

        #endregion
    }
}
