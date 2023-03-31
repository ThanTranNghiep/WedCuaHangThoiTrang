namespace KNT_Shop.Models.ViewModel
{
    public class GenderAccount
    {
        public Gender Gender { get; set; }
    }
    public enum Gender
    {
        Male,
        Female,
        Other
    }
}