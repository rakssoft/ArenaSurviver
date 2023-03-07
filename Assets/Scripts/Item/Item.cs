
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string ItemName;
    public Sprite Icon;
    public AudioClip Sound;
    public float Weight;

    public abstract void Activate(GameObject parentObject);

}
