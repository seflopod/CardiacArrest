using UnityEngine;
using System;

public class InputController : Singleton<InputController>
{
    protected override void Awake()
    {
        base.Awake();
    }
    
    private void Update()
    {
		//How input is handled is dependent on the game state...
        switch(GameManager.State)
        {
            case GameState.TitleScreen:
                TitleInputController();
                break;
            case GameState.PlayingGame:
                PlayInputController();
                break;
            case GameState.Quit: //??
                break;
            default:
                break;
        }
        
		//...except quitting, that is always Esc
        if(Input.GetKeyDown(KeyCode.Escape))
			GameManager.State = GameState.Quit;
    }
    
    private void TitleInputController()
    {
        if(Input.GetButton("Fire1") || Input.GetKey(KeyCode.Return))
        {
            GameManager.Instance.StartGame();
        }
        else
        {                
            MovementManager.Instance.TitleScreenMovement(
											Input.GetButtonDown("Horizontal"),
                                            (Input.GetButtonDown("Jump") ||
                                            Input.GetButtonDown("Vertical")));
        }
    }
    
    private void PlayInputController()
    {
        MovementManager.Instance.PlayerMovement(Input.GetButtonDown("Horizontal"),
                                            (Input.GetButtonDown("Jump") ||
                                            Input.GetButtonDown("Vertical")));
    }
}