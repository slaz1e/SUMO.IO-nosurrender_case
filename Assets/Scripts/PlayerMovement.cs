using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _movementSpeed = 1f;
    [SerializeField] float _rotationSpeed = 500f;

    private Touch _touch;

    Vector3 _touchDown;
    Vector3 _touchUp;

    bool _dragStarted;

    private void Update()
    {
        Movement();
    }
    void Movement()
    {
        gameObject.transform.Translate(Vector3.forward * Time.deltaTime * _movementSpeed);
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            if (_touch.phase == TouchPhase.Began)
            {
                _dragStarted = true;
                _touchDown = _touch.position;
                _touchUp = _touch.position;
            }
        }
        if (_dragStarted)
        {
            if (_touch.phase == TouchPhase.Moved)
            {
                _touchDown = _touch.position;
            }
            else if (_touch.phase == TouchPhase.Ended)
            {
                _touchDown = _touch.position;
                _dragStarted = false;
            }
            gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, CalculateRotation(), _rotationSpeed * Time.deltaTime);
        }
    }
    Quaternion CalculateRotation()
    {
        Quaternion _temp = Quaternion.LookRotation(CalculateDirection(), Vector3.up);
        return _temp;
    }
    Vector3 CalculateDirection()
    {
        Vector3 _temp = (_touchDown - _touchUp).normalized;
        _temp.z = _temp.y;
        _temp.y = 0;
        return _temp;
    }
}
