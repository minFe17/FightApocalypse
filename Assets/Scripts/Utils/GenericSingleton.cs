using UnityEngine;

namespace Utils
{
    // MonoBehaviour�� ������� �� �̱��� ������ ���ʸ����� ����
    public class GenericSingleton<T> where T : MonoBehaviour
    {
        // �̱��� �ν��Ͻ��� �����ϴ� ���� ����
        private static T _instance;

        // �ܺο��� ���� ������ �ν��Ͻ� ������Ƽ
        public static T Instance
        {
            get
            {
                // �ν��Ͻ��� ������ �� GameObject�� �����Ͽ� ������Ʈ�� ����
                if (_instance == null)
                {
                    GameObject temp = new GameObject(typeof(T).Name);
                    _instance = temp.AddComponent<T>();

                    // ���� ��ȯ�Ǿ �ı����� �ʵ��� ����
                    Object.DontDestroyOnLoad(temp);
                }
                return _instance;
            }
        }
    }
}
