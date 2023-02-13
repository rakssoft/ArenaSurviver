using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolText : MonoBehaviour
{
    [SerializeField] private List<TextDamage> _textDamageNotActiveList;
    private List<TextDamage> _textDamageIsActiveList = new List<TextDamage>();

    private void OnEnable()
    {
        EventManager.TakeDamage += ShowDamage;
    }

    private void OnDisable()
    {
        EventManager.TakeDamage -= ShowDamage;
    }

    private void ShowDamage(Vector3 pos, string damage)
    {
        _textDamageNotActiveList[0].gameObject.SetActive(true);
        _textDamageNotActiveList[0].ShowText(pos, damage);
        _textDamageIsActiveList.Add(_textDamageNotActiveList[0]);
        _textDamageNotActiveList.RemoveAt(0);
    }
}


