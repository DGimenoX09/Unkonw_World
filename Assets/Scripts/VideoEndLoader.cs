using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoEndLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName = "Unkonw_World"; // Pon aquí el nombre de tu escena del juego

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

    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
