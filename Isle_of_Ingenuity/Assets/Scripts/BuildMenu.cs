using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
using TMPro;

public class BuildMenu : MonoBehaviour
{
    //[SerializeField] Text houseCostText;
    [SerializeField] TMP_Text houseCostText;

    private MaterialManager MaterialManager;

    

    void OnEnable() {
        MaterialManager = FindAnyObjectByType<MaterialManager>();
        UpdateCostDisplay();
    }

    void UpdateCostDisplay() {
        var matNum = MaterialManager.getAllMatNum();
        int numWood = matNum.Item1;
        int numStone = matNum.Item2;
        int numPlank = matNum.Item3;
        int numBrick = matNum.Item4;

        int requiredWood = MaterialManager.houseCostWood;
        int requiredStone = MaterialManager.houseCostStone;
        
        string woodColor = numWood >= requiredWood ? "green" : "red";
        string stoneColor = numStone >= requiredStone ? "green" : "red";



        // houseCostText.text =
        //     $"<sprite name=\"wood\"> Wood: <color={woodColor}>{numWood}</color> / {requiredWood}\n" +
        //     $"<sprite name=\"stone\"> Stone: <color={stoneColor}>{numStone}</color> / {requiredStone}";
        houseCostText.text =
            $"Wood: <color={woodColor}>{numWood}</color> / {requiredWood}\n" +
            $"Stone: <color={stoneColor}>{numStone}</color> / {requiredStone}";

    
    }
}
