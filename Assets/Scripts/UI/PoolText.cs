using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolText : MonoBehaviour
{
    [SerializeField] private List<TextDamage> _textDamageNotActiveList;
    private Queue<TextDamage> _textDamageQueue = new Queue<TextDamage>();

    private void Awake()
    {
        EventManager.TakeDamage += ShowDamage;
        EventManager.TakeDamageIsOff += AddTextDamageToQueue;
    }

    private void OnDestroy()
    {
        EventManager.TakeDamage -= ShowDamage;
        EventManager.TakeDamageIsOff -= AddTextDamageToQueue;
    }

    private void ShowDamage(Vector3 pos, string damage)
    {
        if (_textDamageQueue.Count > 0)
        {
            TextDamage textDamage = _textDamageQueue.Dequeue();
            textDamage.gameObject.SetActive(true);
            textDamage.ShowText(pos, damage);
            _textDamageNotActiveList.Add(textDamage);
        }
    }

    private void AddTextDamageToQueue(GameObject gameObject)
    {
        TextDamage textDamage = gameObject.GetComponent<TextDamage>();
        textDamage.gameObject.SetActive(false);
        _textDamageQueue.Enqueue(textDamage);
        _textDamageNotActiveList.Remove(textDamage);
    }
}
