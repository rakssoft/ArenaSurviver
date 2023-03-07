
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioClip[] _audioClips;
    [SerializeField] private AudioSource _audioSource;

    private void OnEnable()
    {
        EventManager.IndexAudioClipPlay += PlaySound;
    }

    private void OnDisable()
    {
        EventManager.IndexAudioClipPlay -= PlaySound;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="indexClip"></param>
    public void PlaySound(int indexClip)
    {
        _audioSource.PlayOneShot(_audioClips[indexClip]);
    }
}
