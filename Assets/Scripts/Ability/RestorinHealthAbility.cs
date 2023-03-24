using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/RestorinHealth")]
public class RestorinHealthAbility : Ability
{
    [SerializeField] private int _baseRestoringHealth;
    [SerializeField] private int _currentRestoringHealth;
    [SerializeField] private GameObject _restoringHealth;

    public override void Activate(GameObject parent)
    {
         if(_currentRestoringHealth < _baseRestoringHealth)
        {
            _currentRestoringHealth = _baseRestoringHealth;
        }
        GameObject prefab = Instantiate(_restoringHealth, parent.transform.position, Quaternion.identity);
        RestoringHealthposition positionHandler = prefab.AddComponent<RestoringHealthposition>();
        positionHandler.parent = parent.transform;
        Destroy(prefab, Duration);
        float RestoreHealth = _currentRestoringHealth;
        EventManager.TakeDamagePlayer?.Invoke(RestoreHealth);
    }

    public override void LevelUp()
    {      
        _currentRestoringHealth ++;

    }

    private void OnEnable()
    {
        _currentRestoringHealth = Level;
    }

}
public class RestoringHealthposition : MonoBehaviour
{
    public Transform parent;

    private void Update()
    {
        transform.position = parent.position;
    }


}
