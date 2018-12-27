namespace JaniceIq.MetaEngine.Core.Storage.Assets
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using JaniceIq.MetaEngine.Core.Annotations;

    public class Asset : IAsset, INotifyPropertyChanged
    {
        #region Fields

        private readonly Dictionary<string, object> mProperties;

        private readonly ObservableCollection<IAsset> mChildren;

        private readonly Mutex mChildrenMutex;

        private readonly Mutex mPropertiesMutex;

        #endregion

        #region Constructors

        public Asset(Guid guid)
        {
            Guid = guid;

            mProperties = new Dictionary<string, object>();
            Properties = new ReadOnlyDictionary<string, object>(mProperties);

            mChildren = new ObservableCollection<IAsset>();
            Children = new ReadOnlyObservableCollection<IAsset>(mChildren);

            mChildrenMutex = new Mutex();
            mPropertiesMutex = new Mutex();
        }

        #endregion

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        public Guid Guid { get; }

        public IReadOnlyDictionary<string, object> Properties { get; }

        public ReadOnlyObservableCollection<IAsset> Children { get; }

        #endregion

        #region Public Methods

        public void SetProperty(string propertyKey, object propertyValue)
        {
            lock (mPropertiesMutex)
            {
                mProperties[propertyKey] = propertyValue;
            }

            OnPropertyChanged(nameof(Properties));
        }

        public void ClearProperties()
        {
            lock (mPropertiesMutex)
            {
                mProperties.Clear();
            }
        }

        public void AddChild(IAsset assetToAdd)
        {
            lock (mChildrenMutex)
            {
                mChildren.Add(assetToAdd);
            }

            OnPropertyChanged(nameof(Children));
        }

        public void RemoveChild(IAsset assetToRemove)
        {
            lock (mChildrenMutex)
            {
                mChildren.Remove(assetToRemove);
            }

            OnPropertyChanged(nameof(Children));
        }

        #endregion

        #region Protected Methods

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
