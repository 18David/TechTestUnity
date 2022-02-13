using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private ObjectToDonwload[] _objects;

    //private List<GameObject> _spawned;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        //_spawned = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (ObjectToDonwload obj in _objects)
        {
            UiManager.Instance.AddObj(obj);
        }
    }

    

    public void LoadObj(string name, string url)
    {
        StartCoroutine(FindARPointAndLoadObj(name,url));
        
    }

    private IEnumerator FindARPointAndLoadObj(string name, string url)
    {
        UiManager.Instance.LookAround();
        Transform t = null;

        while(t == null)
        {
            t = ARManager.Instance.CreateARPoint();
            yield return new WaitForSeconds(0.5f);
        }

        UiManager.Instance.LoadObj(name);
        GlbLoader.Instance.DownloadFile(url,t);
    }


    public void Loaded(GameObject obj)
    {
        //_spawned.Add(obj);
        UiManager.Instance.ScenePage();
        
    }

}
