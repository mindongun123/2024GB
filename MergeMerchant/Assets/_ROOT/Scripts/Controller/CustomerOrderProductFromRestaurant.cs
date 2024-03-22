using System.Collections;
using System.Collections.Generic;
using MJGame.Library.Utility;
using MJGame.MergeMerchant.Merge;
using MJGame.MergeMerchant.Utility;
using Sirenix.OdinInspector;


namespace MJGame.MergeMerchant
{
    public class CustomerOrderProductFromRestaurant : SingletonComponent<CustomerOrderProductFromRestaurant>
    {
        public void AddOrderProductFromCustomer(Slot kSlot)
        {
            List<Slot> ls = MJGameSave.GetList<Slot>(ConstGame.SAVE_ORDER_SLOT, new List<Slot>());
            ls.Add(kSlot);
            MJGameSave.SetList<Slot>(ConstGame.SAVE_ORDER_SLOT, ls);
        }
    }
}
