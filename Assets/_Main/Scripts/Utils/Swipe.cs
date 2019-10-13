using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    bool _tap, _swipeLeft, _swipeRight, _swipeUp, _swipeDown;
    Vector2 _startTouch, _swipeDelta;

    bool _isDraging = false;

    public Vector2 SwipeDelta { get { return _swipeDelta; } }
    public bool SwipeLeft { get { return _swipeLeft; } }
    public bool SwipeRight { get { return _swipeRight; } }
    public bool SwipeUp { get { return _swipeUp; } }
    public bool SwipeDown { get { return _swipeDown; } }
    public bool Tap { get { return _tap; } }
    public bool IsDraging { get { return _isDraging; } }

    private void Update()
    {
        _tap = _swipeUp = _swipeDown = _swipeLeft = _swipeRight = false;

        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            _tap = true;
            _isDraging = true;
            _startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isDraging = false;
            Reset();
        }
        #endregion

        #region Mobile Inputs
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                _tap = true;
                _isDraging = true;
                _startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended ||
                    Input.touches[0].phase == TouchPhase.Canceled)
            {
                _isDraging = false;
                Reset();
            }
        }
        #endregion

        //Calculate DIstance
        _swipeDelta = Vector2.zero;

        if (_isDraging)
        {
            if (Input.touches.Length > 0)
            {
                _swipeDelta = Input.touches[0].position - _startTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                _swipeDelta = (Vector2)Input.mousePosition - _startTouch;
            }
        }

        //Did we cross a dead zone, deadZone = 125
        if (_swipeDelta.magnitude > 125)
        {
            //which direction ?
            float x = _swipeDelta.x;
            float y = _swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Left or Right
                if (x < 0)
                    _swipeLeft = true;
                else
                    _swipeRight = true;
            }
            else
            {
                if (y < 0)
                    _swipeDown = true;
                else
                    _swipeUp = true;
            }

            Reset();
        }
    }

    private void Reset()
    {
        _startTouch = _swipeDelta = Vector2.zero;
        _isDraging = false;
    }
}
