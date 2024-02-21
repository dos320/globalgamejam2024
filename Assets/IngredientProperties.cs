using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientProperties : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeCooked;
    public int iOvercookedAmount;
    public bool bStartTimer;
    public int cutoffTime;
    public bool bCutoffTriggered = false;
    public float fTimerOverflowAmount;

    private bool bOvercookedAmountIncreased = false;

    private void FixedUpdate()
    {
        if (bStartTimer)
        {
            timeCooked += Time.deltaTime;
            if (timeCooked >= cutoffTime && !bCutoffTriggered)
            {
                bCutoffTriggered = true;
                // make ingredients inside "READY" (set a tag)
                // also play a sound or something to indicate

            }
            else if (bCutoffTriggered)
            {
                fTimerOverflowAmount = timeCooked - cutoffTime;
                if (fTimerOverflowAmount >= 5 && fTimerOverflowAmount < 10)
                {
                    iOvercookedAmount = 1;
                }
                else if (fTimerOverflowAmount >= 10)
                {
                    iOvercookedAmount = 2;
                }
            }
        }
    }
}
