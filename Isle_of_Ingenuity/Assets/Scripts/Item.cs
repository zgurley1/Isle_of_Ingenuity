using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using System.ComponentModel;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public TileBase tile;
    public Sprite image;
    public ItemType type;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5, 4);
    public bool stackable = true;


}

public enum ItemType {
    Tool,
    Resource
}

public enum ActionType {
    Attack,
    Place, 
    Throw
}