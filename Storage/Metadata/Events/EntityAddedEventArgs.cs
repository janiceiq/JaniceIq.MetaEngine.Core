namespace JaniceIq.MetaEngine.Core.Storage.Metadata.Events
{
    using System;
    using System.ComponentModel;

    [ImmutableObject(true)]
    public class EntityAddedEventArgs : EventArgs
    {
        #region Constructors

        public EntityAddedEventArgs(Guid addedEntityGuid)
        {
            AddedEntityGuid = addedEntityGuid;
            ParentEntityGuid = null;
        }

        public EntityAddedEventArgs(Guid addedEntityGuid, Guid parentEntityGuid)
        {
            AddedEntityGuid = addedEntityGuid;
            ParentEntityGuid = parentEntityGuid;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the added entity unique identifier.
        /// </summary>
        /// <value>
        /// The added entity unique identifier.
        /// </value>
        public Guid AddedEntityGuid { get; }

        /// <summary>
        /// Gets the parent entity unique identifier. This is <c>null</c> should the added entity be a top level entity.
        /// </summary>
        /// <value>
        /// The parent entity unique identifier.
        /// </value>
        public Guid? ParentEntityGuid { get; }

        #endregion
    }
}
