using UnityEngine;
using System.Collections;

public class GameMenuController : MonoBehaviour {

	public bool inMainMenu = true;					//If true, pause button disabled in main menu (Cancel in input manager, default escape key)
	public Animator animColorFade; 					//Reference to animator which will fade to and from black when starting game.
    public Animator animMenuAlpha;					//Reference to animator that will fade out alpha of MenuPanel canvas group
	public AnimationClip fadeColorAnimationClip;		//Animation clip fading to color (black default) when changing scenes
	public AnimationClip fadeAlphaAnimationClip;		//Animation clip fading out UI elements alpha


	private PlayMusic playMusic;										//Reference to PlayMusic script
	private float fastFadeIn = .1f;									//Very short fade time (10 milliseconds) to start playing music immediately without a click/glitch
	private ShowPanels showPanels;										//Reference to ShowPanels script on UI GameObject, to show and hide panels

    public void StartButtonClicked()
    {
        Invoke("LoadDelayed", fadeColorAnimationClip.length * .5f);
        animColorFade.SetTrigger("fade");
    }

}
