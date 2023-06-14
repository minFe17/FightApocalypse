using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // ΩÃ±€≈Ê
    SoundController _soundController;

    public SoundController SoundController 
    { 
        get
        { 
            if(_soundController == null)
            {
                GameObject temp = Resources.Load("Prefabs/SoundController") as GameObject;
                GameObject soundController = Instantiate(temp);
                _soundController = soundController.GetComponent<SoundController>();
            }
            return _soundController;
        }
    }
}
