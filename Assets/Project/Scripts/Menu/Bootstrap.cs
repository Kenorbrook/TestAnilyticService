using Analytics;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{ 
    
    [SerializeField] private string _serverUrl = "myurl";

    private void Awake()
    {
        PlayerDataChanger.LoadData();
        AnalyticService.Initialize(_serverUrl);
    }

}
