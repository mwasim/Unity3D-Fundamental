using UnityEngine;
using UnityEngine.UI;

public class VirtualDPad : MonoBehaviour
{
    [SerializeField]
    private Text _directionText;
    private Touch _theTouch;
    private Vector2 _touchStartPosition, _touchEndPosition;
    private string _direction;


    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            _theTouch = Input.GetTouch(0);

            if (_theTouch.phase == TouchPhase.Began)
            {
                _touchStartPosition = _theTouch.position;
            }
            else if (_theTouch.phase == TouchPhase.Moved || _theTouch.phase == TouchPhase.Ended)
            {
                _touchEndPosition = _theTouch.position;

                var x = _touchEndPosition.x - _touchStartPosition.x;
                var y = _touchEndPosition.y - _touchStartPosition.y;

                if (Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0)
                {
                    _direction = "Tapped";
                }
                else if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    _direction = x > 0 ? "Right" : "Left";
                }
                else
                {
                    _direction = y > 0 ? "Up" : "Down";
                }
            }
        }

        _directionText.text = _direction;
    }
}
