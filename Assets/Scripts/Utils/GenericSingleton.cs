using UnityEngine;

namespace Utils
{
    // MonoBehaviour를 기반으로 한 싱글턴 패턴을 제너릭으로 구현
    public class GenericSingleton<T> where T : MonoBehaviour
    {
        // 싱글턴 인스턴스를 저장하는 정적 변수
        private static T _instance;

        // 외부에서 접근 가능한 인스턴스 프로퍼티
        public static T Instance
        {
            get
            {
                // 인스턴스가 없으면 새 GameObject를 생성하여 컴포넌트를 붙임
                if (_instance == null)
                {
                    GameObject temp = new GameObject(typeof(T).Name);
                    _instance = temp.AddComponent<T>();

                    // 씬이 전환되어도 파괴되지 않도록 설정
                    Object.DontDestroyOnLoad(temp);
                }
                return _instance;
            }
        }
    }
}
