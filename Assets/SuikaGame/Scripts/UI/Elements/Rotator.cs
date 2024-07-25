using System;
using UnityEngine;

namespace SuikaGame.Scripts.UI.Elements
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private RotatePair[] rotatePairs;
        
        private void Update()
        {
            var deltaTime = Time.deltaTime;
            foreach (var rotatePair in rotatePairs)
                rotatePair.ManualUpdate(deltaTime);
        }

        [Serializable]
        private class RotatePair
        {
            [SerializeField] private Transform transform;
            [SerializeField] private float rotationSpeed;

            public void ManualUpdate(float deltaTime)
            {
                transform.Rotate(Vector3.back * (rotationSpeed * deltaTime));
            }
        }
    }
}