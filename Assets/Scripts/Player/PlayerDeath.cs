using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    #region Varibles

    [Header("Misc")]
    //private Animator anim;
    private Rigidbody2D rb;
    public GameObject deathMenu;
    //[SerializeField] private AudioSource deathSoundEffect;

    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        //anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Die();
        }
    }

    private void Die()
    {
        //deathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        //anim.SetTrigger("death");
        deathMenu.SetActive(!deathMenu.activeSelf);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
