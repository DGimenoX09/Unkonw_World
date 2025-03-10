using UnityEngine;

public class AlternarMundo : MonoBehaviour
{
    public GameObject empty1; // Asigna el primer empty en el inspector
    public GameObject empty2; // Asigna el segundo empty en el inspector

    private bool isEmpty1Active = true; // Estado inicial, el primer empty esta activo

    void Start()
    {
        // Asegurate de que el primer empty este activo y el segundo este desactivado al inicio
        empty1.SetActive(true);
        empty2.SetActive(false);
    }

    void Update()
    {
        // Verifica si se presiona la tecla "J"
        if (Input.GetKeyDown(KeyCode.J))
        {
            ToggleEmpties();
        }
    }

    void ToggleEmpties()
    {
        // Alterna entre los dos mundos
        isEmpty1Active = !isEmpty1Active;

        empty1.SetActive(isEmpty1Active); //mundo normal
        empty2.SetActive(!isEmpty1Active); //mundo Unkown World
    }
}