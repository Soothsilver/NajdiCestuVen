namespace Nsnbc.Stories
{
    public class InventoryItem
    {
        public ArtName Art { get; }
        public string Description { get; }

        public InventoryItem(ArtName art, string description)
        {
            Art = art;
            Description = description;
        }
    }
}