using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIminiMap : MonoBehaviour
{
    [SerializeField] private Camera minimapCamrea;
    [SerializeField] private float zoomMin = 1;
    [SerializeField] private float zoomMax = 30;
    [SerializeField] private float zoomOneStep = 1;
    [SerializeField] private TextMeshPro textMapname;
    // Start is called before the first frame update
    private void Awake()
    {
        textMapname.text = SceneManager.GetActiveScene().name;
    }
    public void ZoomIn()
    {
        minimapCamrea.orthographicSize = Mathf.Max(minimapCamrea.orthographicSize - zoomOneStep, zoomMin);
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
