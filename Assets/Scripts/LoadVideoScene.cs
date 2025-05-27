using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadVideoScene : MonoBehaviour
{
    public string sceneToLoad = "FinalVideoScene";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
