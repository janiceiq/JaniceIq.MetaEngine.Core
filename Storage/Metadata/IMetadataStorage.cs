namespace JaniceIq.MetaEngine.Core.Storage.Metadata
{
    using System;
    using System.Collections.Generic;
    using JaniceIq.MetaEngine.Core.Storage.Metadata.Events;

    public interface IMetadataStorage : IStorage
    {
        #region Public Events

        event EventHandler<EntityAddedEventArgs> EntityAdded;

        event EventHandler<EntityDeletedEventArgs> EntityDeleted;

        event EventHandler<EntityPropertyUpdatedEventArgs> EntityPropertyUpdated;

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the root entity unique identifiers.
        /// </summary>
        /// <returns>A collection of unique identifiers of top level entities.</returns>
        ICollection<Guid> GetRootEntities();

        /// <summary>
        /// Gets the unique identifiers stored in the meta data storage whose parent is identified by <see cref="parentGuid"/>.
        /// </summary>
        /// <param name="parentGuid">The parent unique identifier.</param>
        /// <returns>A collection of unique identifiers who are children of the given <see cref="parentGuid"/>.</returns>
        ICollection<Guid> GetEntities(Guid parentGuid);

        /// <summary>
        /// Creates a new entity and returns its unique identifier.
        /// </summary>
        /// <returns>The unique identifier of the newly created entity.</returns>
        Guid CreateEntity();

        /// <summary>
        /// Deletes the entity identified by <see cref="entityGuidToDelete"/>. Also recursively deletes all associated children.
        /// </summary>
        /// <param name="entityGuidToDelete">The unique identifier of the entity to delete.</param>
        void DeleteEntity(Guid entityGuidToDelete);
        
        void SetEntityProperty(Guid entityGuid, string propertyKey, object propertyValue);

        /// <summary>
        /// Gets the entity property.
        /// </summary>
        /// <param name="entityGuid">The entity unique identifier.</param>
        /// <param name="propertyKey">The property key.</param>
        /// <returns>The property value, represented as an <see cref="object"/> type.</returns>
        object GetEntityProperty(Guid entityGuid, string propertyKey);

        /// <summary>
        /// Gets the entity property keys.
        /// </summary>
        /// <param name="entityGuid">The entity unique identifier.</param>
        /// <returns>A collection of property keys valid for this entity.</returns>
        ICollection<string> GetEntityPropertyKeys(Guid entityGuid);

        #endregion
    }
}
