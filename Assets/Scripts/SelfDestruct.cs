using UnityEngine;
public class SelfDestruct : MonoBehaviour
{
    public float lifetime = 0.5f;
    void Start() { Destroy(gameObject, lifetime); }
}