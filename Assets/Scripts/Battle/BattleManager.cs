using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
//    [SerializeField] private TextMeshProUGUI _textTimerBattle;
    [SerializeField] private float _timerBattle;
    [SerializeField] private Text _textTimerBattle;
    [SerializeField] private bool _sceneBoss;

    void Update()
    {
        if (!_sceneBoss)
        {
            _timerBattle -= Time.deltaTime;
            _textTimerBattle.text = _timerBattle.ToString("F0");

            if (_timerBattle < 0)
            {
                _timerBattle = 0;
                EventManager.BatttleIsWon?.Invoke(true);
            }
        }
    }
}
