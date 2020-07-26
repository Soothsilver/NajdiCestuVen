using Nsnbc.Core;

namespace Nsnbc.Events
{
    public interface IQActivity
    {
        bool Blocking { get; }
        bool Dead { get; }
        void Update(AirSession airSession, float elapsedSeconds);
    }

    public interface IDrawableActivity : IQActivity
    {
        void Draw();
    }
}