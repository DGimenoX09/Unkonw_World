using UnityEngine;

public class ActivarObjetoConTrigger : MonoBehaviour
{
    [Tooltip("Arrastra aquí el objeto que quieres activar cuando el jugador entre")]
    public GameObject objetoAActivar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (objetoAActivar != null)
            {
                objetoAActivar.SetActive(true);
            }
            else
            {
                Debug.LogWarning("No se asignó el objeto a activar en el Inspector.");
            }
        }
    }
}
