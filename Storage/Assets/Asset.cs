using System.Runtime.CompilerServices;
using JaniceIq.MetaEngine.Core.Annotations;

namespace JaniceIq.MetaEngine.Core.Storage.Assets
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    public class Asset : IAsset, INotifyPropertyChanged
    {
        #region Fields

        private readonly Dictionary<string, object> mProperties;

        private readonly ObservableCollection<IAsset> mChildren;

        #endregion

        #region Constructors

        public Asset(Guid guid)
        {
            Guid = guid;

            mProperties = new Dictionary<string, object>();
            Properties = new ReadOnlyDictionary<string, object>(mProperties);

            mChildren = new ObservableCollection<IAsset>();
            Children = new ReadOnlyObservableCollection<IAsset>(mChildren);
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
            mProperties[propertyKey] = propertyValue;

            OnPropertyChanged(nameof(Properties));
        }

        public void ClearProperties()
        {
            mProperties.Clear();
        }

        public void AddChild(IAsset assetToAdd)
        {
            mChildren.Add(assetToAdd);
        }

        public void RemoveChild(IAsset assetToRemove)
        {
            mChildren.Remove(assetToRemove);
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
