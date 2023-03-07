using UnityEngine;
using UnityEngine.UI;


public class Manager : MonoBehaviour
{
    [SerializeField] private GameObject _musicOn, _musicOff;
    [SerializeField] private GameObject _soundOn, _soundOff;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundSource;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;


    private void OnEnable()
    {
        EventManager.BatttleIsWon += EndBattle;
    }

    private void OnDisable()
    {
        EventManager.BatttleIsWon -= EndBattle;
    }
    private void Start()
    {
        if (_winPanel)
        _winPanel.SetActive(false);
        if(_losePanel)
        _losePanel.SetActive(false);
        if(_musicOff)
        _musicOff.SetActive(false);
        if(_musicOn)
        _musicOn.SetActive(true);     
        if(_soundOn)
        _soundOn.SetActive(true);
        if(_soundOff)
        _soundOff.SetActive(false);

        _musicSource.Play();
        _soundSource.volume = 1;
    }
    public void SwitchMusic()
    {
        if(_musicOn.activeSelf == true)
        {
            _musicOn.SetActive(false);
            _musicOff.SetActive(true);
            _musicSource.Stop();

        }
        else
        {
            _musicOff.SetActive(false);
            _musicOn.SetActive(true);
            _musicSource.Play();
        }
    } 
    public void SwitchSound()
    {
        if(_soundOn.activeSelf == true)
        {
            _soundOn.SetActive(false);
            _soundOff.SetActive(true);
            _soundSource.volume = 0;

        }
        else
        {
            _soundOff.SetActive(false);
            _soundOn.SetActive(true);          
            _soundSource.volume = 1;
        }
    }
    /// <summary>
    /// index clip
    /// </summary>
    /// <param name="index"></param>
    public void PlaySoundClipIndex(int index)
    {
        EventManager.IndexAudioClipPlay?.Invoke(index);
    }

    private void EndBattle(bool isWin)
    {
        Time.timeScale = 0;
        if (isWin == true)
        {
            _winPanel.SetActive(true);
        }
        else
        {
            _losePanel.SetActive(true);
        }
    }


}
