
using UnityEngine;
using TMPro;

public class TextDamage : MonoBehaviour
{
    [SerializeField] private float _disappearedTimer;
    private Color _textColor;
    public TMP_Text TextMesh;
    public bool isActive;
    [SerializeField] private float disappearSpeed;

    public void ShowText(Vector3 pos, string damageText )
    {
        float damage = float.Parse(damageText);        
        _textColor = TextMesh.color;
        disappearSpeed = 3;
        gameObject.transform.position = pos;
        TextMesh.text = damage.ToString("F0");

        _disappearedTimer = 1;
        isActive = true;
    }
    private void Update()
    {
        if (isActive == true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z + 0.01f);
            _disappearedTimer -= Time.deltaTime;
            if (_disappearedTimer < 0)
            {
                _textColor.a -= disappearSpeed * Time.deltaTime;
                TextMesh.color = _textColor;
                if (_textColor.a <= 0)
                {
                    isActive = false;   
                }
            }
        }
        else
        {
            _textColor.a = 1;
            TextMesh.color = _textColor;
            EventManager.TakeDamageIsOff?.Invoke(gameObject);
            gameObject.SetActive(false);

        }
    }
}
