using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
//play button should set deaths to zero and title screen should have record deaths
public class player : MonoBehaviour
{
    public Transform transform;
    public Rigidbody2D rigidBody;
    public float speed = 0.5f;
    private bool canMove = true;
    public int deaths = 0;
    private Vector3 respawnPosition;
    public int level = 0;
    public TextMeshProUGUI deathCounter;
    public static player Instance;
    public int coinCount = 0;
    private int coinGoal = 0;
    private TextMeshProUGUI coinText;
    List<int> records = new List<int>();
    void Start()
    {
        if (Instance != null) {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (SceneManager.GetActiveScene().name == "level 1") {
            level = 0;
        }
        if (SceneManager.GetActiveScene().name == "level 2") {
            level = 1;
        }
        if (SceneManager.GetActiveScene().name == "level 3") {
            level = 2;
        }
        if (SceneManager.GetActiveScene().name == "level 4") {
            level = 3;
        }

        coinCount = 0;
        foreach (TextMeshProUGUI i in UnityEngine.Object.FindObjectsOfType<TextMeshProUGUI>()) {
            if (i.text.StartsWith("De") || i.text.StartsWith("co")) i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
            if (i.text.StartsWith("co")) {
                i.text = "coins: " + coinCount + "/" + coinGoal;
                coinText = i;
            }
        }

        handleLevels();
        print(SceneManager.GetActiveScene().name);
    }

    void OnSceneLoaded(Scene s, LoadSceneMode m) {
        deathCounter.text = "Deaths: " + deaths;
        handleLevels();
        coinCount = 0;
        foreach (TextMeshProUGUI i in UnityEngine.Object.FindObjectsOfType<TextMeshProUGUI>()) {
            i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        }
        coinText.text = "coins: " + coinCount + "/" + coinGoal;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("g")) {
            backlevel();
        }
        if (Input.GetKey("h")) {
            forwardlevel();
        }
        if (Input.GetKey(KeyCode.Escape)) {
            SceneManager.LoadScene("title screen");
        }
    }

    void FixedUpdate() {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        if (canMove) rigidBody.MovePosition(rigidBody.position + movement * speed * Time.deltaTime);
    }

    void handleLevels() {
        if (level == 0) {
            transform.position = new Vector3(-2.11f, -1.73f, 0);
            respawnPosition = transform.position;
            coinGoal = 0;
        }
        if (level == 1) {
            transform.position = new Vector3(-1.78f, 9.31f, 0);
            respawnPosition = transform.position;
            coinGoal = 9;
        }
        if (level == 2) {
            transform.position = new Vector3(2.64f, -1.1f, 0);
            respawnPosition = transform.position;
            coinGoal = 1;
        }
        if (level == 3) {
            transform.position = new Vector3(2.63f, -8.58f, 0);
            respawnPosition = transform.position;
            coinGoal = 3;
        }
    }

    void forwardlevel() {
        if (level < 3) {
            level++;
            SceneManager.LoadScene(level);
        } else {
            records.Add(deaths);
            SceneManager.LoadScene("title screen");
        }
    }

    void backlevel() {
        if (level > 0) {
            level--;
            SceneManager.LoadScene(level);
        }
    }

    void OnTriggerEnter2D(Collider2D target) {
        if (target.tag == "Goal") {
            if (coinCount == coinGoal) forwardlevel();
        }
        if (target.tag == "Ball") {
            if (level < 4) StartCoroutine(die());
        }
        if (target.tag == "Coin") {
            coinCount++;
            StartCoroutine(flash(coinText, "g"));
            coinText.text = "coins: " + coinCount + "/" + coinGoal;
            target.gameObject.GetComponent<coin>().collect();
        }
    }

    IEnumerator die() { 
        
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject c in coins) {
            StartCoroutine(c.GetComponent<coin>().enable());
        }
        coinCount = 0;
        coinText.text = "coins: " + coinCount + "/" + coinGoal;

        deaths++;
        StartCoroutine(flash(deathCounter, "r"));
        deathCounter.text = "Deaths: " + deaths;  
        canMove = false;
        GetComponent<BoxCollider2D>().enabled = false;

        //fade
        float opacity = 0.95f;
        yield return new WaitForSeconds(0.2f);
        while (opacity > 0) {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, opacity);
            opacity -= 0.05f;
            yield return new WaitForSeconds(0.025f);
        }

        transform.position = respawnPosition;

        //appear
        opacity = 0.05f;
        while (opacity <= 1.05f) {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, opacity);
            opacity += 0.05f;
            yield return new WaitForSeconds(0.025f);
        }

        canMove = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    IEnumerator flash(TextMeshProUGUI tx, string color) {
        float r = 0.95f;
        if (color == "r") tx.color = new Color(1f, 0, 0, 1);
        if (color == "g") tx.color = new Color(0, 1f, 0, 1);
        yield return new WaitForSeconds(0.1f);
        if (color == "r") {
            while (tx.color.r > 0) {
                tx.color = new Color(r, 0, 0, 1);
                r -= 0.05f;
                yield return new WaitForSeconds(0.025f);
            }
        }
        if (color == "g") {
            while (tx.color.g > 0) {
                tx.color = new Color(0, r, 0, 1);
                r -= 0.05f;
                yield return new WaitForSeconds(0.025f);
            }
        }
        yield return new WaitForSeconds(0.025f);
    }

    public int getRecord() {
        if (records.Count > 0) {
            int min = records[0];
            for (int i = 1; i < records.Count; i++) {
                int cur = records[i];
                if (min > cur) min = cur;
            }
            return min;
        }
        return 0;
    }
}