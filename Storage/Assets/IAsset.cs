using System.Collections.ObjectModel;

namespace JaniceIq.MetaEngine.Core.Storage.Assets
{
    using System;
    using System.Collections.Generic;

    public interface IAsset
    {
        #region Public Properties

        /// <summary>
        /// Gets the unique identifier of the asset.
        /// </summary>
        /// <value>
        /// The unique identifier of the asset.
        /// </value>
        Guid Guid { get; }

        /// <summary>
        /// Gets the properties of the asset.
        /// </summary>
        /// <value>
        /// The properties of the asset as read only dictionary. All individual properties are stored as <see cref="object"/> types.
        /// </value>
        IReadOnlyDictionary<string, object> Properties { get; }

        ReadOnlyObservableCollection<IAsset> Children { get; }

        #endregion

        #region Public Methods

        void SetProperty(string propertyKey, object propertyValue);

        void ClearProperties();

        void AddChild(IAsset assetToAdd);

        void RemoveChild(IAsset assetToRemove);

        #endregion
    }
}
