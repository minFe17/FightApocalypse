using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sound : MonoBehaviour
{
    [SerializeField] GameObject _soundoption;
    // Start is called before the first frame update
    public void SoundOption()
    {
        Debug.Log("���� �ɼ����� �Ѿ");

        _soundoption.SetActive(true);

    }
}
