using UnityEngine;

public class DiggingProcess : MonoBehaviour
{
    [Header("Distances")]
    [SerializeField] private float _blockSize;
    [SerializeField] private float _diggingRange;       // Some variables should be set by ScriptableObject


    [Header("GameObjects")]
    [SerializeField] private GameObject _blockPrefab;
    [SerializeField] private Transform _blocksParent;


    [SerializeField] private LayerMask _blockLayer;
    [SerializeField] private Inventory _inventory;


    private Collider2D _collider;
    private int _blockId;
    private BlockState _blockState;

    private Vector2[] orthogonalDirections = {
        Vector2.up,
        Vector2.right,
        Vector2.down,
        Vector2.left
    };



    public int Dig(Vector2 charPos, Vector2 direction)      // In Inventory script this output will be used to refresh
    {
        if (CheckIfTheresBlock(charPos, direction, _diggingRange))
        {
            DestroyBlock(_collider);
            CreateBlocksAround(charPos, direction);
            return _blockId;
        }
        return -2;
    }


    private bool CheckIfTheresBlock(Vector2 pos, Vector2 direction, float distance)
    {
        RaycastHit2D blockHit = Physics2D.Raycast(pos, direction, distance, _blockLayer);
        _collider = blockHit.collider;

        if (_collider != null)
        {
            return true;
        }
        return false;
    }


    private void DestroyBlock(Collider2D collider)
    {
        _blockState = collider.gameObject.GetComponent<BlockState>();
        _blockId = _blockState.Destroy(collider);
    }


    void CreateBlocksAround(Vector2 charPos, Vector2 direction)
    {
        foreach (Vector2 orthogonalDirection in orthogonalDirections)
        {
            RaycastHit2D blockHit = Physics2D.Raycast(charPos + direction, orthogonalDirection, _blockSize, _blockLayer);
            if (CanCreateBlock(blockHit.collider))
            {
                CreateBlock(charPos + direction * _diggingRange + orthogonalDirection * _blockSize);
            }
        }
    }


    private bool CanCreateBlock(Collider2D collider)
    {
        if (collider != null)
        {
            return false;
        }
        if (collider.gameObject.transform.position.y < 0)   //Have no idea whare surface should be
        {
            return true;
        }
        return false;
    }


    private void CreateBlock(Vector2 blockPos)
    {
        Instantiate(_blockPrefab, blockPos, Quaternion.identity, _blocksParent);
    }
}