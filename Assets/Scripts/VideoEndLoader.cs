using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoEndLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName = "Unkonw_World"; // Pon aquí el nombre real de tu escena del juego

    private bool hasSkipped = false;

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoFinished;
        }
        else
        {
            Debug.LogError("No se asignó el VideoPlayer");
        }
    }

    void Update()
    {
        // Si se pulsa M y aún no hemos saltado
        if (Input.GetKeyDown(KeyCode.M) && !hasSkipped)
        {
            hasSkipped = true;
            LoadNextScene();
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        if (!hasSkipped)
        {
            hasSkipped = true;
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
