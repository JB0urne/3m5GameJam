using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour {

	public float maxChunkHealth = 100.0f;
    private float _chunkHealth;
    public float chunkHealth
    {
        get
        {
            return _chunkHealth;
        }
        set
        {
            _chunkHealth = value;
            if (_chunkHealth <= 0)
                destroy();
        }
    }

    public float TouchHealthLossPerSecond = 1f;
    public float MaxPickupHealthLoss = 40f;

	// Use this for initialization
	void Start () {
		chunkHealth = maxChunkHealth;
	}
	
	// Update is called once per frame
	void Update () {
		float colorChannel = (chunkHealth/maxChunkHealth);
		this.GetComponentInParent<SpriteRenderer>().color = new Color(colorChannel, colorChannel, colorChannel);
	}

	public void incHealth(float amount) {
		chunkHealth += amount;
	}

	public void decHealth(float amount) {
		chunkHealth -= amount;
	}

    public void DealDamageWithTouch(float touchTimeInSeconds)
    {
        chunkHealth -= TouchHealthLossPerSecond * touchTimeInSeconds;
    }

    public void DealDamageWithPickup(float damageMultiplier)
    {
        chunkHealth -= MaxPickupHealthLoss * damageMultiplier;
    }
    
    public float destroyStep1Min = 0.3f;
    public float destroyStep1Max = 1.0f;
    public float destroyStep2Min = 0.5f;
    public float destroyStep2Max = 2.0f;
    public float destroyStep3Min = 0.5f;
    public float destroyStep3Max = 4.0f;

    private bool isDestroyed = false;
    public void destroy()
    {
        if (isDestroyed == false)
        {
            isDestroyed = true;
            Vector3 oldPosition = transform.position;
            Vector3 newScale = new Vector2(0.4f, 0.4f);

            int count = (int)Random.Range(4.0f, 8f);

            for (int i = 0; i < count; ++i)
            {
                GameObject obj = cloneNewAndSmaller(this.gameObject, newScale);
                StartCoroutine(destroySetp2In(obj, Random.Range(destroyStep1Min, destroyStep1Max)));
            }
            transform.localScale = new Vector2(0.0f, 0.0f);
        }
    }

    private IEnumerator destroySetp2In(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        destroyStep2(obj);
    }

    private void destroyStep2(GameObject obj)
    {
        Vector3 newScale = new Vector2(0.2f, 0.2f);

        List<GameObject> newParts = new List<GameObject>();

        for (int i = 0; i < (int)Random.Range(4.0f, 8f); ++i)
        {
            GameObject newObj = cloneNewAndSmaller(obj, newScale);
            StartCoroutine(destroySetp3In(newObj, Random.Range(destroyStep2Min, destroyStep2Max)));
        }
        Destroy(obj);
    }

    private IEnumerator destroySetp3In(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        destroyStep3(obj);
    }

    private void destroyStep3(GameObject obj)
    {
        Vector3 newScale = new Vector2(0.1f, 0.1f);

        List<GameObject> newParts = new List<GameObject>();

        for (int i = 0; i < (int)Random.Range(4.0f, 8f); ++i)
        {
            GameObject newObj = cloneNewAndSmaller(obj, newScale);
            StartCoroutine(destroySetp4In(newObj, Random.Range(destroyStep3Min, destroyStep3Max)));
        }
        Destroy(obj);
    }

    private IEnumerator destroySetp4In(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(obj);
    }

    private GameObject cloneNewAndSmaller(GameObject parent, Vector3 newScale)
    {
        Vector3 vTransform = new Vector3(Random.Range(-1f, 1f) * 0.25f, Random.Range(-1f, 1f) * 0.25f);
        GameObject newWall = Instantiate(parent, parent.transform.position + vTransform, Quaternion.identity);
        newWall.transform.localScale = newScale;
        Rigidbody2D rid = newWall.GetComponent<Rigidbody2D>();
        rid.bodyType = RigidbodyType2D.Dynamic;
        rid.AddForce(new Vector3(vTransform.x * 100, vTransform.y * 100));
        rid.AddTorque(vTransform.x * 10);
        return newWall;
    }
}
