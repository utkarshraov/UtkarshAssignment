using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LocalPlayerController : PlayerController, IInputController {

    private PhotonView photonView;

    [SerializeField]
    private Unit selectedUnit = null;
    public Unit SelectedUnit { get { return selectedUnit; } set { selectedUnit = value; } }

    [SerializeField]
    public Map map = null;

    [SerializeField]
    public InGameUi inGameUi = null;

    [SerializeField]
    public GraphicRaycaster graphicRaycaster = null;

    [SerializeField]
    public EventSystem eventSystem = null;

    [SerializeField]
    public InputManager inputManager = null;

    public UnitEvent OnSelectedUnitChanged;

    private const int LEFT_MOUSE_BUTTON = 0;
    private const int RIGHT_MOUSE_BUTTON = 1;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    protected override void Start() {
        base.Start();
        inputManager.Add(this);
        SetSelectedUnit(SelectedUnit, true);
    }

    public bool ProcessInput() {

        // player commands
        if (photonView.isMine)
        {
            checkInput();
        }

        return true;
	}

    private void checkInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetSelectedUnit(units[0]);
        }

        if (Input.GetMouseButtonDown(LEFT_MOUSE_BUTTON) && !IsMouseOverUi())
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            bool hit = Physics.Raycast(ray, out raycastHit);
            if (hit)
            {
                Unit unit = raycastHit.collider.GetComponentInParent<Unit>();
                SetSelectedUnit(unit);
            }
            else
            {
                SetSelectedUnit(null);
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            game.InGameUi.ShowStore();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            game.App.Scenes.GoToMainMenu();
        }

        // unit commands
        if (selectedUnit != null && selectedUnit.Owner == player)
        {
            if (Input.GetMouseButtonDown(RIGHT_MOUSE_BUTTON))
            {
                Vector3 mapPos, mapNormal;
                bool hit = map.GetMapPointFromScreenPoint(Input.mousePosition, out mapPos, out mapNormal);
                if (hit)
                {
                    selectedUnit.Controller.MoveTo(mapPos);
                }
            }

            if (Input.GetKey(KeyCode.S))
            {
                selectedUnit.Controller.StopAll();
            }
        }
    }

    private void SetSelectedUnit(Unit unit, bool forceEvent = false) {
        if(unit != selectedUnit || forceEvent) {
            selectedUnit = unit;
            OnSelectedUnitChanged.Invoke(selectedUnit);

        }
    }



    public void TryCastAbility(Ability ability) {

        if(selectedUnit == null || selectedUnit.Owner != player) {
            inGameUi.ShowMessage("You don't control this unit", Color.red);
            inGameUi.Invoke("HideMessage", 3f);
            return;
        }

        string error;
        if (!ability.CanCast(out error)) {
            inGameUi.ShowMessage(error, Color.red);
            inGameUi.Invoke("HideMessage", 3f);
        } else {
            selectedUnit.GetComponent<UnitController>().StopAll();
            ability.Cast();
        }
    }

    /// <summary>
    /// Is the mouse over user interface.
    /// </summary>
    /// <returns><c>true</c>, if mouse is currently over the UI, <c>false</c> otherwise.</returns>
    bool IsMouseOverUi() {
        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, results);
        return results.Count > 0;
    }

}
