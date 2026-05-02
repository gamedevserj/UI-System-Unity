namespace UISystem.Core.Elements
{
    /// <summary>
    /// Defines contract for interactable UI element in Unity.
    /// </summary>
    public partial interface IInteractableElement
    {
        /// <summary>
        /// Selects the element.
        /// </summary>
        void Select();

        /// <summary>
        /// Switches element's interactability.
        /// </summary>
        /// <param name="enable">Whether element should be interactable.</param>
        void SwitchInteractability(bool enable);
    }
}
