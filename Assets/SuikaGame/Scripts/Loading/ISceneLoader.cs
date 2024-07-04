namespace SuikaGame.Scripts.Loading
{
    public interface ISceneLoader
    {
        public int LoadingSceneIndex { get; }
        public int BootstrapSceneIndex { get; }
        
        public void Initialize(bool endLoadingInstantly);
        public void LoadScene(int index);
    }
}