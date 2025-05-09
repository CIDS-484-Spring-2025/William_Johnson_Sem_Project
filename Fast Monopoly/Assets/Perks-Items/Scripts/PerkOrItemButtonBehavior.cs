using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkOrItemButtonBehavior : ButtonBehavior
{
    private Dictionary<Image, Color> originalColors = new Dictionary<Image, Color>();
    public int removalIndex;
    public bool clickable = true;

    protected override void Start()
    {
        base.Start();
        CacheOriginalColors(this.image);
    }

    protected override void Update()
    {
        if(!clickable){

        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(rectangle, Input.mousePosition))
        {
            ResetImageAndChildrenToOriginalColor(this.image);

            if (Input.GetButtonDown("Fire1"))
            {
                References.canvas.hasJustClickedButton = true;
                eventToTrigger.Invoke();
                playButtonSound();
            }
        }
        else
        {
            SetImageAndChildrenBrightness(this.image, 0.5f);
        }
    }

    private void CacheOriginalColors(Image rootImage)
    {
        originalColors[rootImage] = rootImage.color;

        Image[] childImages = rootImage.GetComponentsInChildren<Image>(includeInactive: true);
        foreach (Image img in childImages)
        {
            if (!originalColors.ContainsKey(img))
            {
                originalColors[img] = img.color;
            }
        }
    }

    public void SetImageAndChildrenBrightness(Image rootImage, float brightnessFactor)
    {
        AdjustImageBrightness(rootImage, brightnessFactor);

        Image[] childImages = rootImage.GetComponentsInChildren<Image>(includeInactive: true);
        foreach (Image img in childImages)
        {
            if (img != rootImage)
            {
                AdjustImageBrightness(img, brightnessFactor);
            }
        }
    }

    private void AdjustImageBrightness(Image img, float factor)
    {
        if (!originalColors.ContainsKey(img)) return;

        Color original = originalColors[img];
        Color adjusted = new Color(
            Mathf.Clamp01(original.r * factor),
            Mathf.Clamp01(original.g * factor),
            Mathf.Clamp01(original.b * factor),
            original.a
        );
        img.color = adjusted;
    }

    public void ResetImageAndChildrenToOriginalColor(Image rootImage)
    {
        ResetImageColor(rootImage);

        Image[] childImages = rootImage.GetComponentsInChildren<Image>(includeInactive: true);
        foreach (Image img in childImages)
        {
            if (img != rootImage)
            {
                ResetImageColor(img);
            }
        }
    }

    private void ResetImageColor(Image img)
    {
        if (originalColors.ContainsKey(img))
        {
            img.color = originalColors[img];
        }
    }
}
