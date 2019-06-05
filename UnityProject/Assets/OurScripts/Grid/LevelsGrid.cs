using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>
/// Génère un ecran de chargement de niveau.
/// </summary>
public class LevelsGrid : MonoBehaviour
{
    /// <summary>
    /// ed. Nombre de colones du tableau de niveaux.
    /// </summary>
    public int nbCols = 2;
    /// <summary>
    /// ed. Nombre de lignes du tableau de niveaux.
    /// </summary>
    public int nbRows = 4;

    /// <summary>
    /// ed. Modifie l'espace disponible pour placer les boutons et autres elements
    /// </summary>
    public int paddingLeft = 150;
    /// <summary>
    /// ed. Modifie l'espace disponible pour placer les boutons et autres elements
    /// </summary>
    public int paddingRight = 150;
    /// <summary>
    /// ed. Modifie l'espace disponible pour placer les boutons et autres elements
    /// </summary>
    public int paddingBottom = 200;
    /// <summary>
    /// ed. Modifie l'espace disponible pour placer les boutons et autres elements
    /// </summary>
    public int paddingTop = 100;

    /// <summary>
    /// ed. Police d'ecriture pour les boutons de niveau
    /// </summary>
    public Font myFont;


    /// <summary>
    /// Au démarage on crée les panneaux et on y place les boutons.
    /// </summary>
    public void Start()
    {
        //On calcule le nombre de panneau à creer en fonction du nombre de niveau éxistants dans le jeu
        int nbPanels = Mathf.CeilToInt((float)ParametersLoader.GetNbLevels() / (nbCols * nbRows));
        int nbLevelsPanel = nbCols * nbRows;
        //Nombre de panneaux restants à placer
        int toplace = ParametersLoader.GetNbLevels();
        int totalPlaced = 0;

        GameObject[] panels = new GameObject[nbPanels];

        for (int panelAct = 0; panelAct < nbPanels; panelAct++)
        {
           panels[panelAct] = (GameObject)Instantiate(Resources.Load("Panel"));
        }

        for (int panelAct = 0; panelAct < nbPanels; panelAct++)
        {
            RectTransform panelTransform = panels[panelAct].GetComponent<RectTransform>();

            panelTransform.SetParent(gameObject.transform);

            //Permet de faire que le panel fasse la même taille que son père.
            panelTransform.sizeDelta = new Vector3(0, 0, 0);
            panelTransform.localScale = new Vector3(1, 1, 1);

            PlaceButtonsOnPanel(Math.Min(toplace - totalPlaced, nbLevelsPanel), panelTransform, totalPlaced);
            totalPlaced += nbLevelsPanel;

            PlaceBottomButtons(panels, nbPanels, panelAct);
           
        }


    }

    private void PlaceButtonsOnPanel(int nbButton, Transform panel, int first)
    {
        RectTransform parent = gameObject.GetComponentInParent<RectTransform>();
        //Taille du panneau supportant les boutons.
        Vector3 delta_Size = parent.sizeDelta / 2;
        Vector3 UpLeft = new Vector3(-delta_Size.x + paddingLeft, delta_Size.y - paddingTop, 0);

        //La distance entre chaque bouton en y:row et x:col
        float dstBtwnRows = (parent.sizeDelta.y - paddingBottom - paddingTop) / (nbRows - 1);
        float dstBtwnCol = (parent.sizeDelta.x - paddingLeft - paddingRight) / (nbCols - 1);

        int placed = 0;
        for (int rowAct = 0; rowAct < nbRows; rowAct++)
        {
            for (int colAct = 0; colAct < nbCols && placed < nbButton; colAct++, placed++)
            {
                //On charge un préfab de bouton
                GameObject button = (GameObject)Instantiate(Resources.Load("Button"));

                //Placement du bouton
                Vector3 buttonPosition = new Vector3(UpLeft.x + dstBtwnCol * colAct, UpLeft.y - dstBtwnRows * rowAct, 0);
                PlaceButtonInPanel(button, panel, buttonPosition);

                int buttonNumber = first + placed;
                SetButtonAppearence(button, buttonNumber);
            }
        }

    }

    private void PlaceButtonInPanel(GameObject button, Transform panel, Vector3 buttonPosition)
    {
        button.transform.SetParent(panel);
        button.transform.localPosition = buttonPosition;
        button.transform.localScale = new Vector3(1, 1, 1);
    }

    private void SetButtonAppearence(GameObject button, int buttonNumber)
    {
        int levelsUnlocked = ParametersLoader.GetUnlockedLevels();
        button.GetComponentInChildren<Text>().text = "" + buttonNumber;
        button.GetComponentInChildren<Text>().font = myFont;
        SetButtonColor(button, buttonNumber, levelsUnlocked);
    }

    private void SetButtonColor(GameObject button, int numButon, int nbUnlocked)
    {
        if (numButon > nbUnlocked)
        {
            button.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            ButtonUtil util = button.AddComponent<ButtonUtil>();

            button.GetComponent<Button>().onClick.AddListener(delegate { util.StartLevelTheme(); util.LoadScene("Niveau" + numButon); });
        }
    }

    private void PlaceBottomButtons(GameObject[] panels, int nbPanels, int panelAct)
    {
        //Premier panneau: button home + suivant si existe
        if (panelAct == 0)
        {
            ButtonsForFirstPanel(panels, nbPanels, panelAct);
        }
        //Dernier panneau si plus de un panneau: bouton panneau précédent
        else if (panelAct == nbPanels - 1 && nbPanels != 1)
        {
            ButtonsForNormalPanel(panels, panelAct);
        }
        //Panneau avec précédent et suivant.
        else
        {
            ButtonsForLastPanel(panels, panelAct);
        }
    }

    private void ButtonsForFirstPanel(GameObject[] panels, int nbPanels, int panelAct)
    {
        PlaceOneBottomButton(panels[panelAct].transform, true, "ButtonMenu", null);
        if (nbPanels > 1)
        {
            PlaceOneBottomButton(panels[panelAct].transform, false, "NextPanel", panels[panelAct + 1]);
        }
    }

    private void ButtonsForNormalPanel(GameObject[] panels, int panelAct)
    {
        panels[panelAct].SetActive(false);
        PlaceOneBottomButton(panels[panelAct].transform, true, "PrevPanel", panels[panelAct - 1]);
    }

    private void ButtonsForLastPanel(GameObject[] panels, int panelAct)
    {
        panels[panelAct].SetActive(false);
        PlaceOneBottomButton(panels[panelAct].transform, true, "PrevPanel", panels[panelAct - 1]);
        PlaceOneBottomButton(panels[panelAct].transform, false, "NextPanel", panels[panelAct + 1]);
    }

    private void PlaceOneBottomButton(Transform panel, bool left, string buttonName, GameObject nextPanel)
    {
        //Pour placer le boutton en bas à gauche(droite) du panel qui le contient.
        RectTransform parent = gameObject.GetComponentInParent<RectTransform>();
        //La taille du panel
        Vector3 delta_Size = parent.sizeDelta / 2;

        GameObject button = (GameObject)Instantiate(Resources.Load(buttonName));
        button.transform.SetParent(panel);

        //Pour placer le bouton non pas par son milleu mais par un de ses coin
        Vector3 buttonDelta = button.GetComponent<RectTransform>().sizeDelta / 2;

        Vector3 buttonPos;
        if (left)
        {
            buttonPos = new Vector3(-delta_Size.x + buttonDelta.x, -delta_Size.y + buttonDelta.y, 0);
        }
        else
        {
            buttonPos = new Vector3(delta_Size.x - buttonDelta.x, -delta_Size.y + buttonDelta.y, 0);
        }

        if (nextPanel != null)
        {
            ButtonUtil util = button.AddComponent<ButtonUtil>();
            button.GetComponent<Button>().onClick.AddListener(delegate { util.ChangePanel(nextPanel); });
        }

        button.transform.localScale = new Vector3(1, 1, 1);
        button.transform.localPosition = buttonPos;
    }
}
