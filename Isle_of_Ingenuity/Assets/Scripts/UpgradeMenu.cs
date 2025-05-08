using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
using TMPro;

public class UpgradeMenu : MonoBehaviour
{
    //[SerializeField] Text houseCostText;
    [SerializeField] TMP_Text dockCostText;
    [SerializeField] TMP_Text boatCostText;

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


        // Dock Text
        int requiredDockWood = MaterialManager.dockCostWood;
        int requiredDockStone = MaterialManager.dockCostStone;
        int requiredDockPlank = MaterialManager.dockCostPlank;
        int requiredDockBrick = MaterialManager.dockCostBrick;
        
        string dockWoodColor = numWood >= requiredDockWood ? "green" : "red";
        string dockStoneColor = numStone >= requiredDockStone ? "green" : "red";
        string dockPlankColor = numPlank >= requiredDockPlank ? "green" : "red";
        string dockBrickColor = numBrick >= requiredDockBrick ? "green" : "red";

        dockCostText.text =
            $"Wood: <color={dockWoodColor}>{numWood}</color> / {requiredDockWood}\n" +
            $"Stone: <color={dockStoneColor}>{numStone}</color> / {requiredDockStone}\n" +
            $"Planks: <color={dockPlankColor}>{numStone}</color> / {requiredDockPlank}\n" +
            $"Bricks: <color={dockBrickColor}>{numStone}</color> / {requiredDockBrick}";

        int requiredBoatWood = MaterialManager.dockCostWood;
        int requiredBoatStone = MaterialManager.dockCostStone;
        int requiredBoatPlank = MaterialManager.dockCostPlank;
        int requiredBoatBrick = MaterialManager.dockCostBrick;
        
        string boatWoodColor = numWood >= requiredBoatWood ? "green" : "red";
        string boatStoneColor = numStone >= requiredBoatStone ? "green" : "red";
        string boatPlankColor = numPlank >= requiredBoatPlank ? "green" : "red";
        string boatBrickColor = numBrick >= requiredBoatBrick ? "green" : "red";

        boatCostText.text =
            $"Wood: <color={boatWoodColor}>{numWood}</color> / {requiredBoatWood}\n" +
            $"Stone: <color={boatStoneColor}>{numStone}</color> / {requiredBoatStone}\n" +
            $"Planks: <color={boatPlankColor}>{numStone}</color> / {requiredBoatPlank}\n" +
            $"Bricks: <color={boatBrickColor}>{numStone}</color> / {requiredBoatBrick}";

    
    }
}
