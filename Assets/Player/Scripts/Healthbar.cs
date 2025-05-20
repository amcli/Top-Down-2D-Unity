using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider sl;

    // Start is called before the first frame update
    void Start()
    {
        sl = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {

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

