using UnityEngine;
using UnityEngine.UI;

public class HeartOverlay : MonoBehaviour
{
    public Image blackHeartImage; // Imagen del corazón negro (overlay)
    private int maxDeaths = 10;

    void Start()
    {
        UpdateHeartOpacity(0);
    }

    public void UpdateHeartOpacity(int deathCount)
    {
        if (blackHeartImage == null)
            return;

        // Opacidad proporcional: 0 muertes = 0 alpha, 10 muertes = 1 alpha
        float alpha = Mathf.Clamp01(deathCount / (float)maxDeaths);

        Color c = blackHeartImage.color;
        c.a = alpha;
        blackHeartImage.color = c;
    }
}
