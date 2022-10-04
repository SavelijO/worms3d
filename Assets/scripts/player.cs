using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class player : MonoBehaviour
{
    public Color color;
    public int unitCount;
    public List<unit> unitArr = new List<unit>();
    public unit currentUnit;
    public int currentUnitIndex = 0;
    public GameObject unitPrefab;
    private GameObject tempGameObject;

    void Start()
    {
        for (int i = 0; i < unitCount; i++)
        {
            tempGameObject = Instantiate(unitPrefab, new Vector3(0,0,0), Quaternion.identity); ;
            tempGameObject.transform.position = this.transform.position;
            tempGameObject.transform.position += new Vector3(0+i*2, 0, 0+i*2);
            tempGameObject.name = "unit " + (i + 1).ToString();
            unitArr.Add(tempGameObject.GetComponent<unit>());
            unitArr[i].color = color;
        }
        currentUnit = unitArr[currentUnitIndex];

    }

    void Update()
    {
        unitCount = 0;
        foreach(unit unit in unitArr)
        {
            if(unit != null)
            {
                unitCount += 1;
            }
        }

        if(unitCount == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void switchUnit()
    {
        if(currentUnit != null)
        {
            currentUnit.GetComponent<MeshCollider>().enabled = true;
            currentUnit.GetComponent<Rigidbody>().isKinematic = false;
            currentUnit.GetComponent<MeshRenderer>().enabled = true;
            currentUnitIndex++;
            currentUnitIndex %= unitCount;
            currentUnit = unitArr[currentUnitIndex];
        }
        else
        {
            for (int i = 0; i < unitArr.Count; i++)
            {
                currentUnitIndex++;
                currentUnitIndex %= unitCount;
                currentUnit = unitArr[currentUnitIndex];
                
                if(currentUnit != null)
                {
                    break;
                }

            }
        }
    }

}
