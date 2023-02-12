
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _forceBullet;
    [SerializeField] private float _timerForFalse;
   
    private float _timer;
    public int BulletPenetrationAbility;
    public float DamageBullet;
    public void StartBullet()
    {
        _timer = 0;
        GetComponent<Rigidbody>().AddForce(transform.forward * _forceBullet, ForceMode.Impulse);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= _timerForFalse)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(DamageBullet);
            BulletPenetrationAbility--;
            if(BulletPenetrationAbility <= 0)
            {
                gameObject.SetActive(false);
            }

        }
    }

}
