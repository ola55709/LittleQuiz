using UnityEngine;

public class OrbitParent : MonoBehaviour
{
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private float orbitSpeed = 50f;

    void Update()
    {
        transform.RotateAround(
            parent.position,
            Vector3.up,
            orbitSpeed * Time.deltaTime
        );
    }
}