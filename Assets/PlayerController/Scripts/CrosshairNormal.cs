using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EvolveGames
{
    public class CrosshairNormal : MonoBehaviour
    {
        [Header("Crosshair - Normal")]
        [SerializeField] Image Active;
        [SerializeField] Image Base;
        [SerializeField] float MaxActiveSize = 90;
        [SerializeField] float MinActiveSize = 20;
        [SerializeField] float Smooth = 10;

        public bool State;
        private void Update()
        {
            if (State)
            {
                Color OpacityColor = Base.color;
                OpacityColor.a = Mathf.Lerp(OpacityColor.a, 0, Smooth * 2 * Time.deltaTime);
                Base.color = OpacityColor;
                Color OpacityColor1 = Active.color;
                OpacityColor1.a = Mathf.Lerp(OpacityColor1.a, 1, Smooth * 2 * Time.deltaTime);
                Active.color = OpacityColor1;
                float BackValue = Mathf.Lerp(Active.rectTransform.rect.width, MaxActiveSize, Smooth * Time.deltaTime);
                Active.rectTransform.sizeDelta = new Vector2(BackValue, BackValue);
            }
            else
            {
                Color OpacityColor1 = Active.color;
                OpacityColor1.a = Mathf.Lerp(OpacityColor1.a, 0, Smooth / 2 * Time.deltaTime);
                Active.color = OpacityColor1;
                float BackValue = Mathf.Lerp(Active.rectTransform.rect.width, MinActiveSize, Smooth * Time.deltaTime);
                Active.rectTransform.sizeDelta = new Vector2(BackValue, BackValue);
                if (Active.rectTransform.rect.width < MinActiveSize * 1.5)
                {
                    Color OpacityColor = Base.color;
                    OpacityColor.a = Mathf.Lerp(OpacityColor.a, 1, Smooth * Time.deltaTime);
                    Base.color = OpacityColor;
                }
            }
        }
        public void StateActive()
        {
            State = true;
        }
        public void StateBase()
        {
            State = false;
        }
    }


}