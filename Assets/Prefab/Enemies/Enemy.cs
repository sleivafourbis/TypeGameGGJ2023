using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")] 
    public int attackDamge;
    
    [Header("Logic")]
    public SpriteRenderer cursor;
    public bool isFocused;
    public string currentWord;
    public List<char> arrWord;
    public TextMesh word;
    public TextMesh correctWord;
    public TextMesh wrongWord;
    public TextMesh healthTextMesh;
    public TextMesh timerTextMesh;
    public GameObject SpriteObj;
    private Animator animator;
    private int errorCount = 0;
    public int errorTolreance;
    public bool isDead = false;
    
    [Header("Health")]
    public int health;
    public int MAX_HEALTH;
    public int MIN_HEALTH;

    [Header("Timing")] 
    public float displayTiming;
    private float timing;
    public int MAX_TIMING;
    public int MIN_TIMING;

    [Header("Audio")] 
    public AudioSource audioSource;
    public List<AudioClip> hitClips;
    public List<AudioClip> damageClips;
    public List<AudioClip> deathClips;
    
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        animator = SpriteObj.GetComponent<Animator>();
        SetWord();
        health = Random.Range(MIN_HEALTH, MAX_HEALTH + 1);
        timing = Random.Range(MIN_TIMING, MAX_TIMING + 1);
        displayTiming = timing;
        UpdateEnemyGUI();
        
    }

    void Update()
    {
        if (isDead)
        {
            return;
        }
        
        displayTiming -= Time.deltaTime;
        
        if (isFocused)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(arrWord[0].ToString().ToLower()))
                {
                    arrWord.RemoveAt(0);
                    currentWord = ListCharToString(arrWord);
                    ShowWordSuccess();
                    Debug.Log("ok");
                }
                else
                {
                    ShowWordError();
                    errorCount++;
                    Debug.Log("error");
                }
            }
        }

        if (errorCount > errorTolreance)
        {
            displayTiming--;
            errorCount = 0;
        }
        
        if (arrWord.Count == 0)
        {
            GetDamage();
            SetWord();
        }

        if (displayTiming <= 0)
        {
            Attack();
            displayTiming = timing;
        }

        if (health <= 0)
        {
            health = 0;
            isDead = true;
        }

        if (isDead)
        {
            Dead();
            displayTiming = 0;
        }
        
        UpdateEnemyGUI();
    }

    void Dead()
    {
        animator.SetTrigger("Death");
        var randomClip = Random.Range(0, deathClips.Count);
        audioSource.PlayOneShot(deathClips[randomClip]);
        GameObject.Find(nameof(BattleManager)).gameObject.GetComponent<BattleManager>().RemoveEnemy(gameObject);
    }
    
    void GetDamage()
    {
        animator.SetTrigger("Damaged");
        
    }

    public void ExecuteDamage()
    {
        var randomClip = Random.Range(0, damageClips.Count);
        audioSource.PlayOneShot(damageClips[randomClip]);
        healthTextMesh.color = Color.red;
        var coroutine = BlendFromRed(healthTextMesh);
        StartCoroutine(coroutine);
        health -= PlayerStats.attackDamage;
    }

    IEnumerator BlendFromRed(TextMesh txt)
    {
        yield return new WaitForSeconds(0.4f);
        txt.color = Color.white;
    }

    void Attack()
    {
        PlayerStats.health -= attackDamge;
        var randomClip = Random.Range(0, hitClips.Count);
        audioSource.PlayOneShot(hitClips[randomClip]);
        Debug.Log($"Player Health : {PlayerStats.health}");
    }
    
    void UpdateEnemyGUI()
    {
        healthTextMesh.text = $"vida: {health}";
        timerTextMesh.text = $"t: {displayTiming.ToString("##.00")}";
        word.text = currentWord;
    }

    void SetWord()
    {
        currentWord = WordsService.GetWord(1);
        var len = currentWord.Length;
        word.gameObject.transform.SetLocalPositionAndRotation(new Vector3(len*3.6f, 40, 0), new Quaternion(0,0,0,0));
        word.text = currentWord;
        correctWord.gameObject.transform.SetLocalPositionAndRotation(new Vector3(len*3.6f, 40, 1), new Quaternion(0,0,0,0));
        correctWord.text = currentWord;
        wrongWord.gameObject.transform.SetLocalPositionAndRotation(new Vector3(len*3.6f, 40, 2), new Quaternion(0,0,0,0));
        wrongWord.text = currentWord;

        arrWord = currentWord.ToCharArray().ToList();
    }

    string ListCharToString(List<char> arr)
    {
        var str = "";
        foreach (var item in arr)
        {
            str += item;
        }

        return str;
    }

    void ShowWordError()
    {
        var wrongWordTransform = wrongWord.gameObject.transform;
        var wrongWordPosition = wrongWordTransform.position;
        if (wrongWordPosition.z > 1.1f)
        {
            wrongWordTransform.SetPositionAndRotation(new Vector3(wrongWordPosition.x, wrongWordPosition.y, 1), new Quaternion(0,0,0,0));
            correctWord.gameObject.transform.SetPositionAndRotation(new Vector3(wrongWordPosition.x, wrongWordPosition.y, 2), new Quaternion(0,0,0,0));
        }
    }

    void ShowWordSuccess()
    {
        var wrongWordTransform = wrongWord.gameObject.transform;
        var wrongWordPosition = wrongWordTransform.position;
        if (wrongWordPosition.z < 1.9f)
        {
            wrongWordTransform.SetPositionAndRotation(new Vector3(wrongWordPosition.x, wrongWordPosition.y, 2), new Quaternion(0,0,0,0));
            correctWord.gameObject.transform.SetPositionAndRotation(new Vector3(wrongWordPosition.x, wrongWordPosition.y, 1), new Quaternion(0,0,0,0));
        }
    }
}
