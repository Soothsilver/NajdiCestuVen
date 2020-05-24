namespace Nsnbc
{
    public interface IQActivity
    {
        bool Blocking { get; }
        bool Dead { get; }
        void Run(TSession session, float elapsedSeconds);
    }

    public interface IDrawableActivity : IQActivity
    {
        void Draw();
    }
}