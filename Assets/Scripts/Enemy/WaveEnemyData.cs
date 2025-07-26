using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WaveEnemyData : MonoBehaviour
{
    //ΩÃ±€≈Ê
    List<EnemySpawnData> _lstEnemyData = new List<EnemySpawnData>();
    public List<EnemySpawnData> LstEnemyData
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
            EnemySpawnData temp = new EnemySpawnData();
            temp.Index = int.Parse(values[0]);
            temp.Wave = int.Parse(values[1]);
            temp.Zombie = int.Parse(values[2]);
            temp.RangeZombie = int.Parse(values[3]);
            temp.ExplosionZombie = int.Parse(values[4]);
            temp.Ghoul = int.Parse(values[5]);
            temp.Raptor = int.Parse(values[6]);
            temp.Pachy = int.Parse(values[7]);
            temp.Boss = int.Parse(values[8]);
            temp.TotalEnemy = int.Parse(values[9]);

            _lstEnemyData.Add(temp);
        }
    }
}

public struct EnemySpawnData
{
    public int Index;
    public int Wave;
    public int Zombie;
    public int RangeZombie;
    public int ExplosionZombie;
    public int Ghoul;
    public int Raptor;
    public int Pachy;
    public int Boss;
    public int TotalEnemy;
}