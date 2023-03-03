using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignTexture : MonoBehaviour
{

    public ComputeShader shader;
    public int textResolution = 256;

    Renderer rend;
    RenderTexture outputTexture;
    int kernelHandel;

    void Start()
    {
        outputTexture = new RenderTexture(textResolution, textResolution, 0);
        outputTexture.enableRandomWrite = true;
        outputTexture.Create();

        rend = GetComponent<Renderer>();
        rend.enabled = true;

        InitShader();

	}

    private void InitShader()
    {
        kernelHandel = shader.FindKernel("CSMain");
        shader.SetTexture(kernelHandel, "Result", outputTexture);
        rend.material.SetTexture("_MainTex", outputTexture);

        DispatchShader(textResolution / 16, textResolution / 16);
    
    }

    private void DispatchShader(int x, int y)
    {

        shader.Dispatch(kernelHandel, x, y, 1);

    }


	void Update()
    {

        if (Input.GetButtonUp("Fire1"))
        {
            DispatchShader(textResolution / 8, textResolution / 8);
        
        }

    }
}
