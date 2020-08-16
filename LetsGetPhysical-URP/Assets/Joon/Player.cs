using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [Header("Settings")] 
    public float thrustSpeed;

    public float thrustDistance;
    public float thrustMaxTime;
    public AnimationCurve thrustTimeModifierNormalized;
    public AnimationCurve gravityDistanceModifierNormalized;
    public float gravityScale;
    public float outerRadius;
    
    [Header("References")]
    [GlitchTag("listo")] 
    public SpriteRenderer[] listos;
    
    [GlitchTag("listo2")] 
    public SpriteRenderer[] listoRings;
    
    [GlitchTag("coin")] 
    public CircleCollider2D[] coins;
    
    [GlitchTag("spawn")] 
    public SpriteRenderer[] spawns;
    
    [GlitchTag("enemy")] 
    public CircleCollider2D[] enemies;
    
    [GlitchTag("exit")] 
    public CircleCollider2D[] exits;

    [GlitchTag("listotext")] public TextMeshPro[] listoTexts;

    public Color[] colors;
    public string[] keys;
    
    [GlitchTag("_this")]
    public Rigidbody2D _rigidbody;

    float[] _isThrusting;
    CircleCollider2D _currentExit;
    bool _resetting;
    void Start()
    {
        _isThrusting = new float[6];
        for (int i = 0; i < 6; i++)
        {
            listos[i].color = colors[i];
            listoRings[i].color = colors[i];

            listoTexts[i].text = keys[i].ToUpper();
        }

        transform.position = spawns.GetRandom<SpriteRenderer>().transform.position;
        foreach (var spawn in spawns)
        {
            spawn.gameObject.SetActive(false);
        }
        
        
        foreach (var exit in exits)
        {
            exit.gameObject.SetActive(false);
        }
        _currentExit = exits.GetRandom<CircleCollider2D>();
        _currentExit.gameObject.SetActive(true);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }
    
    void FixedUpdate()
    { 
 
        for (int i = 0; i < keys.Length; i++)
        {
            listoTexts[i].transform.rotation = Quaternion.identity;

            if (Input.GetKey(keys[i]))
            {
                if (_isThrusting[i] < 0)
                {
                    listos[i].transform.DOKill();
                    listos[i].transform.DOScale(1.2f, 0.05f).SetLoops(-1, LoopType.Yoyo);
                    _isThrusting[i] = 0;
                }

                _isThrusting[i] += Time.deltaTime;

                var force = -listos[i].transform.up * (thrustSpeed * thrustTimeModifierNormalized.Evaluate(_isThrusting[i] / thrustMaxTime));
                _rigidbody.AddForceAtPosition(force, listos[i].transform.position, ForceMode2D.Force);
            }
            else
            {
                if (_isThrusting[i] >= 0)
                {
                    listos[i].transform.DOKill();
                    listos[i].transform.DOScale(1f, 0.1f);
                    _isThrusting[i] = -1;
                } 
            }
        }

        var distanceNormalized = Vector3.Distance(transform.position, Vector3.zero) / outerRadius;
        distanceNormalized = Mathf.Clamp01(distanceNormalized);
        var gravity = gravityDistanceModifierNormalized.Evaluate(distanceNormalized) * gravityScale;
        var direction = transform.position.normalized;
        _rigidbody.AddForce(direction * gravity, ForceMode2D.Force);

    }

    void Win()
    {
        foreach (var listo in listos)
        {
            listo.color = Color.green;
        }
        foreach (var listo in listoRings)
        {
            listo.color = Color.green;
        }
        
        Reset();
    }

    void Lose()
    {
        foreach (var listo in listos)
        {
            listo.color = Color.red;
        }
        foreach (var listo in listoRings)
        {
            listo.color = Color.red;
        }
        
        Reset();
    }

    
    void Reset()
    {
        if (_resetting) return;
        _resetting = true;
        StartCoroutine(ResetCoroutine());

    }

    IEnumerator ResetCoroutine()
    {
        //Destroy(_rigidbody);
        foreach (var listo in listos)
        {
            var rb = listo.gameObject.AddComponent<Rigidbody2D>();
            listo.gameObject.AddComponent<CircleCollider2D>();
            rb.AddForce(Random.insideUnitCircle * (Random.value * 50 + 50), ForceMode2D.Impulse);
            rb.gravityScale = 0;
        }
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (coins.Contains(other))
        {
            other.gameObject.SetActive(false);
        }
        else if (enemies.Contains(other))
        {
            Lose();
        }
        else if (exits.Contains(other))
        {
            Win();
        }
    }
} 
 