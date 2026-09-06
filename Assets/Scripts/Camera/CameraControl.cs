using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform _charTrans;
    private float _cameraCharPosYDiff;

    private void Start()
    {
        _cameraCharPosYDiff = transform.position.y - _charTrans.position.y;
    }
    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, _charTrans.position.y + _cameraCharPosYDiff, transform.position.z);
    }
}