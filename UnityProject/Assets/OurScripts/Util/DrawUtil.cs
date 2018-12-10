using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Un ensemble d'outils pour tracer des lignes avec Unity. Le line renderer est attaché à l'objet de départ.
/// </summary>
public class DrawUtil : MonoBehaviour
{
    /// <summary>Permet de tracer une ligne.</summary>
    ///  <param name="start">l'objet de départ</param>
    ///  <param name="end">un point d'arrivé</param>
    ///  <param name="color">la couleur de la ligne</param>
    ///  <returns>Le line renderer crée.</returns>
    public static LineRenderer DrawLine(GameObject start, Vector3 end, Color color)
    {
        //On recicle le line renderer.
        LineRenderer renderer = start.GetComponent<LineRenderer>();
        if (renderer == null)
        {
            renderer = start.AddComponent<LineRenderer>();
        }
        renderer.material = new Material(Shader.Find("Sprites/Default"));
        renderer.widthMultiplier = 0.8f;
        start.GetComponent<LineRenderer>().endColor = color;
        start.GetComponent<LineRenderer>().startColor = color;

        renderer.SetPosition(0, start.gameObject.transform.position);
        renderer.SetPosition(1, end);

        return renderer;
    }

    /// <summary>Permet de tracer une ligne.</summary>
    ///  <param name="start">l'objet de départ</param>
    ///  <param name="end">un objet d'arrivé</param>
    ///  <param name="color">la couleur de la ligne</param> 
    ///  <returns>Le line renderer crée.</returns>
    public static LineRenderer DrawLine(GameObject start, GameObject end, Color color)
    {
        return DrawLine(start, end.transform.position, color);
    }

    /// <summary>Permet de tracer une ligne.</summary>
    ///  <param name="start">l'objet de départ</param>
    ///  <param name="end">un objet d'arrivé</param>
    public static LineRenderer DrawLine(GameObject start, GameObject end)
    {
        return DrawLine(start, end, Color.gray);
    }


    /// <summary>Actualise le tracé d'une ligne.</summary>
    ///  <param name="start">l'objet de départ</param>
    ///  <param name="end">un point d'arrivée</param>
    /// <param name="renderer">le line renderer à actualliser. </param>
    public static void UpdateLine(GameObject start, Vector3 end, LineRenderer renderer)
    {
        renderer.SetPosition(0, start.gameObject.transform.position);
        renderer.SetPosition(1, end);
    }

    /// <summary>Actualise le tracé d'une ligne.</summary>
    ///  <param name="start">l'objet de départ qui possède le line renderer</param>
    ///  <param name="end">un point d'arrivée</param>
    public static void UpdateLine(GameObject start, Vector3 end)
    {
        UpdateLine(start, end, start.GetComponent<LineRenderer>());
    }

    /// <summary>Actualise le tracé d'une ligne.</summary>
    ///  <param name="start">l'objet de départ</param>
    ///  <param name="end">l'objet d'arrivée</param>
    /// <param name="renderer">le line renderer à actualliser. </param>
    public static void UpdateLine(GameObject start, GameObject end, LineRenderer renderer)
    {
        UpdateLine(start, end.transform.position, renderer);
    }

    /// <summary>Actualise le tracé d'une ligne.</summary>
    ///  <param name="start">l'objet de départ qui possède le line renderer</param>
    ///  <param name="end">l'objet d'arrivée</param>
    public static void UpdateLine(GameObject start, GameObject end)
    {
        UpdateLine(start, end.transform.position, start.GetComponent<LineRenderer>());
    }

    /// <summary>Pour effacer une ligne.</summary>
    /// <param name="start">l'obejt possédant un line renderer</param>
    public static void EraseLine(GameObject start)
    {
        Destroy(start.GetComponent<LineRenderer>());
    }

    /// <summary> Change la couleur d'une ligne.</summary>
    /// <param name="start">l'obejt possédant un line renderer</param>
    /// <param name="color"> la nouvelle couleur de la ligne</param>
    public static void SetLineColor(GameObject start, Color color)
    {
        start.GetComponent<LineRenderer>().endColor = color;
        start.GetComponent<LineRenderer>().startColor = color;
    }


    /// <summary>Pour ajouter du texte à un gameObject centré au millieu</summary>
    /// <param name="g">L'objet qui recevra le text.</param>
    /// <param name="text"> Le texte à écrire.</param>
    public static void addText(GameObject g, string text)
    {
        GameObject ng = new GameObject("Symbol");
        ng.transform.SetParent(g.transform);
        ng.transform.localScale = new Vector3(0.008f, 0.008f, 1f);
        ng.transform.localPosition = new Vector3(0f, 0f, -10f);

        Text myText = ng.AddComponent<Text>();
        myText.alignment = TextAnchor.MiddleCenter;
        myText.color = Color.red;
        myText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        if (text.Length > 0)
            myText.fontSize = 115 / text.Length;
        else myText.fontSize = 40;
        myText.horizontalOverflow = HorizontalWrapMode.Overflow;
        myText.verticalOverflow = VerticalWrapMode.Overflow;
        myText.text = text;
    }



    /// <summary>Permet de tracer une ligne.</summary>
    ///  <param name="start">l'objet de départ</param>
    ///  <param name="end">un point d'arrivé</param>
    ///  <param name="color">la couleur de la ligne</param>
    ///  <returns>Le line renderer crée.</returns>
    public static LineRenderer DrawFil(GameObject start, GameObject end)
    {
        //On recycle le line renderer.
        LineRenderer renderer = start.GetComponent<LineRenderer>();
        if (renderer == null)
        {
            renderer = start.AddComponent<LineRenderer>();
        }
        renderer.material = new Material(Shader.Find("Sprites/Default"));
        renderer.widthMultiplier = 0.8f;
        start.GetComponent<LineRenderer>().endColor = Color.gray;
        start.GetComponent<LineRenderer>().startColor = Color.gray;

        List<Vector3> list = AlgoCable.resolve(start, end.transform.position);
        Vector3[] arr = AlgoCable.listToArray(list);
        renderer.positionCount = arr.Length;
        renderer.SetPositions(arr);

        return renderer;
    }


    /// <summary>Actualise le tracé d'une ligne.</summary>
    ///  <param name="start">l'objet de départ</param>
    ///  <param name="end">un point d'arrivée</param>
    /// <param name="renderer">le line renderer à actualliser. </param>
    public static void UpdateFil(GameObject start, GameObject end)
    {
        LineRenderer renderer = start.GetComponent<LineRenderer>();
        List<Vector3> list = AlgoCable.resolve(start, end.transform.position);
        Vector3[] arr = AlgoCable.listToArray(list);
        renderer.positionCount = arr.Length;
        renderer.SetPositions(arr);
    }

}
