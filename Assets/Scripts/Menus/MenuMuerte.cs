using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMuerte : MonoBehaviour
{
    public void RetryButton()
    {
        if (DeathManager.Instance != null)
            DeathManager.Instance.ResetDeaths();

        SceneManager.LoadScene("Unkonw_World"); // Asegúrate que el nombre esté bien escrito
    }

    public void MainMenuButton()
    {
        if (DeathManager.Instance != null)
            DeathManager.Instance.ResetDeaths();

        SceneManager.LoadScene("MenuInicial");
    }
}
