using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetGenerator : MonoBehaviour
{
    [SerializeField]
    public CabinetFactory factory;

    public LineCabinetInformation lineInfo;
    public CabinetSpec cabinetSpec;

    public Vector3 position;
    public Vector3 rotation;

    private void Start ()
    {
        GenerateCabinet(position, rotation);
    }

    public void GenerateCabinet(Vector3 pos, Vector3 rot)
    {
        GameObject cabinet;

        if (Enum.IsDefined(typeof(WallCabinetFactory.CabinetType), cabinetSpec.code))
        {
            WallCabinetFactory.CabinetType cabinetType;
            Enum.TryParse(cabinetSpec.code, out cabinetType);
            factory = new WallCabinetFactory().cabinet(cabinetType);
            factory.lineInfo = lineInfo;
            factory.spec = cabinetSpec;

            cabinet = factory.GenerateCabinet();
        }
        else if (Enum.IsDefined(typeof(BaseCabinetFactory.CabinetType), cabinetSpec.code))
        {
            BaseCabinetFactory.CabinetType cabinetType;
            Enum.TryParse(cabinetSpec.code, out cabinetType);
            factory = new BaseCabinetFactory().cabinet(cabinetType);
            factory.lineInfo = lineInfo;
            factory.spec = cabinetSpec;

            cabinet = factory.GenerateCabinet();
        }
        else if (Enum.IsDefined(typeof(TallCabinetFactory.CabinetType), cabinetSpec.code))
        {
            TallCabinetFactory.CabinetType cabinetType;
            Enum.TryParse(cabinetSpec.code, out cabinetType);
            factory = new TallCabinetFactory().cabinet(cabinetType);
            factory.lineInfo = lineInfo;
            factory.spec = cabinetSpec;

            cabinet = factory.GenerateCabinet();
        }
        else
        {
            cabinet = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cabinet.name = string.Format("NotExistCode({0})", cabinetSpec.code);
            MeshRenderer meshR = cabinet.GetComponent<MeshRenderer>();
            meshR.material.color = Color.black;
            Vector3 nullScale = new Vector3(cabinetSpec.width, cabinetSpec.height, cabinetSpec.height);

            cabinet.transform.localScale = nullScale;
        }

        cabinet.transform.position = pos;
        cabinet.transform.eulerAngles = rot;
    }
}
