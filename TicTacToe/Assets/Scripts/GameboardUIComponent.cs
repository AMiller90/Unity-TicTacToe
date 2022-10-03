using UnityEngine;
using UnityEngine.UI;

public class GameboardUIComponent : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup _gridLayoutGroup;
    [SerializeField] private RectTransform _rectTransform;
    public void Initialize(int maxSize, int textureSize)
    {
        _gridLayoutGroup.constraintCount = maxSize;
        _rectTransform.sizeDelta = new Vector2( maxSize * textureSize, maxSize * textureSize);
    }
}
