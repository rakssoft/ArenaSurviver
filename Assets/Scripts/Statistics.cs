using UnityEngine.UI;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    [SerializeField] private Text _curCountEnemyText;  
    private int _currentCountEnemy;

    private void OnEnable()
    {
        EventManager.CurrentCountEnemy += CurrentEnemy;
    }

    private void OnDisable()
    {
        EventManager.CurrentCountEnemy -= CurrentEnemy;
    }

    private void CurrentEnemy(int count)
    {
        _currentCountEnemy += count;
        _curCountEnemyText.text = _currentCountEnemy.ToString();
    }



}
