namespace JaniceIq.MetaEngine.Core.Storage.Metadata.Events
{
    using System;
    using System.ComponentModel;
    using JaniceIq.MetaEngine.Core.Storage.Assets;

    [ImmutableObject(true)]
    public class AssetAddedEventArgs : EventArgs
    {
        #region Constructors

        public AssetAddedEventArgs(IAsset addedAsset)
        {
            if (addedAsset == null)
            {
                throw new ArgumentNullException(nameof(addedAsset), "Added asset may not be null.");
            }

            AddedAsset = addedAsset;
            ParentAsset = null;
        }

        public AssetAddedEventArgs(IAsset addedAsset, IAsset parentAsset)
        {
            if (addedAsset == null)
            {
                throw new ArgumentNullException(nameof(addedAsset), "Added asset may not be null.");
            }

            AddedAsset = addedAsset;
            ParentAsset = parentAsset;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the added asset.
        /// </summary>
        /// <value>
        /// The added asset.
        /// </value>
        public IAsset AddedAsset { get; }

        /// <summary>
        /// Gets the parent asset. This is <c>null</c> should the added asset be a top level asset.
        /// </summary>
        /// <value>
        /// The parent asset.
        /// </value>
        public IAsset ParentAsset { get; }

        #endregion
    }
}
