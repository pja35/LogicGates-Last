using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Un ensemble d'outils pour ajouter des sprites à un gameobject
/// </summary>
public class SpriteUtil : MonoBehaviour
{
    /// <summary>
    /// Donne une taille absolue à un objet
    /// </summary>
    /// <param name="obj">L'objet à modifier</param>
    /// <param name="sizeX">Taille horizontale</param>
    /// <param name="sizeY">Taille verticale</param>
    public static void SetGameObjectToAbsSize(GameObject obj, float sizeX, float sizeY)
    {
        float actSizeX = obj.GetComponent<Renderer>().bounds.size.x;
        float actSizeY = obj.GetComponent<Renderer>().bounds.size.y;
        Vector3 rescale = obj.transform.localScale;
        rescale.x = sizeX * rescale.x / actSizeX;
        rescale.y = sizeY * rescale.y / actSizeY;
        obj.transform.localScale = rescale;
    }

    /// <summary>
    /// Ajoute une croix rouge sur un gameobject
    /// </summary>
    /// <param name="holder">L'objet sur lequel ajouter une croix</param>
    /// <param name="sizeReference">La taille de l'objet</param>
    public static void AddCrossSprite(GameObject holder,GameObject sizeReference)
    {
        SpriteRenderer renderer = holder.AddComponent<SpriteRenderer>();
        renderer.sprite = Resources.Load<Sprite>("cross");
        Vector3 size = sizeReference.gameObject.GetComponent<Renderer>().bounds.size;
        SpriteUtil.SetGameObjectToAbsSize(holder, size.x, size.y);
    }

   
    private static bool HasCross(GameObject holder)
    {
        for (int i = 0; i < holder.transform.childCount; i++)
        {
            GameObject childAct = holder.transform.GetChild(i).gameObject;
            if (childAct.transform.name == "cross")
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Rajoute une croix sur un objet et donne à la croix la taille de l'objet
    /// </summary>
    /// <param name="holder">L'objet auquel ajouter la croix</param>
    public static void AddCross(GameObject holder)
    {
        if (!HasCross(holder))
        {
            GameObject cross = new GameObject();
            cross.transform.SetParent(holder.transform);
            cross.transform.name = "cross";
            cross.transform.localPosition = new Vector3(0, 0, -1);
            SpriteUtil.AddCrossSprite(cross, holder);
        }
    }

    /// <summary>
    /// Supprime la croix de l'objet si il en as une
    /// </summary>
    /// <param name="holder">Le posseseur de la croix</param>
    public static void RemoveCross(GameObject holder)
    {
        for(int i = 0; i < holder.transform.childCount; i++)
        {
            GameObject childAct = holder.transform.GetChild(i).gameObject;
            if (childAct.transform.name == "cross")
            {
                Destroy(childAct);
            }
        }
    }

}
