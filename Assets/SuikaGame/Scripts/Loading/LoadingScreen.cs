using System;
using System.Collections;
using UnityEngine;

namespace SuikaGame.Scripts.Loading
{
    public class LoadingScreen : MonoBehaviour, ILoadingScreen
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeTime = 1;
        
        public bool IsShow { get; private set; }
        
        public event Action FadeAnimationEnded;

        public void Initialize() 
            => IsShow = gameObject.activeSelf;

        public void StartLoading()
        {
            StopAllCoroutines();
            canvasGroup.alpha = 1;
            gameObject.SetActive(true);
            IsShow = true;
        }

        public void EndLoading()
        {
            StopAllCoroutines();
            StartCoroutine(Fade());
        }

        public void EndLoadingInstantly()
        {
            StopAllCoroutines();
            IsShow = false;
            gameObject.SetActive(false);
            FadeAnimationEnded?.Invoke();
        }

        private IEnumerator Fade()
        {
            float timer = 0;

            while (timer < fadeTime)
            {
                yield return new WaitForEndOfFrame();
                canvasGroup.alpha = fadeTime - timer;
                timer += Time.deltaTime;
            }
            
            IsShow = false;
            gameObject.SetActive(false);
            FadeAnimationEnded?.Invoke();
        }
    }
}