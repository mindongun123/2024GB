using System;
using System.Collections.Generic;


namespace MJGame.MergeMerchant.Utility
{
    public static class ConvertList
    {
        public static List<Slot> MJConvertToSlotOBList(this List<SlotData> slots)
        {
            List<Slot> slotOBList = new List<Slot>();

            foreach (var slot in slots)
            {
                slotOBList.Add(new Slot(slot));
            }

            return slotOBList;
        }

        public static List<SlotData> MJConvertToSlotList(this List<Slot> slotOBList)
        {
            List<SlotData> slots = new List<SlotData>();

            foreach (var slotOB in slotOBList)
            {
                slots.Add(slotOB.GetSlot());
            }

            return slots;
        }
    }

}
