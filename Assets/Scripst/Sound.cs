using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sound : MonoBehaviour
{
    // Start is called before the first frame update
    public void SoundOption()
    {
        Debug.Log("���� �ɼ����� �Ѿ");

        SceneManager.LoadScene("Sound");
    }
}
