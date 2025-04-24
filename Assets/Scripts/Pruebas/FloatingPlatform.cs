using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    public float amplitude = 0.5f; // Qué tanto sube y baja
    public float speed = 1f;       // Qué tan rápido lo hace
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
