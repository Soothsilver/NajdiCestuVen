using Nsnbc.Core;

namespace Nsnbc.Events
{
    public interface IQActivity
    {
        bool Blocking { get; }
        bool Dead { get; }
        void Update(Session session, float elapsedSeconds);
    }

    public interface IDrawableActivity : IQActivity
    {
        void Draw();
    }
}