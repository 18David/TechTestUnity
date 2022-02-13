using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _startPage;

    [SerializeField]
    private GameObject _listPage;

    [SerializeField]
    private GameObject _scenePage;

    [SerializeField]
    private GameObject _loadingPage;

    [SerializeField]
    private GameObject _lookAroundPage;

    [SerializeField]
    private GameObject _prefabObj;

    [SerializeField]
    private Transform _parent;

    
    [SerializeField]
    private TextMeshProUGUI _loadingName;

    [SerializeField]
    private Slider _loadingSlider;

    public static UiManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

    }

    // Start is called before the first frame update
    void Start()
    {
        
        _startPage.SetActive(true);
        _listPage.SetActive(false);
        _scenePage.SetActive(false);
        _loadingPage.SetActive(false);
        _lookAroundPage.SetActive(false);
    }

    public void ListPage()
    {
        _startPage.SetActive(false);
        _listPage.SetActive(true);
        _scenePage.SetActive(false);
        _loadingPage.SetActive(false);
        _lookAroundPage.SetActive(false);
    }

    public void ScenePage()
    {
        _startPage.SetActive(false);
        _listPage.SetActive(false);
        _scenePage.SetActive(true);
        _loadingPage.SetActive(false);
        _lookAroundPage.SetActive(false);

    }

    public void AddObj(ObjectToDonwload obj)
    {
        GameObject tmp = Instantiate(_prefabObj, _parent);
        UiObj ui = tmp.GetComponent<UiObj>();
        if(ui != null) 
        {
            ui.Init(obj.Name, obj.Url);
        }
    }

    public void LoadObj(string name)
    {
        _startPage.SetActive(false);
        _listPage.SetActive(false);
        _scenePage.SetActive(false);
        _loadingPage.SetActive(true);
        _lookAroundPage.SetActive(false);

        _loadingName.text = name;
        _loadingSlider.value = 0f;

    }
    public void LookAround()
    {
        _startPage.SetActive(false);
        _listPage.SetActive(false);
        _scenePage.SetActive(false);
        _loadingPage.SetActive(false);
        _lookAroundPage.SetActive(true);
    }

    public void Loading(float value)
    {
        _loadingSlider.value = value;
    }

}
