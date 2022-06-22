using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] AnimationCurve _dashCurve;

    [SerializeField] private float _travelSpeed;

    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashForce;

    private Rigidbody2D _rb;

    private bool _inWall = false;
    
    private bool _watchRight = true;

    public bool DashActivated { get; private set; } = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.GetComponent<SideWall>())
            _inWall = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<SideWall>())
            _inWall = false;
    }

    public void Move(Vector2 moveDirection)
    {
        _rb.velocity = moveDirection * Time.deltaTime * _travelSpeed;

        if (moveDirection.x < 0 && _watchRight == true)
            MirrorDirection();

        if (moveDirection.x > 0 && _watchRight != true)
            MirrorDirection();
    }

    public IEnumerator Dash()
    {
        DashActivated = true;
        Debug.Log("Coroutine has been started");

        float progress = 0;
        float expiredTime = 0;
        while (progress < 1)
        {
            expiredTime += Time.deltaTime;
            progress = expiredTime / _dashDuration;
            if(_inWall == false)
                transform.Translate(new Vector2(transform.localScale.x, 0) * Time.deltaTime * _dashCurve.Evaluate(progress) * _dashForce);

            yield return null;
        }

        DashActivated = false;
    }

    private void MirrorDirection()
    {
        transform.localScale *= new Vector2(-1, 1);
        _watchRight = !_watchRight;
    }
}