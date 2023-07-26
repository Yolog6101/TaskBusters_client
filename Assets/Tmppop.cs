using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading.Tasks;


public class Tmppop : MonoBehaviour
{
    public GameObject pop;

    public async void popop(){
        pop.SetActive(true);
        await Task.Delay(1000);
        pop.SetActive(false);
    }
}
