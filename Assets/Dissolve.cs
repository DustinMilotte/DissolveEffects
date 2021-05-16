using System.Collections;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    public float speed = 1.0f;
    public bool startDissolved;
    
    private Material material;
    private bool toggle;
    private static readonly int Amount = Shader.PropertyToID("_Amount");

    public void Start()
    {
        material = GetComponent<Renderer>().material;
        if (startDissolved)
        {
            material.SetFloat(Amount, 1f);
            toggle = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            ToggleDissolve();
        }
    }

    [ContextMenu("ToggleDissolve")]
    public void ToggleDissolve()
    {
        StartCoroutine(DissolveMaterial(toggle));
        toggle = !toggle;
    }
 
 
    IEnumerator DissolveMaterial(bool isDissolving)
    {
        if (isDissolving)
        {
            for (float i = 1; i >= 0; i -= speed * Time.deltaTime)
            {
                material.SetFloat(Amount, i);
                yield return null;
            }
            material.SetFloat(Amount, 0f);
        }
        else
        {
            for (float i = 0; i <= 1; i += speed * Time.deltaTime)
            {
                material.SetFloat(Amount, i);
                yield return null;
            }
            material.SetFloat(Amount, 1f);
        }
    }
}