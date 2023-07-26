using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTest : MonoBehaviour
{
    [SerializeField] private CharacterStatus hero;
    [SerializeField] private CharacterStatus warrior;
    [SerializeField] private CharacterStatus mage;
    [SerializeField] private CharacterStatus priest;

    // Start is called before the first frame update
    void Start()
    {
        // hero.SetLevel(100);
        // warrior.SetLevel(100);
        // mage.SetLevel(100);
        // priest.SetLevel(100);
        // PlayerBasedata.SetUserLevel(100);
        // Debug.Log(PlayerBasedata.GetUserLevel());
        hero.SetLevel(PlayerBasedata.GetUserLevel());
        warrior.SetLevel(PlayerBasedata.GetUserLevel());
        mage.SetLevel(PlayerBasedata.GetUserLevel());
        priest.SetLevel(PlayerBasedata.GetUserLevel());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
