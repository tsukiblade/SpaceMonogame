namespace SpaceInvaders.Core
{
    public interface IGameManager
    {
        IGameManagerMemento Save();
        void Restore(IGameManagerMemento memento);
        void ShowScores();
    }
}