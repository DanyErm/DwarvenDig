using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class DwarfControls : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Vector2 _checkBoxSize;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Animator _dwarfAnimator;
    [SerializeField] private AnimationClip _digClip;

    [Inject] private GameSettings _gameSettings;
    [Inject] private DiggingProcess _diggingProcess;

    private Movement _movement;
    private Rigidbody2D _rb;



    void Awake()
    {
        _movement = new Movement();
        _rb = GetComponent<Rigidbody2D>();
    }


    private void Start()
    {
        if (_movement == null)
            Debug.LogWarning("_movement = null");
    }


    void FixedUpdate()
    {
        _movement.Walk(_rb, Input.GetAxis("Horizontal") * _gameSettings.WalkSpeed);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _movement.Jump(_rb, _gameSettings.JumpVelocity, _groundCheck, _checkBoxSize, _groundLayer);
            _dwarfAnimator.Play("JumpAnim");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _movement.CutJump(_rb, 0.5f);
        }

        if (Input.GetMouseButtonDown(0))
        {

        }
    }


    private Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    private IEnumerator DigWithDelay()
    {
        Vector2 directionFromCharToMouse = (GetMousePos() - _rb.position).normalized;
        _dwarfAnimator.Play("Attack1Anim");
        yield return new WaitForSeconds(_digClip.length);
        _diggingProcess.Dig(_rb.position, directionFromCharToMouse);
    }
}