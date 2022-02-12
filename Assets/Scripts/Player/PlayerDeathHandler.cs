using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeathHandler : MonoBehaviour
{
    [SerializeField] private Image OnDeathRedScreen;

    [Header("Death Options")] 
    [SerializeField] private int lives;     //DISPLAY ONLY
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float respawnTimer;

    [Header("UI References")] 
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private GameObject textBlock;

    public void ExecuteRespawnSequence()
    {
        lives--;
        
        DisplayHearts();
        
        //Make the screen blink red
        OnDeathRedScreen.color = new Color(1, 0, 0, 0.95f);
        InvokeRepeating(nameof(FadeGotHitScreen), 0, Time.deltaTime);
        
        
        foreach (var asteroidScript in GetAllAsteroidScripts())
        {
            var asteroidRigidBody2D = asteroidScript.gameObject.GetComponent<Rigidbody2D>();

            asteroidScript.gameObject.AddComponent<ConstantForce2D>()
                .force = asteroidScript.gameObject.transform.position.normalized * 2.25f;
            Destroy(asteroidScript.gameObject, 5);
        }
        
        ResetPlayer();
    }

    public void PlayExplosion(float scaleMultiplier)
    {
        var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        explosion
            .transform.GetChild(0).gameObject
            .GetComponent<Animator>()
            .Play("explode");
        explosion
            .transform.localScale = new Vector3(scaleMultiplier, scaleMultiplier, 1);
        Destroy(explosion, 1);
    }

    public void ResetPlayer()
    {
        GetComponent<Rigidbody2D>().Sleep();
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<Movement>().enabled = false;
        
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }

    #region UI MANAGEMENT

    private async void DisplayHearts()
    {
        await Task.Delay(TimeSpan.FromSeconds(1));
        
        foreach (var heart in hearts)
        {
            if (!heart.activeSelf)
                continue;

            float animationDuration = 1.25f;
            
            LeanTween.moveLocalY(heart, 0, animationDuration).setEaseInOutQuad();
            LeanTween.alpha(heart.GetComponent<RectTransform>(), 1f, animationDuration).setEaseInOutQuad();
            await Task.Delay(TimeSpan.FromSeconds(.25));
        }
        
        RemoveHeartAnimation();
    }

    public async void RemoveHeartAnimation()
    {
        await Task.Delay(TimeSpan.FromSeconds(1.5f));

        GameObject heartToRemove = hearts[lives];
        float animationDuration = 1.25f;

        LeanTween.color(heartToRemove.GetComponent<RectTransform>(), Color.red, 0).setEaseInOutQuad().setOnComplete(
            fadeOutHeart =>
            {
                var rectTransform = heartToRemove.GetComponent<RectTransform>();
                LeanTween.color(rectTransform, Color.white, .55f).setEaseInOutQuad();
                LeanTween.alpha(rectTransform, 0f, 1f).setEaseInOutQuad()
                    .setOnComplete(disableRemovedHeart =>
                    {
                        heartToRemove.SetActive(false);
                    });

                LeanTween.moveLocalY(heartToRemove, 150, animationDuration).setEaseInOutQuad();
            });
        
        DisplayText();
    }

    public async void DisplayText()
    {
        await Task.Delay(TimeSpan.FromSeconds(1f));

        textBlock.GetComponent<Text>().text = $"You have {lives} lives left.";
        LeanTween.textColor(textBlock.GetComponent<RectTransform>(), new Color(1, 1, 1, 1), 1f).setEaseInOutQuad();
    }

        private void FadeGotHitScreen()
        {
            if (OnDeathRedScreen.color.a < 0)
            {
                OnDeathRedScreen.color = new Color(1, 0, 0, 0f);
                CancelInvoke();
            }
            
            OnDeathRedScreen.color = new Color(1, 0, 0, OnDeathRedScreen.color.a - Time.deltaTime * 2);
        }

    #endregion

    

    private List<Asteroid> GetAllAsteroidScripts()
    {
        Transform asteroidPlaceholder = GameObject.Find("---- ASTEROID ----").transform;

        List<Asteroid> asteroids = new List<Asteroid>();
        foreach (Transform child in asteroidPlaceholder)
            asteroids.Add(child.GetComponent<Asteroid>());

        return asteroids;
    }
}
