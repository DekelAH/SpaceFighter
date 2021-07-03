using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed = 0.5f;

    Material myMaterial;
    Vector2 offSet;

    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material; // Getting the material of the background
        offSet = new Vector2(0, backgroundScrollSpeed); // Setting backgroundScrollSpeed variable as the Y axis movement to the Vector2 offset
    }

    // Update is called once per frame
    void Update()
    {
        // Getting access to the background image offset + the offSet variable * Time.deltaTime(frame independent)
        myMaterial.mainTextureOffset += offSet * Time.deltaTime;
    }
}
