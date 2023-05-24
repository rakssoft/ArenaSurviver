
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private float _timerDestroy;
    void Start()
    {
        Destroy(gameObject, _timerDestroy);
    }


}
