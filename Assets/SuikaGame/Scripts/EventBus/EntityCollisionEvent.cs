using Avastrad.EventBusFramework;
using SuikaGame.Scripts.Entities;

namespace SuikaGame.Scripts
{
    public struct EntityCollisionEvent : IEvent
    {
        public readonly Entity Parent;
        public readonly Entity Child;

        public EntityCollisionEvent(Entity parent, Entity child)
        {
            Parent = parent;
            Child = child;
        }
    }
}