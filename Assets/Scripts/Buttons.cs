using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField]
    private Menu menu;

    [SerializeField]
    private Panel panel;

    private MoveFigures moveFigures;

    public void Quit()
    {
        Application.Quit();
    }

    public void PlayWithBot()
    {
        menu.PlayWithBot();
        panel.StartGame();
    }

    public void PlayWithFriendOffline()
    {
        menu.PlayWithFriendOffline();
        panel.StartGame();
    }

    public void Restart()
    {
        if (moveFigures == null)
            moveFigures = GameObject.FindGameObjectWithTag("GameBoard").GetComponent<MoveFigures>();
        moveFigures.Restart();
        panel.Restart();
    }

    public void BackToMenu()
    {
        if (moveFigures == null)
            moveFigures = GameObject.FindGameObjectWithTag("GameBoard").GetComponent<MoveFigures>();
        panel.BackToMenu();
        moveFigures.BackToMenu();
    }
}