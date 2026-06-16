using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private UiViewer _uiViewer;

    private int _heal;

    public void SetHeal(int heal)
    {
        _heal = heal;

        _uiViewer.OnAidMeatGot(_heal);
    }

    public void AddHeal(int heal)
    {
        _heal += heal;

        _uiViewer.OnAidMeatGot(_heal);
    }       

    public int GetHeal() =>
        _heal;
}

