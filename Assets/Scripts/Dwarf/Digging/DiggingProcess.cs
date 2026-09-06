using UnityEngine;
using Zenject;

public class DiggingProcess : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject _blockPrefab;
    [SerializeField] private Transform _blocksParent;


    [SerializeField] private LayerMask _blockLayer;
    [SerializeField] private LayerMask _charLayer;
    [SerializeField] private LayerMask _absentBlockLayer;
    [SerializeField] private Inventory _inventory;


    [Inject] private GameSettings _gameSettings;


    private Collider2D _collider;
    //private int _blockId;
    private BlockState _blockState;

    private Vector2[] orthogonalDirections = {
        Vector2.up,
        Vector2.right,
        Vector2.down,
        Vector2.left
    };



    public void Dig(Vector2 charPos, Vector2 direction)      // In Inventory script this output will be used to refresh
    {
        if (CheckIfTheresBlock(charPos, direction, _gameSettings.DiggingRange))
        {
            _inventory.ChangeAmountOfItemInInventory(DestroyBlock(_collider), 1);
            CreateBlocksAround(charPos, direction);
        }
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


    private int DestroyBlock(Collider2D collider)
    {
        _blockState = collider.gameObject.GetComponent<BlockState>();
        return _blockState.Destroy();
    }


    void CreateBlocksAround(Vector2 charPos, Vector2 direction)
    {
        foreach (Vector2 orthogonalDirection in orthogonalDirections)
        {
            Collider2D hitCollider = Physics2D.OverlapPoint(charPos + direction * _gameSettings.DiggingRange + orthogonalDirection * _gameSettings.BlockSize);

            if (CanCreateBlock(hitCollider, orthogonalDirection))
            {
                CreateBlock((Vector2)_collider.transform.position + orthogonalDirection * _gameSettings.BlockSize);
            }
        }
    }


    private bool CanCreateBlock(Collider2D collider, Vector2 orthogonalDirection)
    {
        if (_collider.gameObject.transform.position.y >= 0 && orthogonalDirection == Vector2.up)
        {
            return false;
        }

        if (collider == null)
        {
            return true;
        }

        if (collider.gameObject.layer == LayerMask.NameToLayer("Block") ||
            collider.gameObject.layer == LayerMask.NameToLayer("Character") ||
            collider.gameObject.layer == LayerMask.NameToLayer("AbsentBlock"))
        {
            return false;
        }
        return true;
    }


    private void CreateBlock(Vector2 blockPos)
    {
        Instantiate(_blockPrefab, blockPos, Quaternion.identity, _blocksParent);
    }
}