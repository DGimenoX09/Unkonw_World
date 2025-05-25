using UnityEngine;

public class AlternarMundo : MonoBehaviour
{
    public GameObject empty1; // Mundo normal
    public GameObject empty2; // Unknown World

    [Header("Sonidos de cambio de mundo")]
    public AudioSource sonidoMundoNormal;
    public AudioSource sonidoMundoAlterno;

    float ltThreshold = 0.01f;
    bool ltPressedLastFrame = false;

    private bool isEmpty1Active = true;

    void Start()
    {
        empty1.SetActive(true);
        empty2.SetActive(false);
    }

    void Update()
    {
        bool ltPressedNow = Input.GetAxis("LT") > ltThreshold;

        if (Input.GetKeyDown(KeyCode.J) || Input.GetButtonDown("ChangeWorld") || (ltPressedNow && !ltPressedLastFrame))
        {
            ToggleEmpties();
        }

        ltPressedLastFrame = ltPressedNow;
    }

    void ToggleEmpties()
    {
        isEmpty1Active = !isEmpty1Active;

        empty1.SetActive(isEmpty1Active);
        empty2.SetActive(!isEmpty1Active);

        if (isEmpty1Active)
        {
            if (sonidoMundoNormal != null)
                sonidoMundoNormal.Play();
        }
        else
        {
            if (sonidoMundoAlterno != null)
                sonidoMundoAlterno.Play();
        }
    }
}
