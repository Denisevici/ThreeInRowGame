using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField]
    private GameObject botButton;

    [SerializeField]
    private GameObject offlineButton;

    [SerializeField]
    private GameObject restartButton;

    [SerializeField]
    private GameObject quitButton;

    [SerializeField]
    private GameObject menuButton;

    [SerializeField]
    private GameObject player1Text;
    
    [SerializeField]
    private GameObject player1ScoreText;

    [SerializeField]
    private GameObject player2Text;

    [SerializeField]
    private GameObject player2ScoreText;

    [SerializeField]
    private GameObject player1TurnText;

    [SerializeField]
    private GameObject player2TurnText;

    private UnityEngine.UI.Text player1;
    
    private UnityEngine.UI.Text player2;

    private readonly float heightUI = Screen.height * 0.05f;

    private readonly float widthUI = Screen.width * 0.25f;

    private void Start()
    {
        SizeUI();
        MoveUI();

        botButton.SetActive(true);
        offlineButton.SetActive(true);
        restartButton.SetActive(false);
        quitButton.SetActive(true);
        menuButton.SetActive(false);
        player1Text.SetActive(false);
        player1ScoreText.SetActive(false);
        player2Text.SetActive(false);
        player2ScoreText.SetActive(false);
        player1TurnText.SetActive(false);
        player2TurnText.SetActive(false);

        player1 = player1ScoreText.GetComponent<UnityEngine.UI.Text>();
        player1.text = "0";
        player2 = player2ScoreText.GetComponent<UnityEngine.UI.Text>();
        player2.text = "0";
    }

    private void SizeUI()
    {
        botButton.GetComponent<RectTransform>().sizeDelta = new Vector2(widthUI, heightUI);
        offlineButton.GetComponent<RectTransform>().sizeDelta = new Vector2(widthUI, heightUI);
        restartButton.GetComponent<RectTransform>().sizeDelta = new Vector2(widthUI, heightUI);
        quitButton.GetComponent<RectTransform>().sizeDelta = new Vector2(widthUI, heightUI);
        menuButton.GetComponent<RectTransform>().sizeDelta = new Vector2(widthUI, heightUI);
        player1Text.GetComponent<RectTransform>().sizeDelta = new Vector2(widthUI, 2 * heightUI);
        player1ScoreText.GetComponent<RectTransform>().sizeDelta = new Vector2(widthUI, 2 * heightUI);
        player2Text.GetComponent<RectTransform>().sizeDelta = new Vector2(widthUI, 2 * heightUI);
        player2ScoreText.GetComponent<RectTransform>().sizeDelta = new Vector2(widthUI, 2 * heightUI);
        player1TurnText.GetComponent<RectTransform>().sizeDelta = new Vector2(widthUI, 2 * heightUI);
        player2TurnText.GetComponent<RectTransform>().sizeDelta = new Vector2(widthUI, 2 * heightUI);
    }

    private void MoveUI()
    {
        botButton.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.7f);
        offlineButton.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.8f);
        restartButton.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.3f);
        quitButton.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.3f);
        menuButton.transform.position = new Vector2(Screen.width * 0.75f, Screen.height * 0.9f);
        player1Text.transform.position = new Vector2(Screen.width * 0.1f, Screen.height * 0.4f);
        player1ScoreText.transform.position = new Vector2(Screen.width * 0.1f, Screen.height * 0.45f);
        player2Text.transform.position = new Vector2(Screen.width * 0.1f, Screen.height * 0.6f);
        player2ScoreText.transform.position = new Vector2(Screen.width * 0.1f, Screen.height * 0.55f);
        player1TurnText.transform.position = new Vector2(Screen.width * 0.9f, Screen.height * 0.5f);
        player2TurnText.transform.position = new Vector2(Screen.width * 0.9f, Screen.height * 0.5f);
    }

    public void StartGame()
    {
        botButton.SetActive(false);
        offlineButton.SetActive(false);
        restartButton.SetActive(false);
        quitButton.SetActive(false);
        menuButton.SetActive(true);
        player1Text.SetActive(true);
        player1ScoreText.SetActive(true);
        player2Text.SetActive(true);
        player2ScoreText.SetActive(true);
    }

    public void GameOver(int player1Score, int player2Score)
    {
        player1.text = player1Score.ToString();
        player2.text = player2Score.ToString();
        restartButton.SetActive(true);
        player1TurnText.SetActive(false);
        player2TurnText.SetActive(false);
    }

    public void Restart()
    {
        restartButton.SetActive(false);
    }

    public void BackToMenu()
    {
        botButton.SetActive(true);
        offlineButton.SetActive(true);
        restartButton.SetActive(false);
        quitButton.SetActive(true);
        menuButton.SetActive(false);
        player1Text.SetActive(false);
        player1ScoreText.SetActive(false);
        player2Text.SetActive(false);
        player2ScoreText.SetActive(false);
        player1TurnText.SetActive(false);
        player2TurnText.SetActive(false);

        player1.text = "0";
        player2.text = "0";
    }

    public void ChangeTurnText(bool player1, bool player2)
    {
        player1TurnText.SetActive(player1);
        player2TurnText.SetActive(player2);
    }
}
