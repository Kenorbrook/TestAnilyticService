using UnityEngine.SceneManagement;

public static class SceneLoader
{
    private const string GAME_SCENE = "Game";
    private const string MENU_SCENE = "MenuScene";

    internal static void PlayScene()
    {
        SceneManager.LoadScene(GAME_SCENE);
    } 
    internal static void MenuScene()
    {
        SceneManager.LoadScene(MENU_SCENE);
    } 
}
