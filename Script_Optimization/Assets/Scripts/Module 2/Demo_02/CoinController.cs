using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("ChangeColor", 0, 1);
        StartCoroutine(ChangeColorCoroutine());
    }

    private void ChangeColor()
    {
        GetComponent<SpriteRenderer>().color = GetRandomColor();
    }

    private IEnumerator ChangeColorCoroutine()
    {
        while (true)
        {
            GetComponent<SpriteRenderer>().color = GetRandomColor();
            yield return new WaitForSeconds(1);
        }
    }

    private Color GetRandomColor()
    {
        return new Color
            (
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f)
            );
    }
}
