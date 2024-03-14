using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void collect() {
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public IEnumerator enable() {

        SpriteRenderer s = GetComponent<SpriteRenderer>();

        bool previouslyEnabled = s.enabled;
        GetComponent<CircleCollider2D>().enabled = true;
        s.enabled = true;

        if (!previouslyEnabled) {
            float opacity = 0.05f;
            while (opacity <= 1.05f) {
                s.color = new Color(1, 1, 1, opacity);
                opacity += 0.05f;
                yield return new WaitForSeconds(0.025f);
            }
        }
        

    }
}
