using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiObj : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _name;

    private string _url;

    public void Init(string name, string url)
    {
        _name.text = name;
        _url = url;
    }

    public void LoadObj()
    {
        GameManager.Instance.LoadObj(_name.text, _url);
    }
}