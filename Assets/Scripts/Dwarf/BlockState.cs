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


    [SerializeField] private int[] weights;


    private void Start()
    {
        _blockId = GetRandomIdByWeight(weights);
        _spriteRenderer.sprite = blocksSprites[_blockId];
    }


    public int Destroy()
    {
        _boxCollider2D.isTrigger = true;
        _boxCollider2D.gameObject.layer = LayerMask.NameToLayer("AbsentBlock");
        _spriteRenderer.sprite = blocksSprites[0];
         return _blockId;
    }


    private int GetRandomIdByWeight(int[] weights)
    {
        int totalWeight = 0;
        foreach (int weight in weights) totalWeight += weight;

        int randomValue = Random.Range(0, totalWeight);

        int cumulative = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            cumulative += weights[i];
            if (randomValue < cumulative)
                return i + 1;
        }
        return 1;
    }
}