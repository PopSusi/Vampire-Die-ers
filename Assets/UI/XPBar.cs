using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.instance.XPUpdateUI += updateXP; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void updateXP(int xp, int level)
    {

    }
}
