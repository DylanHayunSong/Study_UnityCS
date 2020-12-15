using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class DoorExample : MonoBehaviour
{
    public GameObject[] sides;
    public GameObject[] bttops;
    public GameObject[] corners;
    public GameObject center;

    public float width;
    public float height;
    public float depth;


    float sideInterval;

    float btTopInterval;

    [Range(1, 5)]
    public float intervalMult;

    // Start is called before the first frame update
    void Start ()
    {

    }


    // Update is called once per frame
    void Update ()
    {
        Calc();
    }

    void Calc()
    {
        sideInterval = center.transform.localScale.x / 2;
        btTopInterval = center.transform.localScale.y / 2;

        for (int i = 0; i < sides.Length; i++)
        {
            Vector3 dir = sides[i].transform.localPosition - center.transform.localPosition;
            float dist = Vector3.Distance(sides[i].transform.localPosition - dir.normalized * sides[i].transform.localScale.x / 2, center.transform.localPosition);
            float offset = sideInterval - dist;
            Vector3 newScale = new Vector3(sides[i].transform.localScale.x, center.transform.localScale.y, center.transform.localScale.z);
            Vector3 newPos = sides[i].transform.localPosition + offset * dir.normalized;

            sides[i].transform.localScale = newScale;
            sides[i].transform.localPosition = newPos;
        }
        sides[1].transform.localScale = sides[0].transform.localScale;
        for (int i = 0; i < bttops.Length; i++)
        {
            Vector3 dir = bttops[i].transform.localPosition - center.transform.localPosition;
            float dist = Vector3.Distance(bttops[i].transform.localPosition - dir.normalized * bttops[i].transform.localScale.y / 2, center.transform.localPosition);
            float offset = btTopInterval - dist;
            Vector3 newScale = new Vector3(center.transform.localScale.x, bttops[i].transform.localScale.y, center.transform.localScale.z);
            Vector3 newPos = bttops[i].transform.localPosition + offset * dir.normalized;

            bttops[i].transform.localScale = newScale;
            bttops[i].transform.localPosition = newPos;
        }
        bttops[1].transform.localScale = bttops[0].transform.localScale;

        for (int i = 0; i < corners.Length; i++)
        {
            Vector3 dir = corners[i].transform.localPosition - center.transform.localPosition;
            float xDist = Mathf.Abs(dir.x) - corners[i].transform.localScale.x / 2;
            float yDist = Mathf.Abs(dir.y) - corners[i].transform.localScale.y / 2;
            float xOffset = sideInterval - xDist;
            float yOffset = btTopInterval - yDist;

            Vector3 newPos = new Vector3(0, 0, center.transform.localPosition.z);
            newPos.y = (corners[i].transform.localPosition + yOffset * dir.normalized).y;
            newPos.x = (corners[i].transform.localPosition + xOffset * dir.normalized).x;

            Vector3 newScale = new Vector3(sides[0].transform.localScale.x, bttops[0].transform.localScale.y, center.transform.localScale.z);
            corners[i].transform.localPosition = newPos;
            corners[i].transform.localScale = newScale;
            
        }
    }
}
