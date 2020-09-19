using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiTouchDisplay : MonoBehaviour
{
    [SerializeField]
    private Text _multiTouchInfoDisplay;
    private Touch _theTouch;
    private string _multiTouchInfo;
    private int _maxTapCount;

    // Update is called once per frame
    void Update()
    {
        _multiTouchInfo = $"Max tap count: {_maxTapCount}";

        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                _theTouch = Input.GetTouch(i);

                var radius = (_theTouch.radius / (_theTouch.radius + _theTouch.radiusVariance) * 100f).ToString("F1");

                _multiTouchInfo += $"Touch: {i} - Position: {_theTouch.position} - Tap Count: {_theTouch.tapCount} - Finger ID: {_theTouch.fingerId}" +
                    $"\nRadius: {_theTouch.radius} {radius}";

                if (_theTouch.tapCount > _maxTapCount)
                {
                    _maxTapCount = _theTouch.tapCount;
                }
            }
        }

        _multiTouchInfoDisplay.text = _multiTouchInfo;
    }
}
