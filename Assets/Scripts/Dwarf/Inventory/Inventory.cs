using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int[] amountsOfItemsInSlots = new int[6];
    [SerializeField] private TextMeshProUGUI[] textNumbersOfSlots;


    public void ChangeAmountOfItemInInventory(int blockId, int amount)
    {
        amountsOfItemsInSlots[blockId] += amount;
        textNumbersOfSlots[blockId].text = amountsOfItemsInSlots[blockId].ToString();
    }

    public int GetAmountOfItem(int blockId)
    {
        return amountsOfItemsInSlots[blockId];
    }
}