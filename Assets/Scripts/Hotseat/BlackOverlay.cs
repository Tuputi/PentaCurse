using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BlackOverlay : MonoBehaviour
{
    private Image Image;
    private Color TargetColor;
    void Start()
    {
        Image = GetComponent<Image>();
    }

    void Update()
    {
        Image.color = Color.Lerp(Image.color, TargetColor, 0.1f);
    }

    public void FadeIn()
    {
        TargetColor = new Color(0, 0, 0, 0);
    }

    public void FadeOut()
    {
        TargetColor = new Color(0, 0, 0, 0.75f);
    }

}
