namespace SuikaGame.Scripts.GameOver.GameOverControlling
{
    public interface IGameOverController : IGameOverProvider, IGameOverInvoker
    {
        public void Reset();
    }
}