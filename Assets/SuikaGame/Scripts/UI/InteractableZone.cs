using SuikaGame.Scripts.Entities;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SuikaGame.UI
{
    public class InteractableZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, 
        IPointerUpHandler, IPointerDownHandler, IDragHandler
    {
        [SerializeField] private Spawner spawner;

        private bool _pointerIsIn;
        
        public void OnPointerEnter(PointerEventData eventData) 
            => _pointerIsIn = true;

        public void OnPointerExit(PointerEventData eventData) 
            => _pointerIsIn = false;

        public void OnDrag(PointerEventData eventData)
        {
            var spawnPoint = eventData.position;
            var gamePoint = Camera.main.ScreenToWorldPoint(spawnPoint);
            spawner.Move(gamePoint);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            var spawnPoint = eventData.position;
            var gamePoint = Camera.main.ScreenToWorldPoint(spawnPoint);
            spawner.Move(gamePoint);
        }
        
        public void OnPointerUp(PointerEventData eventData) 
            => TrySpawnNewEntity();

        private void TrySpawnNewEntity()
        {
            if(!_pointerIsIn)
                return;
            
            spawner.Spawn();
        }
    }
}