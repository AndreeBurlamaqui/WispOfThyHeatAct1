using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
public class CreateStaticTilemap : MonoBehaviour
{
    private void Reset()
    {
        PhysicsMaterial2D groundMat = Resources.Load("Ground") as PhysicsMaterial2D;

        GameObject newStaticFloor = new GameObject("StaticFloor");
        newStaticFloor.isStatic = true;

        GameObject newGridSF = new GameObject("Grid");
        newGridSF.transform.SetParent(newStaticFloor.transform);
        newGridSF.AddComponent(typeof(Grid));

        GameObject newTilemapSF = new GameObject("Tilemap");
        newTilemapSF.transform.SetParent(newGridSF.transform);
        newTilemapSF.AddComponent(typeof(Tilemap));
        TilemapRenderer newTLRender = newTilemapSF.AddComponent(typeof(TilemapRenderer)) as TilemapRenderer;
        newTLRender.sharedMaterial = (Material)AssetDatabase.LoadAssetAtPath("Packages/com.unity.render-pipelines.universal/Runtime/Materials/Sprite-Unlit-Default.mat", typeof(Material));

        TilemapCollider2D newTlCol = newTilemapSF.AddComponent(typeof(TilemapCollider2D)) as TilemapCollider2D;
        newTlCol.usedByComposite = true;

        Rigidbody2D newRBTL = newTilemapSF.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        newRBTL.bodyType = RigidbodyType2D.Static;
        newRBTL.sharedMaterial = groundMat;

        CompositeCollider2D newCompCol = newTilemapSF.AddComponent(typeof(CompositeCollider2D)) as CompositeCollider2D;
        newCompCol.sharedMaterial = groundMat;

        newGridSF.gameObject.layer = LayerMask.NameToLayer("GroundWalls");
        newTilemapSF.gameObject.layer = LayerMask.NameToLayer("GroundWalls");



        DestroyImmediate(this);

    }
}

#endif
