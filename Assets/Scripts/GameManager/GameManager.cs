using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SkillTree st;
    public PauseManager pm;
    public bool onButtonClick = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
           
        
    }
    public void setSkillTreeActive()
    {
        st.gameObject.SetActive(true);
        //if(onButtonClick == true) pm.ToggleSkillTree(false);
        //else pm.ToggleSkillTree(false);
        //onButtonClick = false;
    }
}
