using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{

	


	public GameObject[] _puzzle;


	public float startPosX = -187f;
	public float startPosY = 109f;


	public float outX = 90f;
	public float outY = 90f;

	public Text _text; 

	public static int click;
	public static GameObject[,] grid;
	public static Vector3[,] position;
	private GameObject[] puzzleRandom;
	public static bool win;

	void Start()
	{
		puzzleRandom = new GameObject[_puzzle.Length];


		float posXreset = startPosX;
		position = new Vector3[5, 5];
		for (int y = 0; y < 5; y++)
		{
			startPosY -= outY;
			for (int x = 0; x < 5; x++)
			{
				startPosX += outX;
				position[x, y] = new Vector3(startPosX, startPosY, -0.3f);
			}
			startPosX = posXreset;
		}

		if (!PlayerPrefs.HasKey("New Game")) StartNewGame(); else Load();
	}

	public void StartNewGame()
	{
		win = false;
		click = 0;
		RandomPuzzle();
		//Debug.Log("New Game");
	}
	
	public void ExitGame()
	{
		Save();
		Application.Quit();
	}

	void Save()
	{
		string content = string.Empty;
		for (int y = 0; y < 5; y++)
		{
			for (int x = 0; x < 5; x++)
			{
				if (content.Length > 0) content += "|";
				if (grid[x, y]) content += grid[x, y].GetComponent<Puzzle>().ID.ToString(); else content += "null";
			}
		}
		PlayerPrefs.SetString("Puzzle", content);
		PlayerPrefs.SetString("PuzzleInfo", click.ToString());
	
		Debug.Log(this + " Save game");
	}

	void Load()
	{
		string[] content = PlayerPrefs.GetString("Puzzle").Split(new char[] { '|' });

		if (content.Length == 0 || content.Length != 25) return;

	//	if (PlayerPrefs.HasKey("PuzzleInfo")) click = Parse(PlayerPrefs.GetString("PuzzleInfo"));

		grid = new GameObject[5, 5];
		int i = 0;
		for (int y = 0; y < 5; y++)
		{
			for (int x = 0; x < 5; x++)
			{
				int j = FindPuzzle(Parse(content[i]));

				if (j >= 0)
				{
					grid[x, y] = Instantiate(_puzzle[j], position[x, y], Quaternion.identity) as GameObject;
					grid[x, y].name = "ID-" + i;
					grid[x, y].transform.parent = transform;

				}
				i++;
			}
		}
	}
	

	int FindPuzzle(int index)
	{
		int j = 0;
		foreach (GameObject e in _puzzle)
		{
			if (e.GetComponent<Puzzle>().ID == index) return j;
			j++;
		}
		return -1;
	}

	int Parse(string text)
	{
		int value;
		if (int.TryParse(text, out value)) return value;
		return -1;
	}

	void CreatePuzzle()
	{
		if (transform.childCount > 0)
		{

			for (int j = 0; j < transform.childCount; j++)
			{
				Destroy(transform.GetChild(j).gameObject);
			}
		}
		int i = 0;
		grid = new GameObject[5, 5];
		int h = Random.Range(0, 4);
		int v = Random.Range(0, 4);
		GameObject clone = new GameObject();
		grid[h, v] = clone; 
		for (int y = 0; y < 5; y++)
		{
			for (int x = 0; x < 5; x++)
			{
				
				if (grid[x, y] == null)
				{
					grid[x, y] = Instantiate(puzzleRandom[i], position[x, y], Quaternion.identity) as GameObject;
					grid[x, y].name = "ID-" + i;
					grid[x, y].transform.parent = transform;
					i++;
				}
			}
		}
		Destroy(clone);
		for (int q = 0; q < _puzzle.Length; q++)
		{
			Destroy(puzzleRandom[q]);
		}
	}

	static public void GameFinish()
	{
		int i = 1;
		for (int y = 0; y < 5; y++)
		{
			for (int x = 0; x < 5; x++)
			{
				if (grid[x, y]) { if (grid[x, y].GetComponent<Puzzle>().ID == i) i++; } else i--;
			}
		}
		if (i == 24)
		{
			for (int y = 0; y < 5; y++)
			{
				for (int x = 0; x < 5; x++)
				{
					if (grid[x, y]) Destroy(grid[x, y].GetComponent<Puzzle>());
				}
			}
			win = true;
			Debug.Log("Finish!");
		}
	}


	void RandomPuzzle()
	{
		int[] tmp = new int[_puzzle.Length];
		for (int i = 0; i < _puzzle.Length; i++)
		{
			tmp[i] = 1;
		}
		int c = 0;
		while (c < _puzzle.Length)
		{
			int r = Random.Range(0, _puzzle.Length);
			if (tmp[r] == 1)
			{
				puzzleRandom[c] = Instantiate(_puzzle[r], new Vector3(0, 10, 0), Quaternion.identity) as GameObject;
				tmp[r] = 0;
				c++;
			}
		}
		CreatePuzzle();
	}

	void Update()
	{
		if (!win)
		{
			
			//_text.text = "Moves:" + click;
		}
		else
		{
			click = 0;
			_text.text = "The end!";
		}
	}
}