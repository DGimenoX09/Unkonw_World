using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    public static DeathManager Instance;
    public int deathCount = 0;
    public string gameOverSceneName = "MenuMuerte";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            deathCount = 0; // Reiniciar contador al empezar
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddDeath()
    {
        deathCount++;
        Debug.Log("Muerte número: " + deathCount);

        if (deathCount >= 10)
        {
            deathCount = 0;
            SceneManager.LoadScene(gameOverSceneName);
        }
    }

    public void ResetDeaths()
    {
        deathCount = 0;
    }
}
