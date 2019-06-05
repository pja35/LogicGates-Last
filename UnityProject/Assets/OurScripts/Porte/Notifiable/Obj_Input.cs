using UnityEngine;

/// <summary>
/// Gere les objets se comportant comme des inputs de porte logique.
/// </summary>
public class Obj_Input : IInOut, INotifiable
{
    /// <summary>
    /// La valeur de l'input.
    /// </summary>
    public bool value;
    /// <summary>
    /// L'eventuelle connection à un output.
    /// </summary>
    public Line connection;
    /// <summary>
    /// Texture quand l'input est à 1
    /// </summary>
    public Sprite textureOn;
    /// <summary>
    /// Texture quand l'input est à 0
    /// </summary>
    public Sprite textureOff;

    /// <summary>
    /// Change l'état de l'input (0 ou 1)
    /// </summary>
    /// <param name="newVal"></param>
    public void SetValue(bool newVal)
    {
        value = newVal;
    }

    /// <summary>
    /// Déconnecte l'input.
    /// </summary>
    public void Disconnect()
    {
        if (connection != null)
        {
            connection.GetComponent<Line>().DestroyLine();
            SetValue(false);
            gameObject.transform.parent.gameObject.GetComponent<INotifiable>().notify();
        }
    }

    /// <summary>
    /// Indique la position locale que l'input devoir occuper en Y
    /// </summary>
    public override float GetYPlacement()
    {
        return -0.5f;
    }

    /// <summary>
    /// Détruit la connexion si elle est invalide
    /// </summary>
    public void DestroyInvalidConnexion()
    {
        if (connection != null && !connection.IsPLacementValid())
        {
            Disconnect();
        }
    }

    /// <summary>
    /// Actualise le tracé du fil
    /// </summary>
    public void UpdateLineDraw()
    {
        if (connection != null)
        {
            connection.UpdateLineDraw();
        }
    }

    /// <summary>
    /// Actualise uniquement la fin du fil
    /// </summary>
    public void UpdateEndOfLine()
    {
        if (connection != null)
        {
            connection.UpdateEndOfLineAndCheckConnection();
        }
    }

    /// <summary>
    ///  Créer et configure une sortie qui sera attaché au GameObject qui lui est passée.
    /// </summary>
    /// <param name="g">le GameObject auquel va être attaché la sortie.</param>
    /// <param name="num"> la num-ième sortie connecté à g.Cela va determiner sa position automatiquement.</param>
    /// <returns></returns>
    public static Obj_Input CreateInput(GameObject g, int num)
    {
        GameObject go = (GameObject)Instantiate(Resources.Load("InputPrefab"), g.transform);
        go.name = "Input " + num;
        go.tag = "Input";

        return go.GetComponent<Obj_Input>();
    }

    /// <summary>
    /// Préviens le posseseur de l'objet d'un changement dans son input
    /// </summary>
    public void notify()
    {
        INotifiable n = transform.parent.GetComponent<INotifiable>();
        if (n == null)
        {
            LogUtil.LogError(gameObject,"Ne peut pas notifier son père, celui - ci n'as pas de " +
                "script implementant l'interface INotifiable");
            return;
        }
        n.notify();
        ChangeTexture();
    }

    private void ChangeTexture()
    {
        SpriteRenderer image = GetComponent<SpriteRenderer>();
        image.sprite = (value) ? textureOn : textureOff;
    }

    /// <summary>
    /// Déconnecte l'input puis le détruit.
    /// </summary>
    public void DestroyIn()
    {
        Disconnect();
        Destroy(gameObject);
    }


}
