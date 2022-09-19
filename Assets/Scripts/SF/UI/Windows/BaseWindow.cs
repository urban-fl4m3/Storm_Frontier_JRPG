﻿using System.Collections.Generic;
using SF.Common.Data;
using SF.Game;
using SF.Game.Worlds;
using SF.UI.Models.Actions;
using SF.UI.Presenters;
using UnityEngine;

namespace SF.UI.Windows
{
    public abstract class BaseWindow : MonoBehaviour, IWindow
    {
        public IReadonlyActionBinder Actions => _actions;
        
        protected IWorld World { get; private set; }
        protected IServiceLocator Services { get; private set; }
        
        protected readonly ActionBinder _actions = new();

        private IEnumerable<IBasePresenter> _presenters;
        
        public void Show()
        {
            foreach (var presenter in _presenters)
            {
                presenter.Enable();
            }
        }

        public void Hide()
        {
            foreach (var presenter in _presenters)
            {
                presenter.Disable();
            }
        }
        
        public void Close()
        {
            Destroy(gameObject);
        }

        public void SetFactoryMeta(IDataProvider dataProvider)
        {
            World = dataProvider.GetData<IWorld>();
            Services = dataProvider.GetData<IServiceLocator>();
            
            _presenters = ResolvePresenters();
        }
        
        protected abstract IEnumerable<IBasePresenter> ResolvePresenters();
    }
}