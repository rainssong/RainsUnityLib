using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace com.rainssong.ui
{
    public class Transition : MonoBehaviour
    {
        //private float fadeSpeed=0.1f;
        //private Color fadeColor=Color.black;

        public float fadeDuration = 1.0f;
        public CanvasGroup canvasGroup;


        void Start()
        {
            // 获取CanvasGroup组件，如果没有则添加一个
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    canvasGroup = gameObject.AddComponent<CanvasGroup>();
                }
            }

            // 开始FadeIn效果
            //FadeIn().ConfigureAwait(false);
        }

        public void FadeInFun()
        {
            FadeIn().ConfigureAwait(false);
        }

        // Start is called before the first frame update
        async public Task FadeIn()
        {
            float elapsedTime = 0f;
            canvasGroup.blocksRaycasts = true;
            while (elapsedTime < fadeDuration)
            {
                canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
                elapsedTime += Time.deltaTime;
                await Task.Yield();
            }
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 1f;
        }

        public async Task FadeOut()
        {
            float elapsedTime = 0f;
            canvasGroup.blocksRaycasts = true;
            while (elapsedTime < fadeDuration)
            {
                canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
                elapsedTime += Time.deltaTime;
                await Task.Yield();
            }
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0f;
            return;
        }
    }
}