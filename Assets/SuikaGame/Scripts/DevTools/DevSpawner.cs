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
                _entityFactory.Create(0, transform.position);
            if (Input.GetKeyDown(KeyCode.Alpha1)) 
                _entityFactory.Create(1, transform.position);
            if (Input.GetKeyDown(KeyCode.Alpha2)) 
                _entityFactory.Create(2, transform.position);
            if (Input.GetKeyDown(KeyCode.Alpha3)) 
                _entityFactory.Create(3, transform.position);
            if (Input.GetKeyDown(KeyCode.Alpha4)) 
                _entityFactory.Create(4, transform.position);
            if (Input.GetKeyDown(KeyCode.Alpha5)) 
                _entityFactory.Create(5, transform.position);
            if (Input.GetKeyDown(KeyCode.Alpha6)) 
                _entityFactory.Create(6, transform.position);
            if (Input.GetKeyDown(KeyCode.Alpha7)) 
                _entityFactory.Create(7, transform.position);
            if (Input.GetKeyDown(KeyCode.Alpha8)) 
                _entityFactory.Create(8, transform.position);
            if (Input.GetKeyDown(KeyCode.Alpha9)) 
                _entityFactory.Create(9, transform.position);
            if (Input.GetKeyDown(KeyCode.A)) 
                _entityFactory.Create(10, transform.position);
            
        }
    }
}