using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public string videoSceneName = "Cinematica"; // Cambia el nombre si tu escena de video se llama diferente

    public void Play()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(videoSceneName);
    }

    public void Quit()
    {
        Debug.Log("Quit...");
        Application.Quit();
    }
}
