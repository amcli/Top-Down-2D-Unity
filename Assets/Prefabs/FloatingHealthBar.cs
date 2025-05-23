using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider sl;

    // Start is called before the first frame update
    void Awake()
    {
        sl = GetComponent<Slider>();
    }

    public void SetMaxHealth(float hp)
    {
        sl.maxValue = hp;
        sl.value = hp;
    }

    public void SetHealth(float hp)
    {
        sl.value = hp;
    }
}
