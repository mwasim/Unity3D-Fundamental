using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedScrollingText : MonoBehaviour, IAttackable
{
    public ScrollingText scrollingTextPrefab;
    public Color textColor;

    public void OnAttack(GameObject attacker, Attack attack)
    {
        var text = attack.Damage.ToString();

        var scrollingText = Instantiate(scrollingTextPrefab, transform.position, Quaternion.identity);
        scrollingText.SetText(text);
        scrollingText.SetColor(textColor);
    }   
}
