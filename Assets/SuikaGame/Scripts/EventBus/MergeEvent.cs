using Avastrad.EventBusFramework;

namespace SuikaGame.Scripts
{
    public struct MergeEvent : IEvent
    {
        public readonly int SizeIndex;

        public MergeEvent(int sizeIndex) 
            => SizeIndex = sizeIndex;
    }
}