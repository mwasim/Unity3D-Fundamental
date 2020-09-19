using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchPhaseDisplay : MonoBehaviour
{
    [SerializeField]
    private Text _phaseDisplayText;

    private Touch theTouch;
    private float _timeTouchEnded;
    private float _displayTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            theTouch = Input.GetTouch(0);
            _phaseDisplayText.text = theTouch.phase.ToString();

            if (theTouch.phase == TouchPhase.Ended)
            {
                _timeTouchEnded = Time.time;
            }
        }
        else if (Time.time - _timeTouchEnded > _displayTime)
        {
            _phaseDisplayText.text = string.Empty;
        }

    }
}
