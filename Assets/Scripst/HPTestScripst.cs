using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class HPTestScripst : MonoBehaviour
{

    [SerializeField] GameObject M_goPrefab = null;
    List<Transform>M_ObjactList = new List<Transform>();    
    List<GameObject> M_hpBatList = new List<GameObject>();  

    Camera m_cam = null;    
    // Start is called before the first frame update
    void Start()
    {
        m_cam = Camera.main;

        GameObject[] t_object = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < t_object.Length; i++)
        {
            M_ObjactList.Add(t_object[i].transform);    
            GameObject t_hpbar = Instantiate(M_goPrefab, t_object[i].transform.position, Quaternion.identity,transform);
            M_hpBatList.Add(t_hpbar);   
        }
     }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i< M_ObjactList.Count; i++)
        {
            M_hpBatList[i].transform.position = m_cam.WorldToScreenPoint(M_ObjactList[i].position + new Vector3(0, 1.5f, 0));
        }
    }
}
