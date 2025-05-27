using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    public Image heartImage; // La imagen del corazón en UI
    public Sprite[] heartStates; // Array con sprites desde lleno a vacío (index 0 = lleno)

    void Start()
    {
        UpdateHeart();
    }

    void OnEnable()
    {
        // Si quieres actualizar cada vez que mueren:
        // Podrías subscribirte a un evento o actualizar en Update o desde DeathManager
    }

    public void UpdateHeart()
    {
        int deaths = DeathManager.Instance != null ? DeathManager.Instance.deathCount : 0;

        // Asumiendo que heartStates.Length = 11 (de 0 a 10 muertes)
        // Queremos mostrar sprite que corresponde al número de muertes actual

        int index = Mathf.Clamp(deaths, 0, heartStates.Length - 1);

        if (heartImage != null && heartStates.Length > 0)
        {
            // Cambia el sprite según las muertes
            heartImage.sprite = heartStates[index];
        }
    }
}
