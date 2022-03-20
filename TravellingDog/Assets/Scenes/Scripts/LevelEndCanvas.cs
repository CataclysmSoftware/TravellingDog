using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEndCanvas : MonoBehaviour
{
	private GameObject levelEndScreen;
	private string mainMenuSceneName = "Main Menu";
	private string shopSceneName = "Store";
	private LevelEnd theLevelEnd;
	private LevelManager theLevelManager;
	public GameObject avertismentImage;
	private int numberToWatch;
	public Sprite buttonOf;
	public Sprite buttonOn;
	public Image x2Coin;

	void Start()
	{
		levelEndScreen = GameObject.Find("Starter pack/Canvas/LevelEndScreen");
		theLevelEnd = FindObjectOfType<LevelEnd>();
		theLevelManager = FindObjectOfType<LevelManager>();
		numberToWatch = 1;
		avertismentImage.SetActive(false);
		x2Coin.sprite = buttonOn;
	}

	public void NextLevel()
	{
		levelEndScreen.SetActive(false);
		theLevelEnd.LevelEndNextLevel();
	}

	public void MainMenu()
	{
		levelEndScreen.SetActive(false);
		SceneManager.LoadScene(mainMenuSceneName);
	}

	public void x2Coins()
	{
		if (numberToWatch == 1)
		{
			if (SystemInfo.deviceType == DeviceType.Desktop || Debug.isDebugBuild)
			{
				//Debug.Log("DEBUG: x2Coins: Aplicatia ruleaza pe desktop sau este Debug Build");
			}

			//Debug.Log("DEBUG: x2Coins: Exista conexiune la internet");
			theLevelManager.DisplayRewardBasedVideo(2);
			numberToWatch--;
			x2Coin.sprite = buttonOf;
		}
		else if (numberToWatch == 0)
		{
			avertismentImage.SetActive(true);
		}
	}

	public void Close()
	{
		avertismentImage.SetActive(false);
	}

	public void Shop()
	{
		levelEndScreen.SetActive(false);
		SceneManager.LoadScene(shopSceneName);
	}
}
