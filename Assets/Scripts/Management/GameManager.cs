using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum GameState
{
	TitleScreen,
	PlayingGame,
    Quit
};

[RequireComponent(typeof(InputController))]
[RequireComponent(typeof(MovementManager))]
public class GameManager : Singleton<GameManager>
{
	#region public_variables
    public PlayerMovementStats movement = new PlayerMovementStats();
    public PlayerHeartStats heartStats = new PlayerHeartStats();
    public PlatformTiles platformTileInfo = new PlatformTiles();
	public BloodCellData bloodCellData = new BloodCellData();
	public GameObject playerPrefab;
	public GameObject enemyPrefab;
	#endregion
	
	#region private_variables
	private static float _speed;
	private static float _heartRate;
	private static GameState _state;
	private Player _player;
    private bool _loadNext;
	#endregion
	
	#region unity_functions
	protected override void Awake()
	{
		base.Awake(); //make sure the Singleton inits
        DontDestroyOnLoad(transform.gameObject);
	}
	
	private void Start()
	{
		_state = GameState.PlayingGame; //DEBUG ONLY
        _loadNext = false;
		InitMainGame(); //DEBUG ONLY
	}
	#endregion
	
	#region inits
	private void InitMainGame()
	{
		InitCells();
		_player = ((GameObject)GameObject.Instantiate(playerPrefab, (new Vector3(29.0f, 1.1f, 0.0f)), Quaternion.Euler(0.0f, 180.0f, 0.0f))).GetComponent<Player>();
		GameObject.Instantiate(enemyPrefab, (new Vector3(21.0f, 1.1f, 4.4f)),
								Quaternion.Euler(0.0f, 180.0f, 0.0f));
		_speed = movement.minSpeed;
		_heartRate = heartStats.minHeartRate;
	}
	
	private void InitCells()
	{
		GenerateBloodCellPaths();
		BloodCell.GenerateCells(bloodCellData);
	}
	
	private void GenerateBloodCellPaths()
	{
		VectorPaths.GenerateLinearPath(Vector3.zero, Vector3.right*bloodCellData.numberOfCells*bloodCellData.cellXScale, true);
		VectorPaths.GenerateSinePath(bloodCellData.numberOfCells, bloodCellData.numberOfCells, 2.0f, 5.0f, 0.0f, 5.0f, true);
	}
	#endregion
	
	#region input_reactions
	/// <summary>
	/// Starts the game by loading the main level.
	/// </summary>
    public void StartGame()
    {
        if(_state == GameState.TitleScreen && !_loadNext)
        {
            _loadNext = true;
            Application.LoadLevel(1);
			_state = GameState.PlayingGame;
        }
    }
    #endregion
	
	#region properties	
	public static float Speed
	{
		get { return _speed; } 
		set { _speed = value; }
	}
	
	public static float HeartRate
	{
		get { return _heartRate; }
		set { _heartRate = value; }
	}
	
	public static GameState State
	{
		get { return _state; }
		set { _state = value; }
	}
	#endregion
}