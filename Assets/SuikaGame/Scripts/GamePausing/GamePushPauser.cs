using System;
using GamePush;
using SuikaGame.Scripts.Ad;

namespace SuikaGame.Scripts.GamePausing
{
    public class GamePushPauser : IGamePauser, IDisposable
    {
        public GamePushPauser()
        {
            GP_Game.OnPause += GlobalGamePause.Pause;
            GP_Game.OnResume += GlobalGamePause.Continue;
            
            GP_Game.OnPause += LocalGamePause.Pause;
            GP_Game.OnResume += LocalGamePause.Continue;
        }

        public void Dispose()
        {
            GP_Game.OnPause -= GlobalGamePause.Pause;
            GP_Game.OnResume -= GlobalGamePause.Continue;
            
            GP_Game.OnPause -= LocalGamePause.Pause;
            GP_Game.OnResume -= LocalGamePause.Continue;
        }
    }
}