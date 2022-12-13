using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackEffect : MonoBehaviour
{
    GameObject canvas;
    public GameObject attack_effect;
    Image image;
    Vector3 begin;
    Vector3 end;
    bool active = false;
    Vector3 direction;
    float distance;
    public float duration;
    float t;
    float velocity;
    float rotation_speed = 3600.0f;
    GameObject effect_obj;

    void Start() {
        canvas = GameManager.instance.canvas_go_;
        image = gameObject.GetComponent<Image>();
    }
    void Update() {
        if (active){
            Vector3 move = direction * velocity * Time.deltaTime;
            Vector3 rotate = Vector3.back * rotation_speed * Time.deltaTime;
            effect_obj.transform.position += move;
            effect_obj.transform.Rotate(rotate);
            effect_obj.transform.localScale -= Vector3.one * 0.9f * Time.deltaTime;
            t -= Time.deltaTime;
            if(t <= 0)
            {
                Destroy(effect_obj);
                active = false;
            }    
        }
    }

    public IEnumerator showEffect() {
        // begin = Camera.main.WorldToScreenPoint(Camera.main.ScreenToWorldPoint(transform.position));
        begin = transform.position;
        end = Camera.main.WorldToScreenPoint(GameManager.instance.monsters[0].transform.position);
        direction = (end - begin).normalized;
        distance = Vector3.Distance(begin, end); 
        velocity = distance / duration;
        effect_obj = Instantiate(attack_effect, canvas.transform);
        effect_obj.transform.position = begin;
        effect_obj.GetComponent<Image>().sprite = image.sprite;
        t = duration;
        active = true;
        yield return new WaitUntil(() => active == false);
    }
}
