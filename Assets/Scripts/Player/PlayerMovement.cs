using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _travelSpeed;

    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashForce;

    //[SerializeField] private Animator _animator;

    [SerializeField] AnimationCurve _dashCurve;
    
    private bool _watchRight = true;

    private Vector2 _lastDirection = new Vector2(1,0);

    public void Move(Vector2 moveDirection)
    {
        transform.Translate(moveDirection * Time.fixedDeltaTime * _travelSpeed);

        //_animator.SetFloat("moveX", moveDirection.x);

        if (moveDirection.x < 0 && _watchRight == true)
        {
            MirrorDirection();
            _lastDirection = new Vector2(-1, 0);
        }
        if (moveDirection.x > 0 && _watchRight != true)
        {
            MirrorDirection();
            _lastDirection = new Vector2(1, 0);
        }
    }

    public IEnumerator Dash()
    {
        Debug.Log("Coroutine has been started");

        float progress = 0;
        float expiredTime = 0;
        while (progress < 1)
        {
            expiredTime += Time.deltaTime;
            progress = expiredTime / _dashDuration;

            transform.Translate(_lastDirection * Time.deltaTime * _dashCurve.Evaluate(progress) * _dashForce);

            yield return null;
        }
    }

    private void MirrorDirection()
    {
        transform.localScale *= new Vector2(-1, 1);
        _watchRight = !_watchRight;
    }
}