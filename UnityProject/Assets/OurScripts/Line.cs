using UnityEngine;

/// <summary>
/// Permet de modélier le cablage entre deux IO
/// </summary>
public class Line : MonoBehaviour, INotifiable
{

    private GameObject start;
    private GameObject end;
    private bool powered = false;


    /// <summary>
    /// On se connecte aux I/O on instantie la valeur du fil et on notifie
    /// </summary>
    /// <param name="end">La connection de fin.</param>
    public void InitLine(GameObject end)
    {
        this.start = gameObject;
        this.end = end;
        end.GetComponent<Obj_Input>().connection = this;

        DrawUtil.DrawFil(start, end);
        
        FixWire(this.GetComponent<LineRenderer>());
        
        notify();
    }

    public static void FixWire(LineRenderer wire){
        Vector3[] positions = new Vector3[wire.positionCount];
        wire.GetPositions(positions);
        for(int i =1; i < wire.positionCount-1;i++)
        {
            IAnchor anchor = GridUtil.NearestAnchorOfAnchorMat(positions[i]);
            anchor.SetFreedom(2);//2 est l'état pour 'libre pour les fils uniquement'
        }
    }
    public static void UnFixWire(LineRenderer wire){
       Vector3[] positions = new Vector3[wire.positionCount];
        wire.GetPositions(positions);
        for(int i =0; i < wire.positionCount-1;i++)
        {
            IAnchor anchor = GridUtil.NearestAnchorOfAnchorMat(positions[i]);
            anchor.SetFreedom(1);//1 est l'état pour 'libre'
        }
    }
    
    /// <summary>
    /// Vérifie que le traçé du cable est valide
    /// </summary>
    public bool IsPLacementValid()
    {
        float minDistance = GridCreater.GetDistBtwnAnchors().y;
        return start.transform.position.y < (end.transform.position.y - minDistance);
    }

    /// <summary>
    /// Actualise l'affichage du fil
    /// </summary>
    public void UpdateLineDraw()
    {
        UnFixWire(this.GetComponent<LineRenderer>());
        DrawUtil.UpdateLineDraw(start, end);
        FixWire(this.GetComponent<LineRenderer>());
    }

    /// <summary>
    /// Actualise la fin du fil et vérifie si la connexion est valide
    /// </summary>
    public void UpdateEndOfLineAndCheckConnection()
    {
        if (!IsPLacementValid())
        {
            SpriteUtil.AddCross(start);
            SpriteUtil.AddCross(end);
        }
        else
        {
            SpriteUtil.RemoveCross(start);
            SpriteUtil.RemoveCross(end);
            DrawUtil.UpdateEndOfLine(start, end);
        }
    }

    /// <summary>
    /// En cas de changement de l'entrée le fil actualise la valeur de sa sortie et change sa couleur.
    /// </summary>
    public void notify()
    {
        powered = start.GetComponent<Obj_Output>().value;
        end.GetComponent<Obj_Input>().SetValue(powered);
        UpdateLineColor();

        DrawUtil.UpdateLineDraw(start, end);
        end.GetComponent<INotifiable>().notify();
    }

    private void UpdateLineColor()
    {
        if (powered)
        {
            DrawUtil.SetLineColor(gameObject, Color.green);
        }
        else
        {
            DrawUtil.SetLineColor(gameObject, Color.red);
        }
    }

    /// <summary>
    /// Détruit le fil et actualise l'état du début et de la fin
    /// </summary>
    public void DestroyLine()
    {
        UnFixWire(this.GetComponent<LineRenderer>());
        SpriteUtil.RemoveCross(start);
        SpriteUtil.RemoveCross(end);
        end.GetComponent<Obj_Input>().value = false;
        end.GetComponent<Obj_Input>().notify();
        DrawUtil.EraseLine(gameObject);
        Destroy(this);
    }

    void OnDestroy()
    {
        DestroyLine();
    }
}
