namespace UISystem.Common.Elements
{
    /// <summary>
    /// Defines contract for interactable element.
    /// </summary>
    public interface IInteractableElement
    {
        /// <summary>
        /// Switches element's interactability.
        /// </summary>
        /// <param name="enable">Whether element should be interactable.</param>
        void SwitchInteractability(bool enable);
    }
}
