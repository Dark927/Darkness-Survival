using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] GameObject panel;

    [SerializeField] List<UpgradeButton> upgradeButtons;

    private void Start()
    {
        HideButtons();
    }

    public void OpenPanel(List<UpgradeData> upgradeDatas)
    {
        Clean();

        GrayscaleEffect.instance.ToggleGrayscale(true);
        PauseManager.instance.PauseGame();
        
        panel.SetActive(true);

        for (int i = 0; i < upgradeDatas.Count; ++i)
        {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtons[i].Set(upgradeDatas[i]);
        }
    }

    public void Clean()
    {
        for(int i = 0; i < upgradeButtons.Count; ++i)
        {
            upgradeButtons[i].Clean();
        }
    }

    public void Upgrade(int pressedButtonID)
    {
        GameManager.instance.playerTransform.GetComponent<Level>().Upgrade(pressedButtonID);
        ClosePanel();
    }

    public void ClosePanel()
    {
        HideButtons();

        PauseManager.instance.UnPauseGame();
        panel.SetActive(false);
        GrayscaleEffect.instance.ToggleGrayscale(false);
    }

    private void HideButtons()
    {
        for (int i = 0; i < upgradeButtons.Count; ++i)
        {
            upgradeButtons[i].gameObject.SetActive(false);
        }
    }
}
