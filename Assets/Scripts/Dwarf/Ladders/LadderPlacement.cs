using UnityEngine;
using Zenject;

public class LadderPlacement : MonoBehaviour
{
    [SerializeField] private LayerMask _absentBlockLayer;
    [SerializeField] private LayerMask _ladderLayer;
    [SerializeField] private GameObject _ladderPrefab;
    [SerializeField] private Transform _laddersParentsTrans;

    [Inject] private Inventory _inventory;


    private void Start()
    {
        _inventory.ChangeAmountOfItemInInventory(5, 10);
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && _inventory.GetAmountOfItem(5) > 0 && CheckIfCanPlaceLadder())
        {
                TryPlaceLadder();
                _inventory.ChangeAmountOfItemInInventory(5, -1);
        }
    }


    private void TryPlaceLadder()
    {
        Collider2D hit = Physics2D.OverlapPoint(transform.position, _absentBlockLayer);

        if (hit != null)
        {
            Instantiate(_ladderPrefab, hit.transform.position, Quaternion.identity, _laddersParentsTrans);
        }
    }

    private bool CheckIfCanPlaceLadder()
    {
        Collider2D hit = Physics2D.OverlapPoint(transform.position, _ladderLayer);

        if (hit != null)
        {
            return false;
        }
        return true;
    }
}