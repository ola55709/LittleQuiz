using UnityEngine;

public class BusMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float minSpeed = 7f;
    [SerializeField]
    private float maxSpeed = 13f;
    [SerializeField]
    private float loopLength = 125f;

    [Header("Fluctuation Settings")]
    [Tooltip("Higher value = faster fluctuations")]
    [SerializeField]
    private float noiseSpeed = 0.5f;

    private Vector3 startPosition;
    private float currentDistance = 0f;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(Time.time * noiseSpeed, 0);
        float currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, noise);
        currentDistance += currentSpeed * Time.deltaTime;
        float loopedZ = currentDistance % loopLength;

        transform.position = new Vector3(
            startPosition.x,
            startPosition.y,
            startPosition.z + loopedZ
        );
    }
}
