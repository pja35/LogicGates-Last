using UnityEngine;

/// <summary>
/// Gere les objets se comportant comme des inputs de porte logique.
/// </summary>
public class Obj_Input : MonoBehaviour, Notifiable
{
    /// <summary>
    /// La valeur de l'input.
    /// </summary>
    public bool value;
    /// <summary>
    /// L'eventuelle connection à un output.
    /// </summary>
    public Fils connection;

    public void setValue(bool newVal)
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
            connection.GetComponent<Fils>().Destroy_Fils();
            setValue(false);
            gameObject.transform.parent.gameObject.GetComponent<Notifiable>().notify();
        }
    }


    /// <summary>
    ///  Créer et configure une sortie qui sera attaché au GameObject qui lui est passée.
    /// </summary>
    /// <param name="g">le GameObject auquel va être attaché la sortie.</param>
    /// <param name="num"> la num-ième sortie connecté à g.Cela va determiner sa position automatiquement.</param>
    /// <returns></returns>
    public static Obj_Input createInput(GameObject g, int num)
    {
        GameObject go = (GameObject)Instantiate(Resources.Load("InputPrefab"), g.transform);
        go.transform.localPosition = new Vector3(num * 1.5f, -1f, 0f);
        go.name = "Input " + num;
        go.tag = "Input";

        Obj_Input input = (Obj_Input)go.GetComponent(typeof(Obj_Input));

        return input;
    }

    /// <summary>
    /// Préviens le posseseur de l'objet d'un changement dans son input
    /// </summary>
    public void notify()
    {
        Debug.Log("Input " + gameObject);
        Notifiable n = gameObject.transform.parent.gameObject.GetComponent<Notifiable>();
        if(n == null) { Debug.Log(gameObject + " " + gameObject.transform.parent.gameObject);return; }
        n.notify();
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
