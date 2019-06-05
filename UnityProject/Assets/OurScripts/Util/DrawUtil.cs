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
    public static void AddText(GameObject g, string text)
    {
        GameObject ng = new GameObject("Symbol");
        ng.transform.SetParent(g.transform);
        ng.transform.localScale = new Vector3(0.008f, 0.008f, 1f);
        ng.transform.localPosition = new Vector3(0f, 0f, -10f);

        Text myText = ng.AddComponent<Text>();
        myText.alignment = TextAnchor.MiddleCenter;
        myText.color = Color.red;
        Font myFont = Resources.Load("Roboto-Regular", typeof(Font)) as Font;
        myText.font = myFont;
        if (text.Length > 1)
        {
            myText.fontSize = 145 / text.Length;
        }     
        else myText.fontSize = 100;
        myText.horizontalOverflow = HorizontalWrapMode.Overflow;
        myText.verticalOverflow = VerticalWrapMode.Overflow;
        myText.text = text;
    }

    /// <summary>Permet de tracer une ligne.</summary>
    ///  <param name="start">l'objet de départ</param>
    ///  <param name="end">un point d'arrivé</param>
    ///  <returns>Le line renderer crée.</returns>
    public static LineRenderer DrawFil(GameObject start, GameObject end)
    {
        //On recycle le line renderer.
        LineRenderer renderer = AddLineRenderer(start);
        renderer.material = new Material(Shader.Find("Sprites/Default"));
        renderer.widthMultiplier = 0.8f;
        start.GetComponent<LineRenderer>().endColor = Color.gray;
        start.GetComponent<LineRenderer>().startColor = Color.gray;

        List<IAnchor> list = AlgoCable.Resolve(start, end.transform.position);
        IAnchor[] arranc = list.ToArray();
        Vector3[] arrpos = AncArrayToPosArray(arranc);
        renderer.positionCount = arrpos.Length;
        renderer.SetPositions(arrpos);

        return renderer;
    }
    

    private static LineRenderer AddLineRenderer(GameObject holder)
    {
        LineRenderer renderer = holder.GetComponent<LineRenderer>();
        if (renderer == null)
        {
            renderer = holder.AddComponent<LineRenderer>();
        }
        return renderer;
    }

    /// <summary>Actualise le tracé d'une ligne.</summary>
    ///  <param name="start">l'objet de départ</param>
    ///  <param name="end">un point d'arrivée</param>
    /// <param name="renderer">le line renderer à actualliser. </param>
    public static void UpdateLineDraw(GameObject start, GameObject end)
    {
        LineRenderer renderer = start.GetComponent<LineRenderer>();
        List<IAnchor> list = AlgoCable.Resolve(start, end.transform.position);
        IAnchor[] arranc = list.ToArray();
        Vector3[] arrpos = AncArrayToPosArray(arranc);
        renderer.positionCount = arrpos.Length;
        renderer.SetPositions(arrpos);
    }

    private static Vector3[] AncArrayToPosArray(IAnchor[] arranc){
        List<Vector3> positions = new List<Vector3>(); 
        foreach (IAnchor anc in arranc)
        {
            //anc.SetFreedom(2);
            positions.Add(PosOnTop(anc.GetPosition()));
        }
        return positions.ToArray();
    }

    private static Vector3 PosOnTop(Vector3 position)
    {
        return position + new Vector3(0, 0, -10);
    }

    /// <summary>
    /// Actualise uniquement le dernier trait d'un fil
    /// </summary>
    /// <param name="start">Départ du fil</param>
    /// <param name="end">Fin du fil</param>
    public static void UpdateEndOfLine(GameObject start, GameObject end)
    {
        LineRenderer renderer = start.GetComponent<LineRenderer>();
        Vector3 last = renderer.GetPosition(renderer.positionCount - 1);
        IAnchor anchor = GridUtil.FindNearestAnchor(end);
        if (anchor.GetPosition().x != last.x && anchor.GetPosition().y != last.y)
        {
            renderer.SetPosition(renderer.positionCount - 1, anchor.GetPosition() + new Vector3(0, 0, -10));
        }
        else
        {
            renderer.SetPosition(0, start.transform.position + new Vector3(0, 0, -10));

        }
    }

}
