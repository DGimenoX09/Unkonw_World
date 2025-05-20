using UnityEngine;

public class ActivarDesactivarObjetoConTrigger : MonoBehaviour
{
    [Tooltip("Objeto que se activará o desactivará")]
    public GameObject objetoObjetivo;

    [Tooltip("¿Este trigger activa el objeto? Si no, lo desactiva.")]
    public bool activaObjeto = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (objetoObjetivo != null)
            {
                objetoObjetivo.SetActive(activaObjeto);
            }
            else
            {
                Debug.LogWarning("No se asignó el objeto objetivo en el Inspector.");
            }
        }
    }
}
