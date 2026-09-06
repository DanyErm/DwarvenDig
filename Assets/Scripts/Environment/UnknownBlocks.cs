using UnityEngine;

public class UnknownBlocks : MonoBehaviour
{
    [SerializeField] private GameObject _unknownBlock;
    [SerializeField] private Transform _char;


    private int _leftX = -9;
    private int _rightX = 10;
    private int _topY = -2;
    private int _bottomY = -6;


    private float _currentBottomY;



    private void Start()
    {
        for (int y = _topY; y >= _bottomY; y--)
        {
            CreateRow(y);
        }
        _currentBottomY = _bottomY;
    }

    private void Update()
    {
        if (_char == null) return;

        if (_char.position.y < _currentBottomY + 6f)
        {
            float newY = _currentBottomY - 1f;
            CreateRow(Mathf.RoundToInt(newY));
            _currentBottomY = newY;
        }
    }

    private void CreateRow(int y)
    {
        for (int x = _leftX; x <= _rightX; x++)
        {
            Vector3 position = new Vector3(x, y, 0f);
            Instantiate(_unknownBlock, position, Quaternion.identity, transform);
        }
    }
}