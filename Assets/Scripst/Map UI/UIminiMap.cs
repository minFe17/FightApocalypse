using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIminiMap : MonoBehaviour
{
    [SerializeField] private float zoomMin = 1;
    [SerializeField] private float zoomMax = 30;
    [SerializeField] private float zoomOneStep = 1;
    [SerializeField] private TextMeshProUGUI textMapname;

    Camera _minimapCamera;
    public Camera MiniMapCamera { set { _minimapCamera = value; } }

    private void Awake()
    {
        textMapname.text = SceneManager.GetActiveScene().name;
    }
    public void ZoomIn()
    {
        _minimapCamera.orthographicSize = Mathf.Max(_minimapCamera.orthographicSize - zoomOneStep, zoomMin);
    }

    public void ZoomOut()
    {
        _minimapCamera.orthographicSize = Mathf.Min(_minimapCamera.orthographicSize + zoomOneStep, zoomMax);
    }

    void Start()
    {
        _minimapCamera.orthographicSize = 10;
    }
}
