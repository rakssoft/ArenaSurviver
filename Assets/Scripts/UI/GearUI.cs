
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
    public bool IsEquipped;
    public Gear.GearStyle EquipmentType { get; private set; }


    private void Start()
    {
        _spriteGear.sprite = _gear.Sprite;
        _spriteCharacteristic.sprite = _gear.SpriteCharackteristic;
        _valueCharacteristic.text = _gear.Value.ToString();
    }
    public void EquipGear()
    {
        if(IsEquipped == false)
        {
            EventManager.EquipGearEvent?.Invoke(_gear);
            Destroy(gameObject);
        }
        else
        {
            EventManager.UnEquipGearEvent?.Invoke(_gear);
            Destroy(gameObject);
        }
    
    }

    public void ShowGear(Gear gear)
    {
        _gear = gear;
        _spriteGear.sprite = _gear.Sprite;      
        _valueCharacteristic.text = _gear.Value.ToString();
        EquipmentType = _gear.EquipmentType;
    }

 
    public void SriteCharackteristick(Sprite sprite)
    {
        _spriteCharacteristic.sprite = sprite;
    }

   
}
