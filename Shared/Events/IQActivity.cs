namespace Nsnbc
{
    public interface IQActivity
    {
        bool Blocking { get; }
        bool Dead { get; }
        void Run(Session session, float elapsedSeconds);
    }

    public interface IDrawableActivity : IQActivity
    {
        void Draw();
    }
}