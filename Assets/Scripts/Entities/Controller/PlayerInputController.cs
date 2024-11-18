using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : TopDownController
{
    private Camera _camera;
    private bool canMove = true;
    private bool canLook = true;

    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main;
    }

    private void Start()
    {
        UIManager.Instance.pause.OnPauseEvent += OnPause;
        UIManager.Instance.pause.OnResumeEvent += OnResume;
    }

    public void OnMove(InputValue value)
    {
        if (canMove)
        {
            Vector2 moveInput = value.Get<Vector2>().normalized;
            CallMoveEvent(moveInput);
            // 움직임은 PlayerMovement에서 함
        }
    }

    public void OnLook(InputValue value)
    {
        if (canLook)
        {
            Vector2 nowAim = value.Get<Vector2>();
            Vector2 worldPos = _camera.ScreenToWorldPoint(nowAim);
            // 마우스의 위치를 월드 좌표로 전환한다.
            nowAim = (worldPos - (Vector2)transform.position).normalized;
            // transform에서 worldpos로 이동하는 것(t->w)은 w-t로 표현한다.

            CallLookEvent(nowAim);
        }
    }

    public void OnFire(InputValue value)
    {
        IsAttacking = value.isPressed;
    }

    private void OnPause()
    {
        canMove = false;
        canLook = false;
    }

    private void OnResume()
    {
        canMove = true;
        canLook = true;
    }
}