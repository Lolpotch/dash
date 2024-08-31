using UnityEngine;

public class ParticleLife : MonoBehaviour
{
    [SerializeField] float lifeTime = 2f;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
