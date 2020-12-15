using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dominion.FunctionLibrary;

public class MeshGenerator
{
    #region Basic form

    // Bottom plane Rule : Always Left Back point is first
    // 1←←←4
    // ↓OOO↑
    // ↓OOO↑
    // 2→→→3
    //
    public Mesh Panel (float width, float height, float depth)
    {
        return Panel(width, height, depth, Vector3.zero, Vector3.forward);
    }
    public Mesh Panel (float width, float height, float depth, Vector3 origin, Vector3 dir)
    {
        Vector3[] bottomVert =
        {
            new Vector3(-width, -height, depth) * 0.5f,
            new Vector3(-width, -height, -depth) * 0.5f,
            new Vector3(width, -height, -depth) * 0.5f,
            new Vector3(width, -height, depth) * 0.5f
        };

        Vector2[] bottomUV = new Vector2[bottomVert.Length];

        int[] bottomTri =
        {
            0, 1, 2,
            0, 2, 3
        };

        for (int i = 0; i < bottomVert.Length; i++)
        {
            bottomUV[i] = new Vector2(bottomVert[i].x, bottomVert[i].z);
        }

        return BoardFromBottomPlane(bottomVert, bottomUV, bottomTri, height, origin, dir);
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

        return BoardFromPoints(bottomVert, height, origin, dir, cabinetType == CabinetType.R, cabinetType: cabinetType);
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

        return BoardFromPoints(bottomVert, height, origin, dir, cabinetType == CabinetType.R, cabinetType: cabinetType);
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

        return BoardFromPoints(bottomVert, height, origin, dir, cabinetType == CabinetType.R, cabinetType: cabinetType);
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

        return BoardFromPoints(bottomVert, height, origin, dir, cabinetType == CabinetType.R, cabinetType: cabinetType);
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

        return BoardFromPoints(bottomVert, height, origin, dir, cabinetType == CabinetType.R, cabinetType: cabinetType);
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

        return BoardFromPoints(bottomVert, height, origin, dir, cabinetType == CabinetType.R, cabinetType: cabinetType);
    }

    #endregion

    #region Cabinet carcass
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
        cutDepth = Mathf.Clamp(cutDepth, 0, width);

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
        cutDepth = Mathf.Clamp(cutDepth, 0, width);

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

    #region Else
    /// Countertop Rule
    /// (1)←←←←←←←←←(10)(4)
    /// ↓ooo(8)→→(9)(5)ooo↑
    /// ↓ooo(7)←←←←←(6)ooo↑
    /// (2)→→→→→→→→→→→→→(3)
    public Mesh CounterTop (List<Vector3> points, float height)
    {
        Vector3 origin = points[0];
        Vector3 forward = (points[points.Count - 1] - points[0]).normalized;

        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(forward, Vector3.up), Vector3.one);

        Vector2[] points2D = new Vector2[points.Count];

        for (int i = 0; i < points.Count; i++)
        {
            points2D[i] = new Vector2(points[i].x, points[i].z);
        }

        Triangulator tr = new Triangulator(points2D);
        int[] tri = tr.Triangulate();
        Vector3[] vert = new Vector3[points.Count];
        for (int i = 0; i < points.Count; ++i)
        {
            vert[i] = trans.inverse.MultiplyPoint(points[i]);
        }

        return BoardFromBottomPlane(vert.ToArray(), points2D, tri.Reverse().ToArray(), height, Vector3.zero, Vector3.forward);
    }
    public Mesh CounterTop (List<Vector3> points, Vector3 sinkOrigin, Vector3 sinkForward, Vector2 sinkSize, float height)
    {
        Vector3 origin = points[0];
        Vector3 forward = (points[points.Count - 1] - points[0]).normalized;

        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(forward, Vector3.up), Vector3.one);

        Vector3 sinkRight = Vector3.Cross(Vector3.up, sinkForward);
        Vector3[] sinkPoints =
        {
            sinkOrigin - (sinkRight * sinkSize.x/2) - (sinkForward * sinkSize.y/2),
            sinkOrigin - (sinkRight * sinkSize.x/2) + (sinkForward * sinkSize.y/2),
            sinkOrigin + (sinkRight * sinkSize.x/2) + (sinkForward * sinkSize.y/2),
            sinkOrigin + (sinkRight * sinkSize.x/2) - (sinkForward * sinkSize.y/2),
        };
        List<Vector3> vert = new List<Vector3>();
        for (int i = 0; i < sinkPoints.Length; i++)
        {
            sinkPoints[i].y = points[0].y;
        }
        Dictionary<Vector2, float> dists = new Dictionary<Vector2, float>();

        for (int i = 0; i < points.Count; i++)
        {
            for (int j = 0; j < sinkPoints.Length; j++)
            {
                dists.Add(new Vector2Int(i, j), Vector3.Distance(points[i], sinkPoints[j]));
            }
        }
        int closestPointIndex = 0;
        int sinkClosestPoint = 0;
        for (int i = 0; i < points.Count; i++)
        {
            for (int j = 0; j < sinkPoints.Length; j++)
            {
                if (Mathf.Min(dists.Values.ToArray()) == dists[new Vector2Int(i, j)])
                {
                    closestPointIndex = i;
                    sinkClosestPoint = j;
                }
            }
        }
        for (int i = 0; i < points.Count; i++)// Vector3 point in points)
        {
            if (closestPointIndex + i < points.Count)
            {
                vert.Add(points[closestPointIndex + i]);
            }
            else
            {
                vert.Add(points[closestPointIndex + i - points.Count]);
            }
        }

        Vector2[] _vert2D = new Vector2[vert.Count];

        for (int i = 0; i < vert.Count; i++)
        {
            _vert2D[i] = new Vector2(vert[i].x, vert[i].z);
        }

        Triangulator _tr = new Triangulator(_vert2D);
        int[] _tri = _tr.Triangulate();

        List<bool> contains = new List<bool>();
        for (int i = 0; i < sinkPoints.Length; i++)
        {
            List<bool> pointContain = new List<bool>();
            for (int j = 0; j + 2 < _tri.Length; j += 3)
            {
                pointContain.Add(CalculateHelper.IsContain(new Vector2(points[_tri[j]].x, points[_tri[j]].z),
                    new Vector2(points[_tri[j + 1]].x, points[_tri[j + 1]].z),
                    new Vector2(points[_tri[j + 2]].x, points[_tri[j + 2]].z),
                    new Vector2(sinkPoints[i].x, sinkPoints[i].z)));
            }
            if (pointContain.Contains(true))
            {
                contains.Add(true);
            }
            else
            {
                contains.Add(false);
            }
        }

        if (!contains.Contains(false))
        {
            Vector3 prePoint = closestPointIndex == 0 ? points[points.Count - 1] : points[closestPointIndex - 1];
            Vector3 nextPoint = closestPointIndex == points.Count + 1 ? points[0] : points[closestPointIndex + 1];
            int nextDir = (nextPoint.x - points[closestPointIndex].x > 0) || (nextPoint.z - points[closestPointIndex].z > 0) ? -1 : 1;
            int sinkisLeft = sinkPoints[sinkClosestPoint].x < points[closestPointIndex].x ? -1 : 1;
            vert.Add(points[closestPointIndex] + (prePoint - points[closestPointIndex]).normalized * 0.00001f * nextDir * sinkisLeft);

            for (int i = 0; i < sinkPoints.Length; i++)
            {
                //vert.Add(sinkPoints[i]);
                if (sinkClosestPoint + i < sinkPoints.Length)
                {
                    vert.Add(sinkPoints[sinkClosestPoint + i]);
                }
                else
                {
                    vert.Add(sinkPoints[sinkClosestPoint + i - sinkPoints.Length]);
                }
            }
            vert.Add(sinkPoints[sinkClosestPoint] - (prePoint - points[closestPointIndex]).normalized * 0.00001f * nextDir * sinkisLeft);
        }

        Vector2[] vert2D = new Vector2[vert.Count];

        for (int i = 0; i < vert.Count; i++)
        {
            vert2D[i] = new Vector2(vert[i].x, vert[i].z);
        }

        Triangulator tr = new Triangulator(vert2D);
        int[] tri = tr.Triangulate();

        for (int i = 0; i < vert.Count; ++i)
        {
            vert[i] = trans.inverse.MultiplyPoint(vert[i]);
        }

        return BoardFromBottomPlane(vert.ToArray(), vert2D, tri.Reverse().ToArray(), height, Vector3.zero, Vector3.forward);
    }
    #endregion 

    #region Helper
    private Mesh BoardFromPoints (Vector3[] points, float thick, Vector3 origin, Vector3 dir, bool reverseTri = false, bool uvRandom = false, CabinetType cabinetType = CabinetType.L)
    {
        Vector2[] uv = new Vector2[points.Length];

        for (int i = 0; i < points.Length; i++)
        {
            uv[i] = new Vector2(points[i].x, points[i].z);
        }

        Triangulator triangulator = new Triangulator(uv);
        int[] tri = triangulator.Triangulate();

        return BoardFromBottomPlane(points, uv, tri.Reverse().ToArray(), thick, origin, dir, reverseTri, uvRandom, cabinetType);
    }
    private Mesh BoardFromBottomPlane (Vector3[] bottomVert, Vector2[] bottomUV, int[] bottomTri, float thick, Vector3 origin, Vector3 dir, bool reverseTri = false, bool uvRandom = false, CabinetType cabinetType = CabinetType.L)
    {
        Vector3 transScale = cabinetType == CabinetType.L ? Vector3.one : new Vector3(-1, 1, 1);
        dir = dir == Vector3.zero ? Vector3.forward : dir;
        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(dir), transScale);

        Vector2 uvFirstPoint = uvRandom == true ? new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)) : Vector2.zero;

        Vector3[] topVert = new Vector3[bottomVert.Length];
        for (int i = 0; i < topVert.Length; i++)
        {
            topVert[i] = bottomVert[i] + Vector3.up * thick;
        }

        int n = 0;
        Vector3[] sideVert = new Vector3[bottomVert.Length * 4];
        for (int i = 0; i + 4 < sideVert.Length; i += 4)
        {
            sideVert[i] = bottomVert[n];
            sideVert[i + 1] = bottomVert[n + 1];
            sideVert[i + 2] = topVert[n + 1];
            sideVert[i + 3] = topVert[n];
            n += 1;
        }
        sideVert[sideVert.Length - 4] = bottomVert[n];
        sideVert[sideVert.Length - 3] = bottomVert[0];
        sideVert[sideVert.Length - 2] = topVert[0];
        sideVert[sideVert.Length - 1] = topVert[n];

        Vector2[] sideUv = new Vector2[sideVert.Length];

        sideUv[0] = uvFirstPoint;
        sideUv[1] = uvFirstPoint + new Vector2(Vector3.Distance(sideVert[0], sideVert[1]), 0);
        sideUv[2] = sideUv[1] + Vector2.up * thick;
        sideUv[3] = sideUv[0] + Vector2.up * thick;

        for (int i = 4; i + 4 <= sideUv.Length; i += 4)
        {
            sideUv[i] = sideUv[i - 3];
            sideUv[i + 1] = new Vector2(sideUv[i].x + Vector3.Distance(sideVert[i], sideVert[i + 1]), 0);
            sideUv[i + 2] = sideUv[i + 1] + Vector2.up * thick;
            sideUv[i + 3] = sideUv[i] + Vector2.up * thick;
        }

        int[] sideTri = new int[sideVert.Length / 2 * 3];
        n = 0;
        for (int i = 0; i + 6 <= sideTri.Length; i += 6)
        {
            sideTri[i] = n;
            sideTri[i + 1] = n + 1;
            sideTri[i + 2] = n + 2;
            sideTri[i + 3] = n;
            sideTri[i + 1 + 3] = n + 1 + 1;
            sideTri[i + 2 + 3] = n + 2 + 1;
            n += 4;
        }

        if (cabinetType == CabinetType.R)
        {
            bottomTri = bottomTri.Reverse().ToArray();
        }

        Mesh[] meshes =
        {
            GenerateMesh(bottomVert, bottomUV, bottomTri),
            GenerateMesh(sideVert, sideUv, reverseTri == true? sideTri: sideTri.Reverse().ToArray()),
            GenerateMesh(topVert, bottomUV, bottomTri.Reverse().ToArray())
        };

        Mesh newMesh = Combine(meshes);
        Vector3[] newVec = new Vector3[newMesh.vertexCount];
        for (int i = 0; i < newMesh.vertexCount; i++)
        {
            newVec[i] = trans.MultiplyPoint(newMesh.vertices[i]);
        }
        newMesh.vertices = newVec;
        newMesh.RecalculateBounds();
        newMesh.RecalculateNormals();
        newMesh.RecalculateTangents();

        return newMesh;
    }
    private Mesh GenerateMesh (Vector3[] vert, Vector2[] uv, int[] tri)
    {
        Mesh mesh = new Mesh
        {
            vertices = vert,
            uv = uv,
            triangles = tri
        };

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        return mesh;
    }
    public Mesh Combine (Mesh[] meshes)
    {
        Mesh mesh = new Mesh();

        CombineInstance[] combine = new CombineInstance[meshes.Length];

        for (int i = 0; i < meshes.Length; i++)
        {
            combine[i].mesh = meshes[i];
            combine[i].transform = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one);
        }

        mesh.CombineMeshes(combine);

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.RecalculateTangents();

        return mesh;
    }
    public Mesh Combine (GameObject[] objs)
    {
        Mesh mesh = new Mesh();

        CombineInstance[] combine = new CombineInstance[objs.Length];

        for (int i = 0; i < objs.Length; i++)
        {
            combine[i].mesh = objs[i].GetComponent<MeshFilter>().mesh;
            combine[i].transform = objs[i].transform.localToWorldMatrix;
        }

        mesh.CombineMeshes(combine);

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.RecalculateTangents();

        foreach (GameObject obj in objs)
        {
            Object.Destroy(obj);
        }

        return mesh;
    }
    public GameObject Combine (GameObject rootObj)
    {
        Vector3 basePosition = rootObj.transform.position;
        Quaternion baseRotation = rootObj.transform.rotation;
        rootObj.transform.position = Vector3.zero;
        rootObj.transform.rotation = Quaternion.identity;

        ArrayList materials = new ArrayList();
        ArrayList combineInstanceArrays = new ArrayList();
        MeshFilter[] meshFilters = rootObj.gameObject.GetComponentsInChildren<MeshFilter>();

        foreach (MeshFilter meshFilter in meshFilters)
        {
            MeshRenderer meshRenderer = meshFilter.GetComponent<MeshRenderer>();

            if (!meshRenderer ||
                !meshFilter.sharedMesh ||
                meshRenderer.sharedMaterials.Length != meshFilter.sharedMesh.subMeshCount)
            {
                continue;
            }

            for (int s = 0; s < meshFilter.sharedMesh.subMeshCount; s++)
            {
                int materialArrayIndex = -1;
                for (int i = 0; i < materials.Count; i++)
                {
                    if (((Material)materials[i]).name == meshRenderer.sharedMaterials[s].name)
                    {
                        materialArrayIndex = i;
                    }
                }
                if (materialArrayIndex == -1)
                {
                    materials.Add(meshRenderer.sharedMaterials[s]);
                    materialArrayIndex = materials.Count - 1;
                }
                combineInstanceArrays.Add(new ArrayList());

                CombineInstance combineInstance = new CombineInstance();
                combineInstance.transform = meshRenderer.transform.localToWorldMatrix;
                combineInstance.subMeshIndex = s;
                combineInstance.mesh = meshFilter.sharedMesh;
                (combineInstanceArrays[materialArrayIndex] as ArrayList).Add(combineInstance);
            }
        }

        // Get / Create mesh filter & renderer
        MeshFilter meshFilterCombine = rootObj.gameObject.GetComponent<MeshFilter>();
        if (meshFilterCombine == null)
        {
            meshFilterCombine = rootObj.gameObject.AddComponent<MeshFilter>();
        }
        MeshRenderer meshRendererCombine = rootObj.gameObject.GetComponent<MeshRenderer>();
        if (meshRendererCombine == null)
        {
            meshRendererCombine = rootObj.gameObject.AddComponent<MeshRenderer>();
        }

        // Combine by material index into per-material meshes
        // also, Create CombineInstance array for next step
        Mesh[] meshes = new Mesh[materials.Count];
        CombineInstance[] combineInstances = new CombineInstance[materials.Count];

        for (int m = 0; m < materials.Count; m++)
        {
            CombineInstance[] combineInstanceArray = (combineInstanceArrays[m] as ArrayList).ToArray(typeof(CombineInstance)) as CombineInstance[];
            meshes[m] = new Mesh();
            meshes[m].CombineMeshes(combineInstanceArray, true, true);

            combineInstances[m] = new CombineInstance();
            combineInstances[m].mesh = meshes[m];
            combineInstances[m].subMeshIndex = 0;
        }

        // Combine into one
        meshFilterCombine.sharedMesh = new Mesh();
        meshFilterCombine.sharedMesh.CombineMeshes(combineInstances, false, false);

        // Destroy other meshes
        foreach (Mesh oldMesh in meshes)
        {
            oldMesh.Clear();
            Object.DestroyImmediate(oldMesh);
        }

        // Assign materials
        Material[] materialsArray = materials.ToArray(typeof(Material)) as Material[];
        meshRendererCombine.materials = materialsArray;

        foreach (MeshFilter meshFilter in meshFilters)
        {
            Object.DestroyImmediate(meshFilter.gameObject);
        }

        rootObj.transform.position = basePosition;
        rootObj.transform.rotation = baseRotation;

        return rootObj;
    }
    #endregion

    #region Enums
    public enum TopType { round, rect, diagonal }
    public enum CabinetType { L, R }

    #endregion
}

