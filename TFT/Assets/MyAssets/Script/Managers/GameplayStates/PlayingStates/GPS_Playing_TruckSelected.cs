﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GPS_Playing_TruckSelected : GPS_GamePlayingState
{
    public InputManager inputManager;
    string raycastTag;
    public TruckAgent truckAgent;
    public WarehouseAgent warehouseAgent;
    public override void Enter()
    {
        Debug.Log("ON PLAYING TRUCK SELECTED----------");
        inputManager = FindObjectOfType<InputManager>();
        raycastTag = "";
        inputManager.TouchAction += SelectWarehouse;
        truckAgent = m_target.truckSelected.GetComponent<TruckAgent>();


    }

    public override void Exit()
    {
        inputManager.TouchAction -= SelectWarehouse;
    }

    public override void Update()
    {
    }
    private void SelectWarehouse(Vector2 hit)
    {
        RaycastHit h;
        if (Physics.Raycast(m_target.principalARController.m_references.FirstPersonCamera.ScreenPointToRay(hit), out h))
        {
            raycastTag = h.collider.tag;
            if (raycastTag == "Truck")
            {
                Debug.Log("OTRO CAMION SELECCIONADO------------------");
                m_target.truckSelected = h.collider.gameObject;
                truckAgent.SM_GoToSelected();
            }
            else if (raycastTag == "Warehouse")
            {
                Debug.Log("CASTEANDOO WAREHOUSE--------------------");
                truckAgent.warehouseSelected = h.collider.gameObject.GetComponent<WarehouseAgent>();
                //WAREHOUSE == SELECTED
                truckAgent.SM_GoToOnWay();
                m_target.GPS_GoToPlaying_Waiting();
            }
        }

    }
}
