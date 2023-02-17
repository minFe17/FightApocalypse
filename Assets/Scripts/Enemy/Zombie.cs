using System.Collections;
using UnityEngine;

public class Zombie : Enemy
{
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        LookTarget();
        Move();
    }

    public void HideEnemy() //좀비 애니메이션 끝나면 실행
    {
        gameObject.SetActive(false);
    }
}
