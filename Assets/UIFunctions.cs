using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFunctions : MonoBehaviour
{
	public GameObject loadingScreen;
	public GameObject mainMenu;
	public GameObject gameOver;
	public GameObject roomsMenu;

	public void MoveToFront(GameObject currentObj)
	{
		//tempParent = currentObj.transform.parent;
		Transform tempParent = currentObj.transform;
		tempParent.SetAsLastSibling();
	}
	public void openLoadingScreen()
	{
		loadingScreen.SetActive(true);
		mainMenu.SetActive(false);
		gameOver.SetActive(false);
	}
	public void openmainMenu()
	{
		loadingScreen.SetActive(false);
		mainMenu.SetActive(true);
		gameOver.SetActive(false);
	}
	public void opengameOverMenu()
	{
		loadingScreen.SetActive(true);
		mainMenu.SetActive(false);
		gameOver.SetActive(true);
	}
	public void openroomsMenu()
	{
		roomsMenu.SetActive(true);
		MoveToFront(roomsMenu);
		loadingScreen.SetActive(false);
		mainMenu.SetActive(false);
		gameOver.SetActive(false);
	}
	public void QuitGame()
	{
		Application.Quit();
	}

}
