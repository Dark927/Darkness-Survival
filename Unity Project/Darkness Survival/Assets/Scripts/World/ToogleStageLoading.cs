using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToogleStageLoading : MonoBehaviour
{
    public void ToogleStageLoad()
    {
        SimpleStageLoader.instance.isStageLoading = !SimpleStageLoader.instance.isStageLoading;
    }
}
