using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class canvas : MonoBehaviour
{
    public RectTransform transform;
    public static canvas Instance;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null) {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene s, LoadSceneMode m) {
        Canvas canvas = GetComponent<Canvas>();
        Camera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        canvas.worldCamera = camera;
        camera.enabled = true;

        if (GameObject.Find("player").GetComponent<player>().level == 0) {
            transform.position = new Vector3(0, 0);
        }
        if (GameObject.Find("player").GetComponent<player>().level == 1) {
            transform.position = new Vector3(0, -10.06f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
