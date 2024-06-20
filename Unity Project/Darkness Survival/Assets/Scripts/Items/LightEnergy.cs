using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LightEnergy : MonoBehaviour
{
    [SerializeField] DataContainer data;
    [SerializeField] TMPro.TextMeshProUGUI energyCountText;

    private void Start()
    {
        data.LoadEnergy();
        energyCountText.text = data.energyAcquired.ToString();
    }

    public void Add(int amount)
    {
        data.energyAcquired += amount;
        energyCountText.text = data.energyAcquired.ToString();

        data.SaveEnergy();

#if UNITY_EDITOR
        EditorUtility.SetDirty(data);
#endif

    }
}
