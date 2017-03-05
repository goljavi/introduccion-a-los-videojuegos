using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class PostprocessTemplate : MonoBehaviour
{
    //Material que vamos a usar para procesar la imagen.
    public Material postprocessMaterial;
    public float noiseAmmount;
    
    void Update()
    {
        noiseAmmount -= Time.deltaTime;
        if (noiseAmmount < 0) noiseAmmount = 0;
    }

    //Se llama luego de que la cámara renderea.
    //http://docs.unity3d.com/ScriptReference/MonoBehaviour.OnRenderImage.html
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {        
        postprocessMaterial.SetTexture("_MainTex",src);
        postprocessMaterial.SetFloat("_NoiseAmmount", noiseAmmount);
        //BLIT: Agarra la textura source, la procesa en el material
        //y la agrega al destination. 
        //http://docs.unity3d.com/ScriptReference/Graphics.Blit.html
        Graphics.Blit(src, dest, postprocessMaterial);
    }
}
