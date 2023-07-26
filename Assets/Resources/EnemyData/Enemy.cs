using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemytemp{
[System.Serializable]
public class Enemy
{
    public int id;//enemyID
    public string enemyname;//name
    public string enemytype;//enemytype(normal/raid)
    public int hp;//HP
    public int attack;//attack power
    public int guard;//guard power
    public int point;//get skill
}

//参考 https://gametech.vatchlog.com/2022/12/14/unity-db-create/
}//https://stackoverflow.com/questions/63492526/c-sharp-unity-the-namespace-global-namespace-already-contains-a-definition-f