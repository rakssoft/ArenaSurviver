using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolText : MonoBehaviour
{
    [SerializeField] TextDamage[] _textsDamageArr;
    private void OnEnable()
    {
        EventManager.TakeDamage += ShowDamage;
    }

    private void ShowDamage(Vector3 pos, string damage)
    {
        
        for (int i = 0; i < _textsDamageArr.Length; i++)
        {
            _textsDamageArr[i].gameObject.SetActive(true);
            
            if (_textsDamageArr[i].isActive == false)
            {
                _textsDamageArr[i].Start();
                _textsDamageArr[i].isActive = true;
                _textsDamageArr[i].gameObject.transform.position = pos;
                _textsDamageArr[i].Damagetext = damage;
                break;
            }
            else
            {
                _textsDamageArr[i].gameObject.SetActive(false);
            }
        }


      
    }

}
