using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/RestorinHealth")]
public class RestorinHealthAbility : Ability
{
    [SerializeField] private int _baseRestoringHealth;   
    [SerializeField] private GameObject _restoringHealth;
    [SerializeField] private int _currentLevel;

    private float _currentRestoringHealth;

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
        EventManager.TakeDamagePlayer?.Invoke(_currentRestoringHealth);
    }

    public override void LevelUp()
    {      
        _currentLevel++;
        float MultimultiplierDamage = 0.2f;
        _currentRestoringHealth += _currentRestoringHealth * MultimultiplierDamage;
    }

    public override void EnableAbility(float Damage)
    {
        _currentLevel = 1;
        _currentRestoringHealth = _baseRestoringHealth;
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
