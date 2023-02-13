
using UnityEngine;
using TMPro;

public class TextDamage : MonoBehaviour
{
    [SerializeField] private float _disappearedTimer;
    private Color _textColor;

    public TMP_Text TextMesh;
    public bool isActive;

    public void ShowText(Vector3 pos, string damageText )
    {
        gameObject.transform.position = pos;
        TextMesh.text = damageText.ToString();
        _textColor = TextMesh.color;
        _disappearedTimer = 1;
        isActive = true;
    }
    private void Update()
    {
        if (isActive == true)
        {
            transform.position += new Vector3(transform.position.x, 1, 1) * Time.deltaTime;
            _disappearedTimer -= Time.deltaTime;
            if (_disappearedTimer < 0)
            {
                float disappearSpeed = 3f;
                _textColor.a -= disappearSpeed * Time.deltaTime;
                TextMesh.color = _textColor;
                if (_textColor.a <= 0)
                {
                    isActive = false;
                    _textColor.a = 100;
                    gameObject.SetActive(false);
       
                }
            }
        }
    }
}
