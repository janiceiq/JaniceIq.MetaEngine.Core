namespace JaniceIq.MetaEngine.Core.Storage.Metadata.Events
{
    using System;

    public class EntityPropertyUpdatedEventArgs : EventArgs
    {
        #region Constructors

        public EntityPropertyUpdatedEventArgs(Guid updatedEntityGuid, string updatedPropertyKey)
        {
            if (string.IsNullOrEmpty(updatedPropertyKey))
            {
                throw new ArgumentNullException(nameof(updatedPropertyKey), "Updated property key may not be null or empty.");
            }

            UpdatedEntityGuid = updatedEntityGuid;
            UpdatedPropertyKey = updatedPropertyKey;
            ParentEntityGuid = null;
        }

        public EntityPropertyUpdatedEventArgs(Guid updatedEntityGuid, string updatedPropertyKey, Guid parentEntityGuid)
        {
            if (string.IsNullOrEmpty(updatedPropertyKey))
            {
                throw new ArgumentNullException(nameof(updatedPropertyKey), "Updated property key may not be null or empty.");
            }

            UpdatedEntityGuid = updatedEntityGuid;
            UpdatedPropertyKey = updatedPropertyKey;
            ParentEntityGuid = parentEntityGuid;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the updated entity unique identifier.
        /// </summary>
        /// <value>
        /// The updated entity unique identifier.
        /// </value>
        public Guid UpdatedEntityGuid { get; }

        /// <summary>
        /// Gets the updated property key.
        /// </summary>
        /// <value>
        /// The updated property key.
        /// </value>
        public string UpdatedPropertyKey { get; }

        /// <summary>
        /// Gets the parent entity unique identifier. This is <c>null</c> should the updated entity be a top level entity.
        /// </summary>
        /// <value>
        /// The parent entity unique identifier.
        /// </value>
        public Guid? ParentEntityGuid { get; }

        #endregion
    }
}
