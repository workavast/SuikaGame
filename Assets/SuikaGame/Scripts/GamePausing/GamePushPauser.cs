using System;
using GamePush;

namespace SuikaGame.Scripts.GamePausing
{
    public class GamePushPauser : IGame, IDisposable
    {
        public GamePushPauser()
        {
            GP_Game.OnPause += GamePauser.Pause;
            GP_Game.OnResume += GamePauser.Continue;
        }

        public void Dispose()
        {
            GP_Game.OnPause -= GamePauser.Pause;
            GP_Game.OnResume -= GamePauser.Continue;
        }
    }
}