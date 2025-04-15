using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutoutObject : MonoBehaviour
{
    [SerializeField]
    private Transform targetObject;

    [SerializeField]
    private LayerMask wallMask;

    [SerializeField]
    private Shader cutoutShader;

    private Camera mainCamera;
    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        ApplyShaderToLayerObjects();
    }
    
    private void Start()
    {
        if(WorldSaveGameManager.instance != null)
        {
            targetObject = WorldSaveGameManager.instance.player.transform;
        }else {
            Debug.LogError("WorldSaveGameManager instance is null. Make sure it is initialized before CutoutObject.");
        }
    }

    private void ApplyShaderToLayerObjects()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach(GameObject obj in allObjects)
        {
            if(((1 << obj.layer) & wallMask) != 0)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer != null)
                {
                    originalMaterials[renderer] = renderer.materials;

                    Material[] newMaterials = new Material[renderer.materials.Length];
                    for (int i = 0; i < renderer.materials.Length; i++) {
                        newMaterials[i] = new Material(cutoutShader);
                        
                        if (renderer.materials[i].HasProperty("_MainTexture"))
                        {
                            newMaterials[i].SetTexture("_MainTexture", renderer.materials[i].GetTexture("_MainTexture"));
                        }
                    
                    }
                    
                    renderer.materials = newMaterials;
                }
            }
        }
    }

    private void Update()
    {
        Vector2 cutoutPos = mainCamera.WorldToViewportPoint(targetObject.position);
        cutoutPos.y /= (Screen.width / Screen.height);

        Vector3 offset = targetObject.position - transform.position;
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);

        foreach (RaycastHit hit in hitObjects)
        {
            Renderer renderer = hit.transform.GetComponent<Renderer>();
            if (renderer != null)
            {
                foreach (Material material in renderer.materials)
                {
                    material.SetVector("_CutoutPos", cutoutPos);
                    material.SetFloat("_CutoutSize", 0.12f);
                    material.SetFloat("_FalloffSize", 0.02f);
                }
            }
        }
    }
}
