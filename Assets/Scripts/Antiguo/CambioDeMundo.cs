using UnityEngine;

public class CambioDeMundo : MonoBehaviour
{
    public GameObject escenario1; // Asigna el primer escenario en el Inspector
    public GameObject escenario2; // Asigna el segundo escenario en el Inspector
    public GameObject filtro; // Asigna el objeto de filtro en el Inspector



    private GameObject escenarioActual;

    void Start()
    {
        // Inicializa el escenario actual
        escenarioActual = escenario1;
        escenario1.SetActive(true);
        escenario2.SetActive(false);
        filtro.SetActive(false); // Asegurate de que el filtro este desactivado al inicio
    }

    void Update()
    {
        // Cambia de escenario al presionar la tecla 'J'
        if (Input.GetKeyDown(KeyCode.J))
        {
            CambiarEscenario();
        }
    }

    void CambiarEscenario()
    {
        // Cambia el estado de los escenarios
        if (escenarioActual == escenario1)
        {
            escenarioActual = escenario2;
            escenario1.SetActive(false);
            escenario2.SetActive(true);
            filtro.SetActive(true); // Activa el filtro al cambiar al escenario 2
            Debug.Log("Filtro activado");
            

            // Aumentar la posicion Y en 60 al cambiar al escenario 2
            transform.position = new Vector3(transform.position.x, transform.position.y + 60, transform.position.z);
        }
        else
        {
            escenarioActual = escenario1;
            escenario2.SetActive(false);
            escenario1.SetActive(true);
            filtro.SetActive(false); // Desactiva el filtro al volver al escenario 1
            

            // Disminuir la posicion Y en 60 al volver al escenario 1
            transform.position = new Vector3(transform.position.x, transform.position.y - 60, transform.position.z);
        }
    }
}