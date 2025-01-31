using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WaveEnemyData : MonoBehaviour
{
    //�̱���
    List<stEnemyData> _lstEnemyData = new List<stEnemyData>();
    public List<stEnemyData> LstEnemyData
    {
        get
        {
            if (_lstEnemyData.Count == 0)
                ReadEnemyData();
            return _lstEnemyData;
        }
    }

    void ReadEnemyData()
    {
        TextAsset textFile = Resources.Load("Datas/EnemyData") as TextAsset;
        string[] data = null;
        using (StringReader sr = new StringReader(textFile.text))
        {
            string baseData = sr.ReadToEnd();
            data = baseData.Split("\r\n");
        }
        if (data.Length < 2)
            return;

        for (int i = 1; i < data.Length; i++)
        {
            var values = data[i].Split(",");
            if (values.Length == 0 || string.IsNullOrEmpty(values[0]))
                continue;
            stEnemyData temp = new stEnemyData();
            temp.INDEX = int.Parse(values[0]);
            temp.WAVE = int.Parse(values[1]);
            temp.ZOMBIE = int.Parse(values[2]);
            temp.RANGEZOMBIE = int.Parse(values[3]);
            temp.EXPLSIONZOMBIE = int.Parse(values[4]);
            temp.GHOUL = int.Parse(values[5]);
            temp.RAPTOR = int.Parse(values[6]);
            temp.PACHY = int.Parse(values[7]);
            temp.BOSS = int.Parse(values[8]);
            temp.TOTALENEMY = int.Parse(values[9]);

            _lstEnemyData.Add(temp);
        }
    }
}

public struct stEnemyData
{
    public int INDEX;
    public int WAVE;
    public int ZOMBIE;
    public int RANGEZOMBIE;
    public int EXPLSIONZOMBIE;
    public int GHOUL;
    public int RAPTOR;
    public int PACHY;
    public int BOSS;
    public int TOTALENEMY;
}