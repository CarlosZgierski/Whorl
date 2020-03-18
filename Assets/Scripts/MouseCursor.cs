
using UnityEngine;
using UnityEngine.UI;


public class MouseCursor : MonoBehaviour
{
    static MouseCursor instance;

     Vector2 mouse;
     int w = 83;
     int h =  83;
    [SerializeField]
     Texture2D cursor;
     
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Cursor.visible = false;

    }

    void Update()
    {
        mouse = new Vector2(Input.mousePosition.x + 40, Screen.height - Input.mousePosition.y + 40);

   
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(mouse.x - (w / 2), mouse.y - (h / 2), w, h), cursor);
    }
}
