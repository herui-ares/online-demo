using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType : byte
{
    Equipment,
    Material,
    Consuable
}

public class Item
{
    public ItemType type;
    public int id;
    public Sprite sprite;
}
