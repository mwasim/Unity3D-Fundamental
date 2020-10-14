using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingText : MonoBehaviour
{
    [SerializeField] private float _duration = 1f;
    [SerializeField] private float _speed;

    private TextMesh _textMesh;
    private float _startTime;

    private void Awake()
    {
        _textMesh = GetComponent<TextMesh>();
        _startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - _startTime < _duration)
        {
            //continue to scroll up
            transform.LookAt(Camera.main.transform); //text object looking at the camera

            //translate up
            transform.Translate(Vector3.up * _speed * Time.deltaTime);            
        }
        else
        {
            Destroy(gameObject);            
        }
    }

    public void SetText(string text)
    {
        _textMesh.text = text;
    }

    public void SetColor(Color color)
    {
        _textMesh.color = color;
    }
}
