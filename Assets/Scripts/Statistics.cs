using UnityEngine.UI;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    [SerializeField] private Text _curCountEnemyText;
    [SerializeField] private Text _timerLevelText;
    private int _currentCountEnemy;
    private float _timerLevel;



    private void OnEnable()
    {
        EventManager.CurrentCountEnemy += CurrentEnemy;
    }

    private void OnDisable()
    {
        EventManager.CurrentCountEnemy -= CurrentEnemy;
    }

    private void Start()
    {
        _timerLevel = 0;
    }

    private void CurrentEnemy(int count)
    {
        _currentCountEnemy += count;
        _curCountEnemyText.text = _currentCountEnemy.ToString();
    }

    private void Update()
    {
        _timerLevel += Time.deltaTime;
        _timerLevelText.text = _timerLevel.ToString("F0");
    }

}
