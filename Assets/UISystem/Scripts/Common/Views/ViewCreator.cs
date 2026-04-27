using UISystem.Core.Views;
using UnityEngine;

namespace UISystem.Views
{
    /// <summary>
    /// View creator.
    /// </summary>
    /// <typeparam name="TView">The type of view to create. Must derive from <see cref="ViewBase"/>.</typeparam>
    internal class ViewCreator<TView> : ViewCreator<ViewBase, TView, Transform>
        where TView : ViewBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewCreator{TView}"/> class.
        /// </summary>
        /// <param name="prefab">View prefab.</param>
        /// <param name="parent">View parent.</param>
        public ViewCreator(ViewBase prefab, Transform parent)
            : base(prefab, parent)
        {
        }

        /// <summary>
        /// Gets a value indicating whether the view is valid.
        /// </summary>
        public override bool IsViewValid => View != null;

        /// <inheritdoc/>
        public override void DestroyView() => View.DestroyView();

        /// <inheritdoc/>
        public override TView CreateView()
        {
            View = GameObject.Instantiate(Prefab, Parent) as TView;
            View.Init();
            return View;
        }
    }
}
