using SuikaGame.Scripts.Entities.Spawning;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts
{
    public class DropDirectionView : MonoBehaviour
    {
        private IEntitySpawner _entitySpawner;
        
        [Inject]
        public void Construct(IEntitySpawner entitySpawner)
        {
            _entitySpawner = entitySpawner;
            _entitySpawner.OnSpawnEntity += Show;
            _entitySpawner.OnDeSpawnEntity += Hide;
            _entitySpawner.OnMoveEntity += Move;
        }

        private void Awake() 
            => Hide();

        private void Show() 
            => gameObject.SetActive(true);

        private void Hide() 
            => gameObject.SetActive(false);

        private void Move(Vector2 newPosition) 
            => transform.position = newPosition;

        private void OnDestroy()
        {
            _entitySpawner.OnSpawnEntity -= Show;
            _entitySpawner.OnDeSpawnEntity -= Hide;
            _entitySpawner.OnMoveEntity -= Move;
        }
    }
}