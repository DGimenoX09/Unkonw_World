using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathManager : MonoBehaviour
{
    public static DeathManager Instance;
    public int deathCount = 0;
    public string gameOverSceneName = "MenuMuerte";

    public TextMeshProUGUI vidasTexto;
    public int maxDeaths = 10;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            deathCount = 0;
            SceneManager.sceneLoaded += OnSceneLoaded; // Se ejecuta cada vez que se carga una escena
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateVidasTexto();
    }

    public void AddDeath()
    {
        deathCount++;
        Debug.Log("Muerte número: " + deathCount);

        HeartOverlay heartOverlay = FindObjectOfType<HeartOverlay>();
        if (heartOverlay != null)
        {
            heartOverlay.UpdateHeartOpacity(deathCount);
        }

        UpdateVidasTexto();

        if (deathCount >= maxDeaths)
        {
            deathCount = 0;
            SceneManager.LoadScene(gameOverSceneName);
        }
    }

    public void ResetDeaths()
    {
        deathCount = 0;

        HeartOverlay heartOverlay = FindObjectOfType<HeartOverlay>();
        if (heartOverlay != null)
        {
            heartOverlay.UpdateHeartOpacity(0);
        }

        UpdateVidasTexto();
    }

    void UpdateVidasTexto()
    {
        if (vidasTexto != null)
        {
            int vidasRestantes = maxDeaths - deathCount;
            vidasTexto.text = vidasRestantes.ToString(); // Solo el número
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Buscar el texto automáticamente si no está asignado
        if (vidasTexto == null)
        {
            GameObject textoObj = GameObject.FindWithTag("VidasTexto");
            if (textoObj != null)
            {
                vidasTexto = textoObj.GetComponent<TextMeshProUGUI>();
                UpdateVidasTexto();
            }
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
