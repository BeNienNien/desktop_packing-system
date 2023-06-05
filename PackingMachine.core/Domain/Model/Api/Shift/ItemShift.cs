namespace PackingMachine.core.Domain.Model.Api.Shift
{
    public class ItemShift
    {
        private Item item1;
        public Item item { get { return item1; } set { item1=value; itemId=item1.id; } }
        public string itemId { get; set; }
        public int plannedQuantity { get; set; }
        public int actualQuantity { get; set; }
        public string note { get; set; }
        public int standardWeight { get; set; }
        public ItemShift (string _itemId,int _plannedQuantity,int _actualQuantity,string _note,int _standardWeight)
        {
            itemId=_itemId;
            plannedQuantity=_plannedQuantity;
            actualQuantity=_actualQuantity;
            note=_note;
            standardWeight=_standardWeight;
        }
    }
}
