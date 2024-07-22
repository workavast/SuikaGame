using SuikaGame.Scripts.Entities.Factory;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.DevTools
{
    public class DevSpawner : MonoBehaviour
    {
        [Inject] private IEntityFactory _entityFactory;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0)) 
                Spawn(0);
            if (Input.GetKeyDown(KeyCode.Alpha1)) 
                Spawn(1);
            if (Input.GetKeyDown(KeyCode.Alpha2)) 
                Spawn(2);
            if (Input.GetKeyDown(KeyCode.Alpha3)) 
                Spawn(3);
            if (Input.GetKeyDown(KeyCode.Alpha4)) 
                Spawn(4);
            if (Input.GetKeyDown(KeyCode.Alpha5)) 
                Spawn(5);
            if (Input.GetKeyDown(KeyCode.Alpha6)) 
                Spawn(6);
            if (Input.GetKeyDown(KeyCode.Alpha7)) 
                Spawn(7);
            if (Input.GetKeyDown(KeyCode.Alpha8)) 
                Spawn(8);
            if (Input.GetKeyDown(KeyCode.Alpha9)) 
                Spawn(9);
            if (Input.GetKeyDown(KeyCode.A)) 
                Spawn(10);
        }
        
        private void Spawn(int sizeIndex)
        {
            var entity = _entityFactory.Create(sizeIndex, transform.position);
            entity.Activate();
        }
    }
}