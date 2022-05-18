using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestItemUI : MonoBehaviour
{
    [SerializeField] private Image targetItemImg;
    [SerializeField] private TextMeshProUGUI amountTMP;
    [SerializeField] private Image completedCheck;
    private bool _completed;
    public void Setup(QuestItem item)
    {
        completedCheck.enabled = false;
        targetItemImg.enabled = true;
        amountTMP.enabled = true;
        _completed = false;
        
        targetItemImg.color = TileUtils.TileTypeToColor(item.TypeToDestroy);
        amountTMP.text = item.countToDestroy.ToString();
    } 
    public void UpdateAmount(int newAmount)
    {
        amountTMP.text = newAmount.ToString();
    } 
    public void Complete()
    {
        if(_completed) return;
        _completed = true;
        amountTMP.enabled = false;
        completedCheck.enabled = true;
    } 
}
