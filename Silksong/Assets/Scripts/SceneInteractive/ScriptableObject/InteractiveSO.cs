using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interactive", menuName = "Interactive/Interactive")]
public class InteractiveSO : ScriptableObject
{
    [Tooltip("The id of interactive item")]
    [SerializeField] private int _interactiveID = default;
    [Tooltip("The name of the item")]
    [SerializeField] private string _name = default;
    [Tooltip("The type of the item")]
    [SerializeField] private EInteractiveItemType _itemType = default;
    [Tooltip("The content of the item")]
    [SerializeField] private string _content = default;
    [Tooltip("The icon of the item")]
    [SerializeField] private Sprite _icon = default;
    [Tooltip("The coord of the item")]
    [SerializeField] private Vector3 _coord = default;
    [Tooltip("The face of the item")]
    [SerializeField] private bool _is_face_right = default;

    public int InteractiveID => _interactiveID;
    public string Name => _name;
    public EInteractiveItemType ItemType => _itemType;
    public string Content => _content;
    public Sprite Icon => _icon;
    public Vector3 Coord => _coord;
    public bool IsFaceRight => _is_face_right;

    public void SetCoord (Vector3 coord)
    {
        _coord = coord;
    }
}
