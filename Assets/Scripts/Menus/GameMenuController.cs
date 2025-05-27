using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuController : MonoBehaviour
{
    public GameObject menuUI; 
    private bool isGamePaused = false;
    
    void Update()
    {
        // Abre o cierra el menu al presionar Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    public void ResumeGame()
    {
        menuUI.SetActive(false);
        Time.timeScale = 1f; // Reanuda el tiempo del juego
        isGamePaused = false;
    }

    public void OpenMenu()
    {
        menuUI.SetActive(true);
        Time.timeScale = 0f; // Pausa el tiempo del juego
        isGamePaused = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Asegurate de reanudar el tiempo antes de reiniciar
        menuUI.SetActive(false); // Cierra el menu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reinicia la escena actual
    }

    public void QuitGame()
    {
        // Carga la escena del menú principal
        SceneManager.LoadScene("MenuInicial"); // Asegúrate de que el nombre coincida con el de tu escena
    }
}