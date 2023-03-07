
using UnityEngine;

public class ItemMove : MonoBehaviour
{

    [SerializeField] private float _speed;
    public Transform _target;


    private void Update()
    {
        if (_target == null)
        {
            return;
        }
        else
        {
            Vector3 direction = _target.position - transform.position;
            float distance = direction.magnitude;

            if (distance > 0)
            {
                float step = Mathf.Min(_speed * Time.deltaTime, distance);
                transform.position += direction.normalized * step;
            }
        }

    }
}



