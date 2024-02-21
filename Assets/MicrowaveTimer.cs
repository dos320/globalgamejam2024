using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrowaveTimer : MonoBehaviour
{
    public float cutoffTime;
    public float realTimer;
    
    [HideInInspector] public bool bCutoffTriggered = false;
    [HideInInspector] public bool bStartTimer = false;
    [HideInInspector] public int iOvercookedAmount = 0;
    public float fTimerOverflowAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bStartTimer)
        {
            realTimer += Time.deltaTime;
            if (realTimer >= cutoffTime && !bCutoffTriggered)
            {
                bCutoffTriggered = true;
                // make ingredients inside "READY" (set a tag)
                // also play a sound or something to indicate

            }
            else if (bCutoffTriggered)
            {
                fTimerOverflowAmount = realTimer - cutoffTime;
                if (fTimerOverflowAmount >= 5 && fTimerOverflowAmount < 10)
                {
                    iOvercookedAmount++; // 1
                }else if (fTimerOverflowAmount >= 10)
                {
                    iOvercookedAmount++; // 2
                }
            }
        }
        else
        {
            // reset timer
            realTimer = 0f;
        } 
    }

}
