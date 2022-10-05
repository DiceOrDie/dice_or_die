// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceSlot : MonoBehaviour
{
    public Image icon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVisible()
    {
        icon.enabled = true;
    }//SetVisable

    public void SetInvisible()
    {
        icon.enabled = false;
    }//SetInvisible
}
