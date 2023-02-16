using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolText : MonoBehaviour
{
    [SerializeField] private List<TextDamage> _textDamageNotActiveList;

    private void OnEnable()
    {
        EventManager.TakeDamage += ShowDamage;
        EventManager.TakeDamageIsOff += AddListTextDamageNotActive;
    }

    private void OnDisable()
    {
        EventManager.TakeDamage -= ShowDamage;
        EventManager.TakeDamageIsOff -= AddListTextDamageNotActive;
    }

    private void ShowDamage(Vector3 pos, string damage)
    {
        _textDamageNotActiveList[0].gameObject.SetActive(true);
        _textDamageNotActiveList[0].ShowText(pos, damage);
        _textDamageNotActiveList.RemoveAt(0);
    }

    private void AddListTextDamageNotActive(GameObject gameObject)
    {
        _textDamageNotActiveList.Add(gameObject.GetComponent<TextDamage>());
    }
}


