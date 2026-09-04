using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class BlockState : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private BoxCollider2D _boxCollider2D;

    [Header("Blocks")]
    public Sprite[] blocksSprites;

    private int _blockId;


    private void Start()
    {
        _blockId = Random.Range(1, 4);      // Chances should be changed
        _spriteRenderer.sprite = blocksSprites[_blockId]; // Random unbroken block
    }

    public int Destroy(Collider2D collider)
    {
        _boxCollider2D.enabled = false;
        _spriteRenderer.sprite = blocksSprites[0];
        return _blockId;
    }
}