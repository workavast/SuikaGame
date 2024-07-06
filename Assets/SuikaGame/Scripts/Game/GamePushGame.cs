using System;
using GamePush;

namespace SuikaGame.Scripts.Game
{
    public class GamePushGame : IGame, IDisposable
    {
        public GamePushGame()
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