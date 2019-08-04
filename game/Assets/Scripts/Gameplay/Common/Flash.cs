using UnityEngine;

public class Flash : MonoBehaviour
{
    public void Awake()
    {
        Destroy(gameObject, 1f);
    }
}
