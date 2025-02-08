namespace UISystem.Common.Elements
{
    internal interface IInteractableElement
    {

        bool IsInteractable { get; }

        void SwitchInteractability(bool enable);

    }
}
