
using System;

namespace Dominion.Enums
{
    public enum SceneType
    {
        MAIN,
        MEASURE,
        VISUALIZATION,
        END
    }

    public enum SpaceType
    {
        Kitchen,
        LivingRoom,
        Bathroom,
        Bedroom,
        Lobby,
        Hall,
        DiningRoom,
        Pantry,
        Studyroom,
        DressingRoom,
        Laundryroom,
        HomeAppliance,
        Office,
        End
    }

    namespace Design
    {
        public enum DesignCandidate
        {
            PLAN1, PLAN2, PLAN3, END
        };
        public enum Type_Material
        {
            /* only for prototype */
            PROTO_PLAN1_DOOR_LOW,
            PROTO_PLAN1_DOOR_MEDIUM,
            PROTO_PLAN1_DOOR_HIGH,
            PROTO_PLAN1_COUNTERTOP,
            PROTO_PLAN2_DOOR_LOW,
            PROTO_PLAN2_DOOR_MEDIUM,
            PROTO_PLAN2_DOOR_HIGH,
            PROTO_PLAN2_COUNTERTOP,
            PROTO_PLAN3_DOOR_LOW,
            PROTO_PLAN3_DOOR_MEDIUM,
            PROTO_PLAN3_DOOR_HIGH,
            PROTO_PLAN3_COUNTERTOP_BEFORE,
            PROTO_PLAN3_COUNTERTOP_AFTER,
            PROTO_PLAN3_COUNTERTOP_OPTION1,
            PROTO_PLAN3_COUNTERTOO_OPTION2,
            /* end */

            Type_Wood, Type_DarkGrey, Type_White, Type_Dark, Type_Pink, Type_Marble, Type_Marble2, Type_Marble3, End
        }

        public enum Type_Door
        {
            PROTO_PLAN1, PROTO_PLAN1_SUB1, PROTO_PLAN1_SUB2, PROTO_PLAN1_SUB3,
            PROTO_PLAN2, PROTO_PLAN2_SUB1, PROTO_PLAN2_SUB2, PROTO_PLAN2_SUB3,
            PROTO_PLAN3, PROTO_PLAN3_SUB1, PROTO_PLAN3_SUB2, PROTO_PLAN3_SUB3,
            PROTO_BTYPE, PROTO_BTYPE_SUB1, PROTO_BTYPE_SUB2, PROTO_BTYPE_SUB3,
            PROTO_BTYPE2, PROTO_BTYPE2_SUB1, PROTO_BTYPE2_SUB2, PROTO_BTYPE2_SUB3,
            PROTO_BTYPE3, PROTO_BTYPE3_SUB1, PROTO_BTYPE3_SUB2, PROTO_BTYPE3_SUB3,

            Type_Flat, Type_Shaker, Type3, Type4, Type5, Type6, End
        }

        public enum Type_CounterTop
        {
            PROTO_PLAN1, PROTO_PLAN1_SUB1, PROTO_PLAN1_SUB2, PROTO_PLAN1_SUB3,
            PROTO_PLAN2, PROTO_PLAN2_SUB1, PROTO_PLAN2_SUB2, PROTO_PLAN2_SUB3,
            PROTO_PLAN3, PROTO_PLAN3_SUB1, PROTO_PLAN3_SUB2, PROTO_PLAN3_SUB3,
            PROTO_BTYPE, PROTO_BTYPE_SUB1, PROTO_BTYPE_SUB2, PROTO_BTYPE_SUB3,
            PROTO_BTYPE2, PROTO_BTYPE2_SUB1, PROTO_BTYPE2_SUB2, PROTO_BTYPE2_SUB3,
            PROTO_BTYPE3, PROTO_BTYPE3_SUB1, PROTO_BTYPE3_SUB2, PROTO_BTYPE3_SUB3,
        }

        public enum Type_CounterTopModifyPart
        {
            TYPE_CABINET, TYPE_ISLAND, END
        }

        public enum Type_Cooktop
        {
            PROTO_PLAN1, PROTO_PLAN1_SUB1, PROTO_PLAN1_SUB2, PROTO_PLAN1_SUB3,
            PROTO_PLAN2, PROTO_PLAN2_SUB1, PROTO_PLAN2_SUB2, PROTO_PLAN2_SUB3,
            PROTO_PLAN3, PROTO_PLAN3_SUB1, PROTO_PLAN3_SUB2, PROTO_PLAN3_SUB3,
            PROTO_BTYPE, PROTO_BTYPE_SUB1, PROTO_BTYPE_SUB2, PROTO_BTYPE_SUB3,
            PROTO_BTYPE2, PROTO_BTYPE2_SUB1, PROTO_BTYPE2_SUB2, PROTO_BTYPE2_SUB3,
            PROTO_BTYPE3, PROTO_BTYPE3_SUB1, PROTO_BTYPE3_SUB2, PROTO_BTYPE3_SUB3,


            Type1, Type2, Type3, Type4, End
        }

        public enum Type_Hood
        {
            PROTO_PLAN1, PROTO_PLAN1_SUB1, PROTO_PLAN1_SUB2, PROTO_PLAN1_SUB3,
            PROTO_PLAN2, PROTO_PLAN2_SUB1, PROTO_PLAN2_SUB2, PROTO_PLAN2_SUB3,
            PROTO_PLAN3, PROTO_PLAN3_SUB1, PROTO_PLAN3_SUB2, PROTO_PLAN3_SUB3,
            PROTO_BTYPE, PROTO_BTYPE_SUB1, PROTO_BTYPE_SUB2, PROTO_BTYPE_SUB3,
            PROTO_BTYPE2, PROTO_BTYPE2_SUB1, PROTO_BTYPE2_SUB2, PROTO_BTYPE2_SUB3,
            PROTO_BTYPE3, PROTO_BTYPE3_SUB1, PROTO_BTYPE3_SUB2, PROTO_BTYPE3_SUB3,

            Type1, Type2, Type3, Type4, Type5, Type6, Type7, Type8, End
        }

        public enum Type_Sink
        {
            PROTO_PLAN1, PROTO_PLAN1_SUB1, PROTO_PLAN1_SUB2, PROTO_PLAN1_SUB3,
            PROTO_PLAN2, PROTO_PLAN2_SUB1, PROTO_PLAN2_SUB2, PROTO_PLAN2_SUB3,
            PROTO_PLAN3, PROTO_PLAN3_SUB1, PROTO_PLAN3_SUB2, PROTO_PLAN3_SUB3,
            PROTO_BTYPE, PROTO_BTYPE_SUB1, PROTO_BTYPE_SUB2, PROTO_BTYPE_SUB3,
            PROTO_BTYPE2, PROTO_BTYPE2_SUB1, PROTO_BTYPE2_SUB2, PROTO_BTYPE2_SUB3,
            PROTO_BTYPE3, PROTO_BTYPE3_SUB1, PROTO_BTYPE3_SUB2, PROTO_BTYPE3_SUB3,

            Type1, Type2, Type3, End
        }

        public enum Type_Faucet
        {
            PROTO_PLAN1, PROTO_PLAN1_SUB1, PROTO_PLAN1_SUB2, PROTO_PLAN1_SUB3,
            PROTO_PLAN2, PROTO_PLAN2_SUB1, PROTO_PLAN2_SUB2, PROTO_PLAN2_SUB3,
            PROTO_PLAN3, PROTO_PLAN3_SUB1, PROTO_PLAN3_SUB2, PROTO_PLAN3_SUB3,
            PROTO_BTYPE, PROTO_BTYPE_SUB1, PROTO_BTYPE_SUB2, PROTO_BTYPE_SUB3,
            PROTO_BTYPE2, PROTO_BTYPE2_SUB1, PROTO_BTYPE2_SUB2, PROTO_BTYPE2_SUB3,
            PROTO_BTYPE3, PROTO_BTYPE3_SUB1, PROTO_BTYPE3_SUB2, PROTO_BTYPE3_SUB3,

            Type1, Type2, Type3, Type4, End
        }

        public enum Type_Refrigerator
        {
            PROTO_PLAN1, PROTO_PLAN1_SUB1, PROTO_PLAN1_SUB2, PROTO_PLAN1_SUB3,
            PROTO_PLAN2, PROTO_PLAN2_SUB1, PROTO_PLAN2_SUB2, PROTO_PLAN2_SUB3,
            PROTO_PLAN3, PROTO_PLAN3_SUB1, PROTO_PLAN3_SUB2, PROTO_PLAN3_SUB3,
            PROTO_BTYPE, PROTO_BTYPE_SUB1, PROTO_BTYPE_SUB2, PROTO_BTYPE_SUB3,
            PROTO_BTYPE2, PROTO_BTYPE2_SUB1, PROTO_BTYPE2_SUB2, PROTO_BTYPE2_SUB3,
            PROTO_BTYPE3, PROTO_BTYPE3_SUB1, PROTO_BTYPE3_SUB2, PROTO_BTYPE3_SUB3,
            End
        }

        public enum Type_Dishwasher
        {
            PROTO_PLAN1, PROTO_PLAN1_SUB1, PROTO_PLAN1_SUB2, PROTO_PLAN1_SUB3,
            PROTO_PLAN2, PROTO_PLAN2_SUB1, PROTO_PLAN2_SUB2, PROTO_PLAN2_SUB3,
            PROTO_PLAN3, PROTO_PLAN3_SUB1, PROTO_PLAN3_SUB2, PROTO_PLAN3_SUB3,
            PROTO_BTYPE, PROTO_BTYPE_SUB1, PROTO_BTYPE_SUB2, PROTO_BTYPE_SUB3,
            PROTO_BTYPE2, PROTO_BTYPE2_SUB1, PROTO_BTYPE2_SUB2, PROTO_BTYPE2_SUB3,
            PROTO_BTYPE3, PROTO_BTYPE3_SUB1, PROTO_BTYPE3_SUB2, PROTO_BTYPE3_SUB3,
            End
        }

        public enum Type_Oven
        {
            PROTO_PLAN1, PROTO_PLAN1_SUB1, PROTO_PLAN1_SUB2, PROTO_PLAN1_SUB3,
            PROTO_PLAN2, PROTO_PLAN2_SUB1, PROTO_PLAN2_SUB2, PROTO_PLAN2_SUB3,
            PROTO_PLAN3, PROTO_PLAN3_SUB1, PROTO_PLAN3_SUB2, PROTO_PLAN3_SUB3,
            PROTO_BTYPE, PROTO_BTYPE_SUB1, PROTO_BTYPE_SUB2, PROTO_BTYPE_SUB3,
            PROTO_BTYPE2, PROTO_BTYPE2_SUB1, PROTO_BTYPE2_SUB2, PROTO_BTYPE2_SUB3,
            PROTO_BTYPE3, PROTO_BTYPE3_SUB1, PROTO_BTYPE3_SUB2, PROTO_BTYPE3_SUB3,
            End
        }

        [System.Flags]
        public enum DesignResultViewMode
        {
            None = 0,
            Normal = 1,
            FullScreen = 2,
            VR = 4,
            Gyro = 8,
            Edit = 16,
            End = 32,
        }

        public enum Type_GradeDoorType
        {
            LOW, MEDIUM, HIGH, END
        }

        public enum Type_DesignItem
        {
            DOOR, COUNTERTOP, REFRIGERATOR, SINK, FAUCET, DISHWASHER, COOKTOP, HOOD, OVEN, END
        }

        public enum DesignModifyPhase
        {
            None, ShowPossibleObj, ShowAlternatives
        }
    }

}
