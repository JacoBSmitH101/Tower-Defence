using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyPanel : MonoBehaviour
{
    [Header("Tab Options")]

    [SerializeField] Image tabArrow;
    [SerializeField] Button tabButton;
    [SerializeField] GameObject buyPanel;
    [SerializeField] float moveAmount;
    [SerializeField] Animator animator;

    public bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        buyPanel = gameObject;
        tabArrow.transform.localScale = new Vector3(tabArrow.transform.localScale.x, 4.1f, tabArrow.transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        // if (!isOpen){
        //     tabArrow.transform.localScale = new Vector3(tabArrow.transform.localScale.x, -4.1f, tabArrow.transform.localScale.z);
        // }else{
        //     tabArrow.transform.localScale = new Vector3(tabArrow.transform.localScale.x, 4.1f, tabArrow.transform.localScale.z);
        // }
    }
    public void toggleOpen() {
        isOpen = animator.GetBool("Open");
        animator.SetBool("Open", !isOpen);
        if (!isOpen){
            tabArrow.transform.localScale = new Vector3(tabArrow.transform.localScale.x, -4.1f, tabArrow.transform.localScale.z);
        }else{
            tabArrow.transform.localScale = new Vector3(tabArrow.transform.localScale.x, 4.1f, tabArrow.transform.localScale.z);
        }
    }

}
