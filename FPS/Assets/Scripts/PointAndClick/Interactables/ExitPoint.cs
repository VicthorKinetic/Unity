using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitPoint : ImageViewer {


    bool one_click = false;
    bool timer_running;
    float timer_for_double_click;
    float delay = 0.1f;

    #region Overriding the interaction to change scene
    public override void Interact ()
	{
		base.Interact ();

		GameManager.ins.currentNode.col.enabled = true;

        CheckMouse();
    }
	#endregion

	#region Check for mouse click to change scene
	public void CheckMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!one_click)
            {
                one_click = true;

                timer_for_double_click = Time.time;
            }
            else
            {
                one_click = false;

                SceneManager.LoadScene("Exploration");
            }
        }
        if (one_click)
        {
            if ((Time.time - timer_for_double_click) > delay)
            {
                one_click = false;
            }
        }
    }
    #endregion
}
