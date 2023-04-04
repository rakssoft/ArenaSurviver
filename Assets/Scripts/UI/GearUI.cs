
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class GearUI : MonoBehaviour
{
    [SerializeField] private Gear _gear;
    [SerializeField] private Image _spriteGear;
    [SerializeField] private Image _spriteCharacteristic;
    [SerializeField] private TextMeshProUGUI _valueCharacteristic;

    public UnityAction<Gear> EquipGearEvent;

    private void Start()
    {
        _spriteGear.sprite = _gear.Sprite;
        _spriteCharacteristic.sprite = _gear.SpriteCharackteristic;
        _valueCharacteristic.text = _gear.Value.ToString();
    }
    public void EquipGear()
    {
        EquipGearEvent?.Invoke(_gear);
        Destroy(gameObject);
    }

    public void ShowGear(Gear gear)
    {
        _gear = gear;
    }
}
