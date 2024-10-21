using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum alphaValue
{
    SHRINKING,
    GROWING,
}
public class FlashingText : MonoBehaviour
{
    public alphaValue currentAlphaValue;
    public float CommentminAlpha;
    public float CommentmaxAlpha;
    public float CommentCurrentAlpha;

    public float speedoftext; //0.004 je default good nastavenie

    public Text MyText;

    void Start()
    {
        CommentminAlpha = 0.2f;
        CommentmaxAlpha = 1.0f;
        CommentCurrentAlpha = 1.0f;
        currentAlphaValue = alphaValue.SHRINKING;
    }

    void Update()
    {
        alphaComments();
    }

    public void alphaComments()
    {
        if (currentAlphaValue == alphaValue.SHRINKING)
        {
            CommentCurrentAlpha = CommentCurrentAlpha - speedoftext;
            MyText.color = new Color(Color.white.r, Color.white.g, Color.white.b, CommentCurrentAlpha);
            if (CommentCurrentAlpha <= CommentminAlpha)
            {
                currentAlphaValue = alphaValue.GROWING;
            }
        }
        else if (currentAlphaValue == alphaValue.GROWING)
        {
            CommentCurrentAlpha = CommentCurrentAlpha + speedoftext;
            MyText.color = new Color(Color.white.r, Color.white.g, Color.white.b, CommentCurrentAlpha);
            if (CommentCurrentAlpha >= CommentmaxAlpha)
            {
                currentAlphaValue = alphaValue.SHRINKING;
            }
        }
    }
}
