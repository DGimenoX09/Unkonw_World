using UnityEngine;

public class CambioDeMundo : MonoBehaviour
{
    public GameObject escenario1; // Asigna el primer escenario en el Inspector
    public GameObject escenario2; // Asigna el segundo escenario en el Inspector

    private GameObject escenarioActual;

    void Start()
    {
        // Inicializa el escenario actual
        escenarioActual = escenario1;
        escenario1.SetActive(true);
        escenario2.SetActive(false);
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
            

            // Aumentar la posicion Y en 60 al cambiar al escenario 2
            transform.position = new Vector3(transform.position.x, transform.position.y + 60, transform.position.z);
        }
        else
        {
            escenarioActual = escenario1;
            escenario2.SetActive(false);
            escenario1.SetActive(true);
            

            // Disminuir la posicion Y en 60 al volver al escenario 1
            transform.position = new Vector3(transform.position.x, transform.position.y - 60, transform.position.z);
        }
    }
}