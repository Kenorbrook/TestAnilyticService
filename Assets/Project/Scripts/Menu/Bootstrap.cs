using Analytics;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    void Awake()
    {
        PlayerDataChanger.LoadData();
        Analytic.Initializate();
    }

}
