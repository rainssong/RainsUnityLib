using com.rainssong.io;
using com.rainssong.utils;
using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace com.rainssong.ui
{
    [RequireComponent(typeof(Image))]
    public class LazyImg : MonoBehaviour
    {
        public ValueListener<string> idLis = new();
        public Image image;
        public string id
        {
            get { return idLis.Value; }
            set { idLis.Value = value; }
        }

        public Action<Sprite> OnChange;


        private void Awake()
        {
            image = GetComponent<Image>();
        }
        // Start is called before the first frame update
        void Start()
        {
            idLis.onValueChanged = IdHandler;
            AssetLoadManager.Instance.requestManager.onLoadComplete += this.CompleteHander;
            CompleteHander(id);

        }

        private void IdHandler(string arg1, string arg2)
        {
            CompleteHander(arg2);
        }

        async public Task FadeOut(float targetAlpha, float duration)
        {
            float speed = 1 / duration;
            while (image.color.a < targetAlpha)
            {
                Color c = image.color;
                c.a -= speed * Time.deltaTime;
                image.color = c;
                await Task.Yield();
            }
        }

        async public Task FadeIn(float targetAlpha, float duration)
        {
            float speed = 1 / duration;
            while (image.color.a > targetAlpha)
            {
                Color c = image.color;
                c.a += speed * Time.deltaTime;
                image.color = c;
                await Task.Yield();
            }
        }

        public async void CompleteHander(string uid)
        {
             if (!id.IsNullOrEmpty())
            {
                if (id==uid)
                {
                    var sp = AssetLoadManager.Instance.requestManager.GetSprite(id);

                    var tex = AssetLoadManager.Instance.requestManager.GetTexture2D(id);
                    if (sp == null)
                    {
                        await FadeOut(0, 0.2f);
                        return;
                    }


                    if (image.sprite == null || sp != image.sprite)
                    {
                        await FadeOut(0, 0.2f);
                        image.sprite = sp;
                        OnChange?.Invoke(sp);
                        await FadeIn(1, 0.2f);
                    }
                }


                //FIXME 这样判断不了
                //if (image.sprite == null || tex != image.sprite.texture)
                //    image.DOFade(0, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
                //    {
                //        // 更改Image的Sprite
                //        image.sprite = sp;
                //        OnChange?.Invoke(sp);
                //        //BUG:与自定义mat冲突
                //        // DOFade淡出Image
                //        image.DOFade(1, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
                //        {

                //        });
                //    });
            }

            else
            {
                image.sprite = null;
            }
        }
    }

}