using UnityEngine;
using UnityEngine.UI;

public class qualityManager : MonoBehaviour
{
    public Dropdown qualityDropdown;

    public void setQuality()
    {
        switch (qualityDropdown.value)
        {
            case 0:
                gameParameters.setQualityLevelToLow();
                break;
            case 1:
                gameParameters.setQualityLevelToNormal();
                break;
            case 2:
                gameParameters.setQualityLevelToHigh();
                break;
            default:
                gameParameters.setQualityLevelToHigh();
                break;
        }
    }
}
