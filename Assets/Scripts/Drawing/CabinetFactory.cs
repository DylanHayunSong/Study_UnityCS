using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dominion.CabinetDrawing
{
    public abstract class CabinetFactory
    {
        public LineCabinetInformation lineInfo;
        public CabinetSpec spec;
        public abstract GameObject GenerateCabinet ();

        protected float doorThick;
        protected CarcassType carcassType;

        protected CabinetMeshGenerator meshGen = new CabinetMeshGenerator();
        protected CabinetMeshGenerator.CabinetType cabinetType;
        protected CabinetMeshGenerator.TopType cabinetTopType;
        protected int roundness = 10;

        protected abstract GameObject GenerateCarcass (CarcassType type);

        /// <summary>
        /// For Cabinet Addons ex)Door, Drawer, Panel...
        /// </summary>
        /// <returns></returns>
        protected abstract GameObject CabinetAddon ();
        /// <summary>
        /// For Carcass Addons ex)Shelf, wineReck, blindPanel...
        /// <br/>Toekick is not here
        /// </summary>
        /// <returns></returns>
        protected abstract Mesh GenerateCarcassAddon ();
        protected enum CarcassType
        {
            cube,
            diagonal,
            piecut,
            roundEnd,
            diagonalEnd,
            softRoundEnd,
            triEnd,
            squareEnd
        }
        protected enum OuterType
        {
            door,
            drawer,
            hutch
        }
        protected enum CarcassAddonType
        {
            shelf,
            intervalShelf,
            blindPanel,
            wineReck,
        }

        protected GameObject GenerateDoor ()
        {
            return null;
        }

        protected GameObject GenerateDrawer ()
        {
            return null;
        }
    }

    [System.Serializable]
    public struct LineCabinetInformation
    {
        public enum CabinetFaceType { frameless, framed }
        public enum CabinetOverlayType { full, half, inset }
        public enum CabinetToekickType { sidePanel }
        public enum Unit { inchi, meter }

        public float faceStileWidth;
        public float faceRailWidth;
        public float faceOverhang;

        public float gapWallTopBottom;
        public float gapBaseTopBottom;
        public float gapCountertop;
        public float gapSide;
        public float gapBetween;

        public float toekickHeight;
        public float toekickOffset;

        public float shelfThick;

        public float caseBackThick;
        public float caseSideThick;
        public float caseBottomThick;

        public float drawerSideThick;
        public float drawerBottomThick;
        public float topDrawerHeight;

        public float seperationWidth;

        public float doorThick;

        public CabinetFaceType faceType;
        public CabinetOverlayType overlayType;
        public CabinetToekickType toekickType;
        public Unit unit;
    }
    [System.Serializable]
    public struct CabinetSpec
    {
        public enum DoorHinge { L, R, LR, T, N }

        public string productCode;
        public string drawingData;
        public string function;
        public string doorType;


        public float width;
        public float height;
        public float depth;
        public int drawerCnt;
        public int shelfCnt;
        public DoorHinge hinge;
    }
}