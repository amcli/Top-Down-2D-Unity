using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public Slider sl;
    public Player player;


    // Start is called before the first frame update
    void Awake()
    {
        sl = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStamina(player.currStamina);
    }

    public void SetMaxStamina(float stamina)
    {
        sl.maxValue = stamina;
        sl.value = stamina;
    }

    public void UpdateStamina(float stamina)
    {
        sl.value = stamina;
    }

}
