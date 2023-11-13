using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterParameter : MonoBehaviour
{
    [SerializeField]
    private Slider heat, stamina, armor;

    [SerializeField]
    private Text lv;

    
    public void HeatIndex(float _maxheat, float _heat)
    {
        heat.maxValue = _maxheat;
        heat.value = _heat;
    }
    public void StaminaIndex(float _maxstamina,float _stamina)      
    {
        stamina.maxValue = _maxstamina;
        stamina.value = _stamina;   
    }    

    public void Armor(float _maxarmor, float _armor)
    {
        armor.maxValue = _maxarmor;
        armor.value = _armor;
    }


}
