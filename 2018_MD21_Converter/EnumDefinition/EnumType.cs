using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roccus_MultiTool.EnumDefinition
{
    class EnumType
    {

        public enum DB2Tables
        {
            itemdisplayinfo,
            itemdisplayinfomaterialres,
            modelfiledata,
            texturefiledata
        }

        public enum Tables : UInt64
        {
            item = 1344507586,
            item_appearance = 1109793673,
            item_modified_appearance = 3834752085,
            item_sparse = 2442913102,
            item_sparse_locale,
            hotfix_data
        }

        public enum Sheath : int
        {
            TwoHand = 1,
            Staff = 2,
            OneHand = 3,
            Shield = 4,
            EnchanterRod = 5,
            OffHand = 6
        }

        public enum InventoryTypeWeapon : int
        {
            Shield = 14,
            RangedBow = 15,
            TwoHand = 17,
            MainHand = 21,
            OffHand = 22,
            HoldableTome = 23,
            Thrown = 25,
            RangedWandGuns = 26
        }

        public enum InventoryTypeArmor : int
        {
            Head = 1,
            Shoulder = 3,
            Shirt = 4,
            Chest = 5,
            Waist = 6,
            Legs = 7,
            Feet = 8,
            Wrists = 9,
            Hands = 10,
            Back = 16,
            Tabard = 19
        }

        public enum Quality : int
        {
            Poor = 0,
            Common = 1,
            Uncommon = 2,
            Rare = 3,
            Epic = 4,
            Legendary = 5,
            Artefact = 6,
            Heirloom = 7,
            Token = 8
        }

        public enum MaterialWeapon : int
        {
            Metal = 1,
            Wood = 2,
            Jewelry = 4
        }

        public enum MaterialArmor : int
        {
            Chain = 5,
            Plate = 6,
            Cloth = 7,
            Leather = 8
        }

        public enum ItemMaterialRes : int
        {
            UpperArm = 0,
            LowerArm = 1,
            Hands = 2,
            UpperTorso = 3,
            LowerTorso = 4,
            UpperLeg = 5,
            LowerLeg = 6,
            Foot = 7
        }

        public EnumType() { }
    }
}
