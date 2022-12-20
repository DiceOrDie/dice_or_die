using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tutorial : MonoBehaviour
{
    public int now_page = 1;
    public Button next;
    public Button last;
    public Button finish;
    public GameObject first;
    public GameObject second;
    public GameObject third;
    public GameObject fourth;
    public GameObject fifth;
    public GameObject sixth;
    public GameObject seventh;
    public GameObject eighth;
    public GameObject nineth;
    public GameObject tenth;
    public GameObject eleventh;
    public GameObject twelves;

    void Start()
    {
        next.onClick.AddListener(change_next);
        last.onClick.AddListener(change_last);
        finish.onClick.AddListener(tut_finish);
        update_page();
    }

    void change_next()
    {
        Debug.Log("you click next");
        if(now_page != 12)
        {
            now_page += 1;
            update_page();
        }
    }

    void change_last()
    {
        Debug.Log("you click last");
        if(now_page != 1)
        {
            now_page -= 1;
            update_page();
        }
    }

    void tut_finish()
    {
        Debug.Log("next scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void update_page()
    {
        if(now_page == 1)
        {            
            last.gameObject.SetActive(false);
            next.gameObject.SetActive(true);
            first.SetActive(true);
            second.SetActive(false);
            third.SetActive(false);
            fourth.SetActive(false);
            fifth.SetActive(false);
            sixth.SetActive(false);
            seventh.SetActive(false);
            eighth.SetActive(false);
            nineth.SetActive(false);
            tenth.SetActive(false);
            eleventh.SetActive(false);
            twelves.SetActive(false);
        }
        else if(now_page == 2)
        {
            last.gameObject.SetActive(true);
            next.gameObject.SetActive(true);
            first.SetActive(false);
            second.SetActive(true);
            third.SetActive(false);
            fourth.SetActive(false);
            fifth.SetActive(false);
            sixth.SetActive(false);
            seventh.SetActive(false);
            eighth.SetActive(false);
            nineth.SetActive(false);
            tenth.SetActive(false);
            eleventh.SetActive(false);
            twelves.SetActive(false);
        }
        else if(now_page == 3)
        {
            last.gameObject.SetActive(true);
            next.gameObject.SetActive(true);
            first.SetActive(false);
            second.SetActive(false);
            third.SetActive(true);
            fourth.SetActive(false);
            fifth.SetActive(false);
            sixth.SetActive(false);
            seventh.SetActive(false);
            eighth.SetActive(false);
            nineth.SetActive(false);
            tenth.SetActive(false);
            eleventh.SetActive(false);
            twelves.SetActive(false);
        }
        else if(now_page == 4)
        {
            last.gameObject.SetActive(true);
            next.gameObject.SetActive(true);
            first.SetActive(false);
            second.SetActive(false);
            third.SetActive(false);
            fourth.SetActive(true);
            fifth.SetActive(false);
            sixth.SetActive(false);
            seventh.SetActive(false);
            eighth.SetActive(false);
            nineth.SetActive(false);
            tenth.SetActive(false);
            eleventh.SetActive(false);
            twelves.SetActive(false);
        }
        else if(now_page == 5)
        {
            last.gameObject.SetActive(true);
            next.gameObject.SetActive(true);
            first.SetActive(false);
            second.SetActive(false);
            third.SetActive(false);
            fourth.SetActive(false);
            fifth.SetActive(true);
            sixth.SetActive(false);
            seventh.SetActive(false);
            eighth.SetActive(false);
            nineth.SetActive(false);
            tenth.SetActive(false);
            eleventh.SetActive(false);
            twelves.SetActive(false);
        }
        else if(now_page == 6)
        {
            last.gameObject.SetActive(true);
            next.gameObject.SetActive(true);
            first.SetActive(false);
            second.SetActive(false);
            third.SetActive(false);
            fourth.SetActive(false);
            fifth.SetActive(false);
            sixth.SetActive(true);
            seventh.SetActive(false);
            eighth.SetActive(false);
            nineth.SetActive(false);
            tenth.SetActive(false);
            eleventh.SetActive(false);
            twelves.SetActive(false);
        }
        else if(now_page == 7)
        {
            last.gameObject.SetActive(true);
            next.gameObject.SetActive(true);
            first.SetActive(false);
            second.SetActive(false);
            third.SetActive(false);
            fourth.SetActive(false);
            fifth.SetActive(false);
            sixth.SetActive(false);
            seventh.SetActive(true);
            eighth.SetActive(false);
            nineth.SetActive(false);
            tenth.SetActive(false);
            eleventh.SetActive(false);
            twelves.SetActive(false);
        }
        else if(now_page == 8)
        {
            last.gameObject.SetActive(true);
            next.gameObject.SetActive(true);
            first.SetActive(false);
            second.SetActive(false);
            third.SetActive(false);
            fourth.SetActive(false);
            fifth.SetActive(false);
            sixth.SetActive(false);
            seventh.SetActive(false);
            eighth.SetActive(true);
            nineth.SetActive(false);
            tenth.SetActive(false);
            eleventh.SetActive(false);
            twelves.SetActive(false);
        }
        else if(now_page == 9)
        {
            last.gameObject.SetActive(true);
            next.gameObject.SetActive(true);
            first.SetActive(false);
            second.SetActive(false);
            third.SetActive(false);
            fourth.SetActive(false);
            fifth.SetActive(false);
            sixth.SetActive(false);
            seventh.SetActive(false);
            eighth.SetActive(false);
            nineth.SetActive(true);
            tenth.SetActive(false);
            eleventh.SetActive(false);
            twelves.SetActive(false);
        }
        else if(now_page == 10)
        {
            last.gameObject.SetActive(true);
            next.gameObject.SetActive(true);
            first.SetActive(false);
            second.SetActive(false);
            third.SetActive(false);
            fourth.SetActive(false);
            fifth.SetActive(false);
            sixth.SetActive(false);
            seventh.SetActive(false);
            eighth.SetActive(false);
            nineth.SetActive(false);
            tenth.SetActive(true);
            eleventh.SetActive(false);
            twelves.SetActive(false);
        }
        else if(now_page == 11)
        {
            last.gameObject.SetActive(true);
            next.gameObject.SetActive(true);
            first.SetActive(false);
            second.SetActive(false);
            third.SetActive(false);
            fourth.SetActive(false);
            fifth.SetActive(false);
            sixth.SetActive(false);
            seventh.SetActive(false);
            eighth.SetActive(false);
            nineth.SetActive(false);
            tenth.SetActive(false);
            eleventh.SetActive(true);
            twelves.SetActive(false);
        }
        else if(now_page == 12)
        {
            last.gameObject.SetActive(true);
            next.gameObject.SetActive(false);
            first.SetActive(false);
            second.SetActive(false);
            third.SetActive(false);
            fourth.SetActive(false);
            fifth.SetActive(false);
            sixth.SetActive(false);
            seventh.SetActive(false);
            eighth.SetActive(false);
            nineth.SetActive(false);
            tenth.SetActive(false);
            eleventh.SetActive(false);
            twelves.SetActive(true);
        }
    }
}
