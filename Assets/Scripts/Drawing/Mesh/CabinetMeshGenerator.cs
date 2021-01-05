using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dominion.FunctionLibrary;

public class CabinetMeshGenerator : MeshGenerator
{
    #region Basic form
    public Mesh Panel (Vector3 startP, Vector3 endP, float width, float depth)
    {
        Matrix4x4 trans = Matrix4x4.TRS(startP, Quaternion.LookRotation(endP), Vector3.one);

        Vector3 origin = Vector3.forward * endP.magnitude;
        origin = trans.MultiplyPoint(origin) * 0.5f;

        return Panel(width, depth, Vector3.Distance(startP, endP), origin, (endP - startP).normalized);
    }

    public Mesh DiagonalBoard (float width, float height, float depth, float cutDepth = 0, CabinetType cabinetType = CabinetType.L)
    {
        return DiagonalBoard(width, height, depth, Vector3.zero, Vector3.forward, cutDepth, cabinetType);
    }
    public Mesh DiagonalBoard (float width, float height, float depth, Vector3 origin, Vector3 dir, float cutDepth = 0, CabinetType cabinetType = CabinetType.L)
    {
        dir = dir == Vector3.zero ? Vector3.forward : dir;
        depth = Mathf.Clamp(depth, 0.0001f, width - 0.0001f);
        cutDepth = Mathf.Clamp(cutDepth, 0.0001f, width - 0.0001f);

        Vector3[] bottomVert =
        {
            new Vector3(-width * 0.5f, -height * 0.5f, width * 0.5f),
            new Vector3(-width * 0.5f, -height * 0.5f, (width * 0.5f - depth)),
            new Vector3((width * 0.5f - depth), -height * 0.5f, -width * 0.5f),
            new Vector3(width * 0.5f, -height * 0.5f, -width * 0.5f),
            new Vector3(width * 0.5f, -height * 0.5f, width * 0.5f - cutDepth),
            new Vector3(width * 0.5f - cutDepth, -height * 0.5f, width * 0.5f)
        };

        Vector3 transScale = cabinetType == CabinetType.L ? Vector3.one : new Vector3(-1, 1, 1);
        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(dir), Vector3.one);

        return BoardFromPoints(bottomVert, height, origin, dir, cabinetType == CabinetType.R);
    }

    public Mesh PieBoard (float width, float height, float depth, float cutDepth = 0, CabinetType cabinetType = CabinetType.L)
    {
        return PieBoard(width, height, depth, Vector3.zero, Vector3.forward, cutDepth, cabinetType);
    }
    public Mesh PieBoard (float width, float height, float depth, Vector3 origin, Vector3 dir, float cutDepth = 0, CabinetType cabinetType = CabinetType.L)
    {
        dir = dir == Vector3.zero ? Vector3.forward : dir;
        depth = Mathf.Clamp(depth, 0.0001f, width - 0.0001f);
        cutDepth = Mathf.Clamp(cutDepth, 0.0001f, width - 0.0001f);
        cutDepth = Mathf.Clamp(cutDepth, 0, width);

        Vector3 transScale = cabinetType == CabinetType.L ? Vector3.one : new Vector3(-1, 1, 1);
        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(dir, Vector3.up), transScale);

        Vector3[] bottomVert =
        {
            new Vector3(-width * 0.5f, -height * 0.5f, width * 0.5f),
            new Vector3(-width * 0.5f, -height * 0.5f, width * 0.5f - depth),
            new Vector3(width * 0.5f - depth, -height * 0.5f, width * 0.5f - depth),
            new Vector3(width * 0.5f - depth, -height * 0.5f, -width * 0.5f),
            new Vector3(width * 0.5f, - height * 0.5f, -width * 0.5f),
            new Vector3(width * 0.5f, - height * 0.5f, width * 0.5f - cutDepth),
            new Vector3(width * 0.5f - cutDepth, - height * 0.5f, width * 0.5f)
        };

        return BoardFromPoints(bottomVert, height, origin, dir, cabinetType == CabinetType.R);
    }

    public Mesh RoundBoard (float width, float height, float depth, int roundness = 10, CabinetType cabinetType = CabinetType.L)
    {
        return RoundBoard(width, height, depth, Vector3.zero, Vector3.forward, roundness, cabinetType);
    }
    public Mesh RoundBoard (float width, float height, float depth, Vector3 origin, Vector3 dir, int roundness = 10, CabinetType cabinetType = CabinetType.L)
    {
        dir = dir == Vector3.zero ? Vector3.forward : dir;

        Vector3 transScale = cabinetType == CabinetType.L ? Vector3.one : new Vector3(-1, 1, 1);
        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(dir, Vector3.up), transScale);

        Vector3[] bottomVert = new Vector3[roundness + 3];
        bottomVert[0] = new Vector3(-width, -height, depth) * 0.5f;
        bottomVert[1] = new Vector3(-width, -height, -depth) * 0.5f;
        Vector3 cornerPoint = new Vector3(width, -height, -depth) * 0.5f;
        bottomVert[bottomVert.Length - 1] = new Vector3(width, -height, depth) * 0.5f;
        for (int i = 0; i < roundness; i++)
        {
            float t = (i + 1) / ((float)roundness + 1);
            bottomVert[i + 2] = CalculateHelper.GetBezierPoint(t, bottomVert[1], cornerPoint, bottomVert[bottomVert.Length - 1]);
        }

        return BoardFromPoints(bottomVert, height, origin, dir, cabinetType == CabinetType.R);
    }

    public Mesh SoftRoundBoard (float width, float height, float depth, int roundness = 10, CabinetType cabinetType = CabinetType.L)
    {
        return SoftRoundBoard(width, height, depth, Vector3.zero, Vector3.forward, roundness, cabinetType);
    }
    public Mesh SoftRoundBoard (float width, float height, float depth, Vector3 origin, Vector3 dir, int roundness = 10, CabinetType cabinetType = CabinetType.L)
    {
        dir = dir == Vector3.zero ? Vector3.forward : dir;

        Vector3 transScale = cabinetType == CabinetType.L ? Vector3.one : new Vector3(-1, 1, 1);
        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(dir, Vector3.up), transScale);

        Vector3[] bottomVert = new Vector3[roundness + 4];
        bottomVert[0] = new Vector3(-width, -height, depth) * 0.5f;
        bottomVert[1] = new Vector3(-width, -height, -depth) * 0.5f;
        Vector3 cornerPoint = new Vector3(width, -height, -depth) * 0.5f;
        bottomVert[bottomVert.Length - 2] = new Vector3(width, -height, -depth * 0.6f) * 0.5f;
        bottomVert[bottomVert.Length - 1] = new Vector3(width, -height, depth) * 0.5f;
        for (int i = 0; i < roundness; i++)
        {
            float t = (i + 1) / ((float)roundness + 1);
            bottomVert[i + 2] = CalculateHelper.GetBezierPoint(t, bottomVert[1], cornerPoint, bottomVert[bottomVert.Length - 2]);
        }

        return BoardFromPoints(bottomVert, height, origin, dir, cabinetType == CabinetType.R);
    }

    public Mesh TriBoard (float width, float height, float depth, CabinetType cabinetType = CabinetType.L)
    {
        return TriBoard(width, height, depth, Vector3.zero, Vector3.forward, cabinetType);
    }
    public Mesh TriBoard (float width, float height, float depth, Vector3 origin, Vector3 dir, CabinetType cabinetType = CabinetType.L)
    {
        dir = dir == Vector3.zero ? Vector3.forward : dir;

        Vector3 transScale = cabinetType == CabinetType.L ? Vector3.one : new Vector3(-1, 1, 1);
        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(dir, Vector3.up), transScale);

        Vector3[] bottomVert =
        {
            new Vector3(-width, -height, -depth) * 0.5f,
            new Vector3(width, -height, depth) * 0.5f,
            new Vector3(-width, -height, depth) * 0.5f
        };

        return BoardFromPoints(bottomVert, height, origin, dir, cabinetType == CabinetType.R);
    }

    public Mesh SquareBoard (float width, float height, float depth, CabinetType cabinetType = CabinetType.L)
    {
        return SquareBoard(width, height, depth, Vector3.zero, Vector3.forward, cabinetType);
    }
    public Mesh SquareBoard (float width, float height, float depth, Vector3 origin, Vector3 dir, CabinetType cabinetType = CabinetType.L)
    {
        dir = dir == Vector3.zero ? Vector3.forward : dir;

        Vector3 transScale = cabinetType == CabinetType.L ? Vector3.one : new Vector3(-1, 1, 1);
        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(dir, Vector3.up), transScale);

        Vector3[] bottomVert =
{
            new Vector3(-width, -height, -depth) * 0.5f,
            new Vector3(width, -height, 0) * 0.5f,
            new Vector3(width, -height, depth) * 0.5f,
            new Vector3(-width, -height, depth) * 0.5f
        };

        return BoardFromPoints(bottomVert, height, origin, dir, cabinetType == CabinetType.R);
    }

    #endregion

    #region Carcass
    public Mesh Cube (float width, float height, float depth, float thick, bool isTopBlocked = false)
    {
        return Cube(width, height, depth, thick, thick, thick, Vector3.zero, Vector3.forward, isTopBlocked);
    }
    public Mesh Cube (float width, float height, float depth, float thick, Vector3 origin, Vector3 dir, bool isTopBlocked = false)
    {
        return Cube(width, height, depth, thick, thick, thick, origin, dir, isTopBlocked);
    }
    public Mesh Cube (float width, float height, float depth, float bottomThick, float sideThick, float backThick, bool isTopBlocked = false)
    {
        return Cube(width, height, depth, bottomThick, sideThick, backThick, Vector3.zero, Vector3.forward, isTopBlocked);
    }
    public Mesh Cube (float width, float height, float depth, float bottomThick, float sideThick, float backThick, Vector3 origin, Vector3 dir, bool isTopBlocked = false)
    {
        List<Mesh> panels = new List<Mesh>();

        dir = dir == Vector3.zero ? Vector3.forward : dir;
        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(dir, Vector3.up), Vector3.one);

        // Bottom
        Vector3 panelPivot = new Vector3(0, -(height * 0.5f - bottomThick * 0.5f), -backThick * 0.5f);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(Panel(width - sideThick * 2, bottomThick, depth - backThick, panelPivot, dir));

        // Sides
        panelPivot = new Vector3(-(width * 0.5f - sideThick * 0.5f), 0, 0);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(Panel(sideThick, height, depth, panelPivot, dir));

        panelPivot = new Vector3(width * 0.5f - sideThick * 0.5f, 0, 0);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(Panel(sideThick, height, depth, panelPivot, dir));

        // Back
        panelPivot = new Vector3(0, 0, depth * 0.5f - backThick * 0.5f);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(Panel(width - sideThick * 2, height, backThick, panelPivot, dir));

        //Top
        if (isTopBlocked)
        {
            panelPivot = new Vector3(0, height * 0.5f - bottomThick * 0.5f, -backThick * 0.5f);
            panelPivot = trans.MultiplyPoint(panelPivot);
            panels.Add(Panel(width - sideThick * 2, bottomThick, depth - backThick, panelPivot, dir));
        }
        else
        {
            panelPivot = new Vector3(0, height * 0.5f - bottomThick * 0.5f, -(depth - backThick) * 0.5f);
            panelPivot = trans.MultiplyPoint(panelPivot);
            panels.Add(Panel(width - sideThick * 2, bottomThick, backThick, panelPivot, dir));
        }

        return Combine(panels.ToArray());
    }

    public Mesh Diagonal (float width, float height, float depth, float thick, float cutDepth = 0, bool isTopBlocked = false, CabinetType cabinetType = CabinetType.L)
    {
        return Diagonal(width, height, depth, thick, thick, thick, Vector3.zero, Vector3.forward, cutDepth, isTopBlocked, cabinetType);
    }
    public Mesh Diagonal (float width, float height, float depth, float thick, Vector3 origin, Vector3 dir, float cutDepth = 0, bool isTopBlocked = false, CabinetType cabinetType = CabinetType.L)
    {
        return Diagonal(width, height, depth, thick, thick, thick, origin, dir, cutDepth, isTopBlocked, cabinetType);
    }
    public Mesh Diagonal (float width, float height, float depth, float bottomThick, float sideThick, float backThick, float cutDepth = 0, bool isTopBlocked = false, CabinetType cabinetType = CabinetType.L)
    {
        return Diagonal(width, height, depth, bottomThick, sideThick, backThick, Vector3.zero, Vector3.forward, cutDepth, isTopBlocked, cabinetType);
    }
    public Mesh Diagonal (float width, float height, float depth, float bottomThick, float sideThick, float backThick, Vector3 origin, Vector3 dir, float cutDepth = 0, bool isTopBlocked = false, CabinetType cabinetType = CabinetType.L)
    {
        depth = Mathf.Clamp(depth, 0, width);
        cutDepth = Mathf.Clamp(cutDepth, 0.0001f, width);

        dir = dir == Vector3.zero ? Vector3.forward : dir;
        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(dir, Vector3.up), Vector3.one);

        List<Mesh> panels = new List<Mesh>();

        //Bottom
        Vector3 panelPivot = cabinetType == CabinetType.L ?
            new Vector3((sideThick - backThick) * 0.5f, -(height - bottomThick) * 0.5f, (sideThick - backThick) * 0.5f) :
            new Vector3((backThick - sideThick) * 0.5f, -(height - bottomThick) * 0.5f, -(backThick - sideThick) * 0.5f);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(DiagonalBoard(width - (sideThick + backThick), bottomThick, depth - backThick, panelPivot, dir, cutDepth, cabinetType));

        //Sides
        panelPivot = cabinetType == CabinetType.L ?
            new Vector3((width - backThick - depth) * 0.5f, 0, -(width - sideThick) * 0.5f) :
            new Vector3(-(width - backThick - depth) * 0.5f, 0, -(width - sideThick) * 0.5f);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(Panel(depth - backThick, height, sideThick, panelPivot, dir));

        panelPivot = cabinetType == CabinetType.L ?
            new Vector3(-(width - sideThick) * 0.5f, 0, (width - backThick - depth) * 0.5f) :
            new Vector3((width - sideThick) * 0.5f, 0, (width - backThick - depth) * 0.5f);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(Panel(sideThick, height, depth - backThick, panelPivot, dir));

        //Backs
        panelPivot = cabinetType == CabinetType.L ?
            new Vector3((width - backThick) * 0.5f, 0, -cutDepth * 0.5f - backThick * 0.5f) :
            new Vector3(-(width - backThick) * 0.5f, 0, -cutDepth * 0.5f - backThick * 0.5f);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(Panel(backThick, height, width - (cutDepth + backThick), panelPivot, dir));

        panelPivot = cabinetType == CabinetType.L ?
            new Vector3(-cutDepth * 0.5f - backThick * 0.5f, 0, (width - backThick) * 0.5f) :
            new Vector3(cutDepth * 0.5f + backThick * 0.5f, 0, (width - backThick) * 0.5f);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(Panel(width - (cutDepth + backThick), height, backThick, panelPivot, dir));

        //CutBack
        Vector3[] cutBackPoints;
        if (cabinetType == CabinetType.L)
        {
            cutBackPoints = new Vector3[]
            {
                new Vector3((width/2 - (backThick + cutDepth)), -height/2, (width/2 - backThick)),
                new Vector3((width/2 - backThick), -height/2, (width/2 - (cutDepth + backThick))),
                new Vector3(width/2, -height/2, width/2 - (cutDepth + backThick)),
                new Vector3((width/2 - (backThick + cutDepth)), -height/2, width/2),
            };
        }
        else
        {
            cutBackPoints = new Vector3[]
            {
                new Vector3(-(width/2 - backThick), -height/2, (width/2 - (cutDepth + backThick))),
                new Vector3(-(width/2 - (backThick + cutDepth)), -height/2, (width/2 - backThick)),
                new Vector3(-(width/2 - (backThick + cutDepth)), -height/2, width/2),
                new Vector3(-width/2, -height/2, width/2 - (cutDepth + backThick))
            };
        }
        panels.Add(BoardFromPoints(cutBackPoints, height, origin, dir));

        //Top
        if (isTopBlocked)
        {
            panelPivot = cabinetType == CabinetType.L ?
                new Vector3((sideThick - backThick) * 0.5f, (height - bottomThick) * 0.5f, (sideThick - backThick) * 0.5f) :
                new Vector3((backThick - sideThick) * 0.5f, (height - bottomThick) * 0.5f, -(backThick - sideThick) * 0.5f);
            panelPivot = trans.MultiplyPoint(panelPivot);
            panels.Add(DiagonalBoard(width - (sideThick + backThick), bottomThick, depth - backThick, panelPivot, dir, cutDepth, cabinetType));
        }

        Vector3[] frontPoints;
        Vector3[] bottomPoints;
        if (cabinetType == CabinetType.L)
        {
            frontPoints = new Vector3[]
            {
                new Vector3(-width * 0.5f, height * 0.5f - bottomThick, width * 0.5f - depth),
                new Vector3(width*0.5f - depth, height * 0.5f - bottomThick, -width * 0.5f),
                new Vector3(width*0.5f - depth, height * 0.5f - bottomThick, -width * 0.5f + sideThick),
                new Vector3(-width * 0.5f + sideThick, height * 0.5f - bottomThick, width * 0.5f - depth),
            };
            bottomPoints = new Vector3[]
            {
                new Vector3(-width * 0.5f, -height * 0.5f, width * 0.5f - depth),
                new Vector3(width*0.5f - depth, -height * 0.5f, -width * 0.5f),
                new Vector3(width*0.5f - depth, -height * 0.5f, -width * 0.5f + sideThick),
                new Vector3(-width * 0.5f + sideThick, -height * 0.5f, width * 0.5f - depth),
            };
        }
        else
        {
            frontPoints = new Vector3[]
            {
                new Vector3(-width*0.5f + depth, height * 0.5f - bottomThick, -width * 0.5f),
                new Vector3(width * 0.5f, height * 0.5f - bottomThick, width * 0.5f - depth),
                new Vector3(width * 0.5f - sideThick, height * 0.5f - bottomThick, width * 0.5f - depth),
                new Vector3(-width*0.5f + depth, height * 0.5f - bottomThick, -width * 0.5f + sideThick),
            };
            bottomPoints = new Vector3[]
            {
                new Vector3(-width*0.5f + depth, -height * 0.5f, -width * 0.5f),
                new Vector3(width * 0.5f, -height * 0.5f, width * 0.5f - depth),
                new Vector3(width * 0.5f - sideThick, -height * 0.5f, width * 0.5f - depth),
                new Vector3(-width*0.5f + depth, -height * 0.5f, -width * 0.5f + sideThick),
            };
        }

        panels.Add(BoardFromPoints(frontPoints, bottomThick, origin, dir));
        panels.Add(BoardFromPoints(bottomPoints, bottomThick, origin, dir));


        return Combine(panels.ToArray());
    }

    public Mesh Piecut (float width, float height, float depth, float thick, float cutDepth = 0, bool isTopBlocked = false, CabinetType cabinetType = CabinetType.L)
    {
        return Piecut(width, height, depth, thick, thick, thick, Vector3.zero, Vector3.forward, cutDepth, isTopBlocked, cabinetType);
    }
    public Mesh Piecut (float width, float height, float depth, float thick, Vector3 origin, Vector3 dir, float cutDepth = 0, bool isTopBlocked = false, CabinetType cabinetType = CabinetType.L)
    {
        return Piecut(width, height, depth, thick, thick, thick, origin, dir, cutDepth, isTopBlocked, cabinetType);
    }
    public Mesh Piecut (float width, float height, float depth, float bottomThick, float sideThick, float backThick, float cutDepth = 0, bool isTopBlocked = false, CabinetType cabinetType = CabinetType.L)
    {
        return Piecut(width, height, depth, bottomThick, sideThick, backThick, Vector3.zero, Vector3.forward, cutDepth, isTopBlocked, cabinetType);
    }
    public Mesh Piecut (float width, float height, float depth, float bottomThick, float sideThick, float backThick, Vector3 origin, Vector3 dir, float cutDepth = 0, bool isTopBlocked = false, CabinetType cabinetType = CabinetType.L)
    {
        depth = Mathf.Clamp(depth, 0, width);
        cutDepth = Mathf.Clamp(cutDepth, 0.0001f, width);

        dir = dir == Vector3.zero ? Vector3.forward : dir;
        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(dir, Vector3.up), Vector3.one);

        List<Mesh> panels = new List<Mesh>();

        //Bottom
        Vector3 panelPivot = cabinetType == CabinetType.L ?
            new Vector3((sideThick - backThick) * 0.5f, -(height - bottomThick) * 0.5f, (sideThick - backThick) * 0.5f) :
            new Vector3((backThick - sideThick) * 0.5f, -(height - bottomThick) * 0.5f, -(backThick - sideThick) * 0.5f);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(PieBoard(width - (sideThick + backThick), bottomThick, depth - backThick, panelPivot, dir, cutDepth, cabinetType));

        //Sides
        panelPivot = cabinetType == CabinetType.L ?
            new Vector3((width - backThick - depth) * 0.5f, 0, -(width - sideThick) * 0.5f) :
            new Vector3(-(width - backThick - depth) * 0.5f, 0, -(width - sideThick) * 0.5f);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(Panel(depth - backThick, height, sideThick, panelPivot, dir));

        panelPivot = cabinetType == CabinetType.L ?
            new Vector3(-(width - sideThick) * 0.5f, 0, (width - backThick - depth) * 0.5f) :
            new Vector3((width - sideThick) * 0.5f, 0, (width - backThick - depth) * 0.5f);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(Panel(sideThick, height, depth - backThick, panelPivot, dir));

        //Backs
        panelPivot = cabinetType == CabinetType.L ?
            new Vector3((width - backThick) * 0.5f, 0, -cutDepth * 0.5f - backThick * 0.5f) :
            new Vector3(-(width - backThick) * 0.5f, 0, -cutDepth * 0.5f - backThick * 0.5f);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(Panel(backThick, height, width - (cutDepth + backThick), panelPivot, dir));

        panelPivot = cabinetType == CabinetType.L ?
            new Vector3(-cutDepth * 0.5f - backThick * 0.5f, 0, (width - backThick) * 0.5f) :
            new Vector3(cutDepth * 0.5f + backThick * 0.5f, 0, (width - backThick) * 0.5f);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(Panel(width - (cutDepth + backThick), height, backThick, panelPivot, dir));

        //CutBack
        Vector3[] cutBackPoints;
        if (cabinetType == CabinetType.L)
        {
            cutBackPoints = new Vector3[]
            {
                new Vector3((width/2 - (backThick + cutDepth)), -height/2, (width/2 - backThick)),
                new Vector3((width/2 - backThick), -height/2, (width/2 - (cutDepth + backThick))),
                new Vector3(width/2, -height/2, width/2 - (cutDepth + backThick)),
                new Vector3((width/2 - (backThick + cutDepth)), -height/2, width/2),
            };
        }
        else
        {
            cutBackPoints = new Vector3[]
            {
                new Vector3(-(width/2 - backThick), -height/2, (width/2 - (cutDepth + backThick))),
                new Vector3(-(width/2 - (backThick + cutDepth)), -height/2, (width/2 - backThick)),
                new Vector3(-(width/2 - (backThick + cutDepth)), -height/2, width/2),
                new Vector3(-width/2, -height/2, width/2 - (cutDepth + backThick))
            };
        }
        panels.Add(BoardFromPoints(cutBackPoints, height, origin, dir));

        //Top
        if (isTopBlocked)
        {
            panelPivot = cabinetType == CabinetType.L ?
                new Vector3((sideThick - backThick) * 0.5f, (height - bottomThick) * 0.5f, (sideThick - backThick) * 0.5f) :
                new Vector3((backThick - sideThick) * 0.5f, (height - bottomThick) * 0.5f, -(backThick - sideThick) * 0.5f);
            panelPivot = trans.MultiplyPoint(panelPivot);
            panels.Add(PieBoard(width - (sideThick + backThick), bottomThick, depth - backThick, panelPivot, dir, cutDepth, cabinetType));
        }
        else
        {
            Vector3[] frontPoints;
            if (cabinetType == CabinetType.L)
            {
                frontPoints = new Vector3[]
                {
                    new Vector3(-width * 0.5f + sideThick,height * 0.5f - bottomThick, width * 0.5f - depth),
                    new Vector3(-width * 0.5f + (width - depth),height * 0.5f - bottomThick, -width * 0.5f + (width - depth)),
                    new Vector3(width*0.5f - depth,height * 0.5f- bottomThick, -width * 0.5f + sideThick),
                    new Vector3(width*0.5f - depth + backThick,height * 0.5f- bottomThick, -width * 0.5f + sideThick),
                    new Vector3(-width * 0.5f + (width - depth) + backThick,height * 0.5f- bottomThick, -width * 0.5f + (width - depth) + backThick),
                    new Vector3(-width * 0.5f + sideThick,height * 0.5f - bottomThick, width * 0.5f - depth + backThick)
                };
            }
            else
            {
                frontPoints = new Vector3[]
                {
                    new Vector3(-width*0.5f + depth, height * 0.5f - bottomThick, -width * 0.5f + sideThick),
                    new Vector3(width * 0.5f - sideThick, height * 0.5f - bottomThick, width * 0.5f - depth),
                    new Vector3(width * 0.5f - sideThick, height * 0.5f - bottomThick, width * 0.5f - depth + backThick),
                    new Vector3(-width*0.5f + depth - backThick, height * 0.5f - bottomThick, -width * 0.5f + sideThick),
                };
            }
            panels.Add(BoardFromPoints(frontPoints, bottomThick, origin, dir));
        }

        return Combine(panels.ToArray());
    }

    public Mesh RoundEnd (float width, float height, float depth, float thick, int roundness = 10, bool isTopBlocked = false, TopType topType = TopType.round, CabinetType cabinetType = CabinetType.L)
    {
        return RoundEnd(width, height, depth, thick, thick, thick, Vector3.zero, Vector3.forward, roundness, isTopBlocked, topType, cabinetType);
    }
    public Mesh RoundEnd (float width, float height, float depth, float thick, Vector3 origin, Vector3 dir, int roundness = 10, bool isTopBlocked = false, TopType topType = TopType.round, CabinetType cabinetType = CabinetType.L)
    {
        return RoundEnd(width, height, depth, thick, thick, thick, origin, dir, roundness, isTopBlocked, topType, cabinetType);
    }
    public Mesh RoundEnd (float width, float height, float depth, float bottomThick, float sideThick, float backThcik, int roundness = 10, bool isTopBlocked = false, TopType topType = TopType.round, CabinetType cabinetType = CabinetType.L)
    {
        return RoundEnd(width, height, depth, bottomThick, sideThick, backThcik, Vector3.zero, Vector3.forward, roundness, isTopBlocked, topType, cabinetType);
    }
    public Mesh RoundEnd (float width, float height, float depth, float bottomThick, float sideThick, float backThcik, Vector3 origin, Vector3 dir, int roundness = 10, bool isTopBlocked = false, TopType topType = TopType.round, CabinetType cabinetType = CabinetType.L)
    {
        List<Mesh> panels = new List<Mesh>();

        dir = dir == Vector3.zero ? Vector3.forward : dir;
        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(dir, Vector3.up), Vector3.one);

        //Bottom
        Vector3 panelPivot = cabinetType == CabinetType.L ?
            new Vector3(sideThick * 0.5f, -(height - bottomThick) * 0.5f, -backThcik * 0.5f) :
            new Vector3(-sideThick * 0.5f, -(height - bottomThick) * 0.5f, -backThcik * 0.5f);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(RoundBoard(width - sideThick, bottomThick, depth - backThcik, panelPivot, dir, roundness, cabinetType));

        //Side
        panelPivot = cabinetType == CabinetType.L ?
            new Vector3(-(width - sideThick) * 0.5f, 0, 0) :
            new Vector3((width - sideThick) * 0.5f, 0, 0);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(Panel(sideThick, height, depth, panelPivot, dir));

        //Back
        panelPivot = cabinetType == CabinetType.L ?
            new Vector3(sideThick * 0.5f, 0, (depth - backThcik) * 0.5f) :
            new Vector3(-sideThick * 0.5f, 0, (depth - backThcik) * 0.5f);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(Panel(width - sideThick, height, backThcik, panelPivot, dir));

        //Top
        panelPivot = cabinetType == CabinetType.L ?
            new Vector3(sideThick * 0.5f, (height - bottomThick) * 0.5f, -backThcik * 0.5f) :
            new Vector3(-sideThick * 0.5f, (height - bottomThick) * 0.5f, -backThcik * 0.5f);
        panelPivot = trans.MultiplyPoint(panelPivot);

        if (isTopBlocked)
        {
            switch (topType)
            {
                case TopType.round:
                    panels.Add(RoundBoard(width - sideThick, bottomThick, depth - backThcik, panelPivot, dir, roundness, cabinetType));
                    break;
                case TopType.rect:
                    panels.Add(Panel(width - sideThick, bottomThick, depth - backThcik, panelPivot, dir));
                    break;
                case TopType.diagonal:
                    panels.Add(RoundBoard(width - sideThick, bottomThick, depth - backThcik, panelPivot, dir, 2, cabinetType));
                    break;
            }
        }

        return Combine(panels.ToArray());
    }

    public Mesh DiagonalEnd (float width, float height, float depth, float bottomThick, float sideThick, float backThcik, Vector3 origin, Vector3 dir, bool isTopBlocked = false, CabinetType cabinetType = CabinetType.L)
    {
        List<Mesh> panels = new List<Mesh>();

        dir = dir == Vector3.zero ? Vector3.forward : dir;
        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(dir, Vector3.up), Vector3.one);

        //Bottom
        Vector3 panelPivot = cabinetType == CabinetType.L ?
            new Vector3(sideThick * 0.5f, -(height - bottomThick) * 0.5f, -backThcik * 0.5f) :
            new Vector3(-sideThick * 0.5f, -(height - bottomThick) * 0.5f, -backThcik * 0.5f);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(RoundBoard(width - sideThick, bottomThick, depth - backThcik, panelPivot, dir, 2, cabinetType));

        //Side
        panelPivot = cabinetType == CabinetType.L ?
            new Vector3(-(width - sideThick) * 0.5f, 0, 0) :
            new Vector3((width - sideThick) * 0.5f, 0, 0);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(Panel(sideThick, height, depth, panelPivot, dir));

        //Back
        panelPivot = cabinetType == CabinetType.L ?
            new Vector3(sideThick * 0.5f, 0, (depth - backThcik) * 0.5f) :
            new Vector3(-sideThick * 0.5f, 0, (depth - backThcik) * 0.5f);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(Panel(width - sideThick, height, backThcik, panelPivot, dir));

        //Top
        panelPivot = cabinetType == CabinetType.L ?
            new Vector3(sideThick * 0.5f, (height - bottomThick) * 0.5f, -backThcik * 0.5f) :
            new Vector3(-sideThick * 0.5f, (height - bottomThick) * 0.5f, -backThcik * 0.5f);
        panelPivot = trans.MultiplyPoint(panelPivot);

        if (isTopBlocked)
        {
            panels.Add(RoundBoard(width - sideThick, bottomThick, depth - backThcik, panelPivot, dir, 2, cabinetType));
        }

        return Combine(panels.ToArray());
    }

    public Mesh SoftRoundEnd (float width, float height, float depth, float thick, int roundness = 10, bool isTopBlocked = false, CabinetType cabinetType = CabinetType.L)
    {
        return SoftRoundEnd(width, height, depth, thick, thick, thick, Vector3.zero, Vector3.forward, roundness, isTopBlocked, cabinetType);
    }
    public Mesh SoftRoundEnd (float width, float height, float depth, float thick, Vector3 origin, Vector3 dir, int roundness = 10, bool isTopBlocked = false, CabinetType cabinetType = CabinetType.L)
    {
        return SoftRoundEnd(width, height, depth, thick, thick, thick, origin, dir, roundness, isTopBlocked, cabinetType);
    }
    public Mesh SoftRoundEnd (float width, float height, float depth, float bottomThick, float sideThick, float backThcik, int roundness = 10, bool isTopBlocked = false, CabinetType cabinetType = CabinetType.L)
    {
        return SoftRoundEnd(width, height, depth, bottomThick, sideThick, backThcik, Vector3.zero, Vector3.forward, roundness, isTopBlocked, cabinetType);
    }
    public Mesh SoftRoundEnd (float width, float height, float depth, float bottomThick, float sideThick, float backThcik, Vector3 origin, Vector3 dir, int roundness = 10, bool isTopBlocked = false, CabinetType cabinetType = CabinetType.L)
    {
        dir = dir == Vector3.zero ? Vector3.forward : dir;
        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(dir, Vector3.up), Vector3.one);

        List<Mesh> panels = new List<Mesh>();

        //Bottom
        Vector3 panelPivot = cabinetType == CabinetType.L ?
            new Vector3(sideThick * 0.5f, -(height - bottomThick) * 0.5f, -backThcik * 0.5f) :
            new Vector3(-sideThick * 0.5f, -(height - bottomThick) * 0.5f, -backThcik * 0.5f);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(SoftRoundBoard(width - sideThick, bottomThick, depth - backThcik, panelPivot, dir, roundness, cabinetType));

        //Side
        panelPivot = cabinetType == CabinetType.L ?
            new Vector3(-(width - sideThick) * 0.5f, 0, 0) :
            new Vector3((width - sideThick) * 0.5f, 0, 0);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(Panel(sideThick, height, depth, panelPivot, dir));

        //Back
        panelPivot = cabinetType == CabinetType.L ?
            new Vector3(sideThick * 0.5f, 0, (depth - backThcik) * 0.5f) :
            new Vector3(-sideThick * 0.5f, 0, (depth - backThcik) * 0.5f);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(Panel(width - sideThick, height, backThcik, panelPivot, dir));

        //Top

        if (isTopBlocked)
        {
            panelPivot = cabinetType == CabinetType.L ?
               new Vector3(sideThick * 0.5f, (height - bottomThick) * 0.5f, -backThcik * 0.5f) :
               new Vector3(-sideThick * 0.5f, (height - bottomThick) * 0.5f, -backThcik * 0.5f);
            panelPivot = trans.MultiplyPoint(panelPivot);
            panels.Add(SoftRoundBoard(width - sideThick, bottomThick, depth - backThcik, panelPivot, dir, roundness, cabinetType));
        }

        return Combine(panels.ToArray());
    }

    public Mesh TriEnd (float width, float height, float depth, float thick, bool isTopBlocked = false, CabinetType cabinetType = CabinetType.L)
    {
        return (TriEnd(width, height, depth, thick, thick, thick, Vector3.zero, Vector3.forward, isTopBlocked, cabinetType));
    }
    public Mesh TriEnd (float width, float height, float depth, float thick, Vector3 origin, Vector3 dir, bool isTopBlocked = false, CabinetType cabinetType = CabinetType.L)
    {
        return (TriEnd(width, height, depth, thick, thick, thick, origin, dir, isTopBlocked, cabinetType));
    }
    public Mesh TriEnd (float width, float height, float depth, float bottomThick, float sideThick, float backThick, bool isTopBlocked = false, CabinetType cabinetType = CabinetType.L)
    {
        return (TriEnd(width, height, depth, bottomThick, sideThick, backThick, Vector3.zero, Vector3.forward, isTopBlocked, cabinetType));
    }
    public Mesh TriEnd (float width, float height, float depth, float bottomThick, float sideThick, float backThick, Vector3 origin, Vector3 dir, bool isTopBlocked = false, CabinetType cabinetType = CabinetType.L)
    {
        dir = dir == Vector3.zero ? Vector3.forward : dir;
        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(dir, Vector3.up), Vector3.one);

        List<Mesh> panels = new List<Mesh>();

        //Bottom
        Vector3 panelPivot = cabinetType == CabinetType.L ?
            new Vector3(sideThick * 0.5f, -(height - bottomThick) * 0.5f, -backThick * 0.5f) :
            new Vector3(-sideThick * 0.5f, -(height - bottomThick) * 0.5f, -backThick * 0.5f);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(TriBoard(width - sideThick, bottomThick, depth - backThick, panelPivot, dir, cabinetType));

        //Back
        panelPivot = cabinetType == CabinetType.L ?
            new Vector3(sideThick * 0.5f, 0, (depth - backThick) * 0.5f) :
            new Vector3(-sideThick * 0.5f, 0, (depth - backThick) * 0.5f);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(Panel(width - sideThick, height, backThick, panelPivot, dir));

        //Sided
        panelPivot = cabinetType == CabinetType.L ?
            new Vector3(-(width - sideThick) * 0.5f, 0, 0) :
            new Vector3((width - sideThick) * 0.5f, 0, 0);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(Panel(sideThick, height, depth, panelPivot, dir));

        //Top
        if (isTopBlocked)
        {
            panelPivot = cabinetType == CabinetType.L ?
            new Vector3(sideThick * 0.5f, (height - bottomThick) * 0.5f, -backThick * 0.5f) :
            new Vector3(-sideThick * 0.5f, (height - bottomThick) * 0.5f, -backThick * 0.5f);
            panelPivot = trans.MultiplyPoint(panelPivot);
            panels.Add(TriBoard(width - sideThick, bottomThick, depth - backThick, panelPivot, dir, cabinetType));
        }

        return Combine(panels.ToArray());
    }

    public Mesh SquareEnd (float width, float height, float depth, float thick, bool isTopBlocked = false, CabinetType cabinetType = CabinetType.L)
    {
        return (SquareEnd(width, height, depth, thick, thick, thick, Vector3.zero, Vector3.forward, isTopBlocked, cabinetType));
    }
    public Mesh SquareEnd (float width, float height, float depth, float thick, Vector3 origin, Vector3 dir, bool isTopBlocked = false, CabinetType cabinetType = CabinetType.L)
    {
        return (SquareEnd(width, height, depth, thick, thick, thick, origin, dir, isTopBlocked, cabinetType));
    }
    public Mesh SquareEnd (float width, float height, float depth, float bottomThick, float sideThick, float backThick, bool isTopBlocked = false, CabinetType cabinetType = CabinetType.L)
    {
        return (SquareEnd(width, height, depth, bottomThick, sideThick, backThick, Vector3.zero, Vector3.forward, isTopBlocked, cabinetType));
    }
    public Mesh SquareEnd (float width, float height, float depth, float bottomThick, float sideThick, float backThick, Vector3 origin, Vector3 dir, bool isTopBlocked = false, CabinetType cabinetType = CabinetType.L)
    {
        dir = dir == Vector3.zero ? Vector3.forward : dir;
        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(dir, Vector3.up), Vector3.one);

        List<Mesh> panels = new List<Mesh>();

        //Bottom
        Vector3 panelPivot = cabinetType == CabinetType.L ?
            new Vector3(0, -(height - bottomThick) * 0.5f, -backThick * 0.5f) :
            new Vector3(0, -(height - bottomThick) * 0.5f, -backThick * 0.5f);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(SquareBoard(width - sideThick * 2, bottomThick, depth - backThick, panelPivot, dir, cabinetType));

        //Back
        panelPivot = new Vector3(0, 0, (depth - backThick) * 0.5f);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(Panel(width - sideThick * 2, height, backThick, panelPivot, dir));

        //Sides
        panelPivot = cabinetType == CabinetType.L ?
            new Vector3(-(width - sideThick) * 0.5f, 0, 0) :
            new Vector3((width - sideThick) * 0.5f, 0, 0);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(Panel(sideThick, height, depth, panelPivot, dir));

        panelPivot = cabinetType == CabinetType.L ?
            new Vector3((width - sideThick) * 0.5f, 0, (depth - backThick) * 0.25f) :
            new Vector3(-(width - sideThick) * 0.5f, 0, (depth - backThick) * 0.25f);
        panelPivot = trans.MultiplyPoint(panelPivot);
        panels.Add(Panel(sideThick, height, (depth + backThick) * 0.5f, panelPivot, dir));


        //Top
        if (isTopBlocked)
        {
            panelPivot = cabinetType == CabinetType.L ?
            new Vector3(0, (height - bottomThick) * 0.5f, -backThick * 0.5f) :
            new Vector3(0, (height - bottomThick) * 0.5f, -backThick * 0.5f);
            panelPivot = trans.MultiplyPoint(panelPivot);
            panels.Add(SquareBoard(width - sideThick * 2, bottomThick, depth - backThick, panelPivot, dir, cabinetType));
        }

        return Combine(panels.ToArray());
    }
    #endregion

    #region Additional
    public Mesh WineRack (float width, float height, float depth, float panelW, Vector3 origin, Vector3 dir)
    {
        dir = dir == Vector3.zero ? Vector3.forward : dir;
        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(dir, Vector3.up), Vector3.one);

        List<Mesh> meshes = new List<Mesh>();

        float interval = UnitConvert.InchiToMilli(5f);
        Vector2 startPoint = new Vector2(0f, 0f);
        Vector2 endPoint = new Vector2(0f, 0f);
        int i = 0;
        while (startPoint.x <= width - interval)
        {
            startPoint.y += interval;
            endPoint.x += interval;

            if (startPoint.y >= height)
            {
                startPoint.x += startPoint.y - height;
                startPoint.y = height;
            }

            if (endPoint.x >= width)
            {
                endPoint.y += endPoint.x - width;
                endPoint.x = width;
            }

            Vector3 newSP = trans.MultiplyPoint(startPoint + new Vector2(-width, -height) * 0.5f);
            Vector3 newEP = trans.MultiplyPoint(endPoint + new Vector2(-width, -height) * 0.5f);

            Mesh newPanel = Panel(newSP, newEP, panelW, depth);
            meshes.Add(newPanel);

            newSP = new Vector3(width - startPoint.x, startPoint.y);
            newEP = new Vector3(width - endPoint.x, endPoint.y);

            newSP = trans.MultiplyPoint(newSP + new Vector3(-width, -height) * 0.5f);
            newEP = trans.MultiplyPoint(newEP + new Vector3(-width, -height) * 0.5f);

            newPanel = Panel(newSP, newEP, panelW - 0.0001f, depth);
            meshes.Add(newPanel);

            i++;

            if (i > 100)
            {
                break;
            }
        }

        return Combine(meshes.ToArray());
    }
    #endregion

    #region Helper

    #endregion

    #region Enums
    public enum TopType { round, rect, diagonal }
    public enum CabinetType { L, R }

    #endregion
}
