﻿using System.Threading.Tasks;
using UISystem.Core.Views;
using UnityEngine;

namespace UISystem.Views
{
    internal class ViewCreator<TView> : ViewCreator<ViewBase, TView, Transform> where TView : ViewBase
    {

        public override bool IsViewValid => _view != null;

        public ViewCreator(ViewBase prefab, Transform parent) : base(prefab, parent)
        { }

        public override void DestroyView() => _view.DestroyView();

        public override TView CreateView()
        {
            //PackedScene loadedPrefab = ResourceLoader.Load<PackedScene>(_prefab);
            //_view = loadedPrefab.Instantiate() as TView;
            //_view.Init();
            //_parent.AddChild(_view);
            //return _view;
            _view = GameObject.Instantiate(_prefab, _parent) as TView;
            _view.Init();
            return _view;
        }

    }
}