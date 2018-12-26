namespace JaniceIq.MetaEngine.Core.Storage.Metadata.Events
{
    using System;

    public class EntityDeletedEventArgs : EventArgs
    {
        #region Constructors

        public EntityDeletedEventArgs(Guid deletedEntityGuid)
        {
            DeletedEntityGuid = deletedEntityGuid;
            ParentEntityGuid = null;
        }

        public EntityDeletedEventArgs(Guid deletedEntityGuid, Guid parentEntityGuid)
        {
            DeletedEntityGuid = deletedEntityGuid;
            ParentEntityGuid = parentEntityGuid;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the deleted entity unique identifier.
        /// </summary>
        /// <value>
        /// The deleted entity unique identifier.
        /// </value>
        public Guid DeletedEntityGuid { get; }

        /// <summary>
        /// Gets the parent entity unique identifier. This is <c>null</c> should the deleted entity be a top level entity.
        /// </summary>
        /// <value>
        /// The parent entity unique identifier.
        /// </value>
        public Guid? ParentEntityGuid { get; }

        #endregion
    }
}
