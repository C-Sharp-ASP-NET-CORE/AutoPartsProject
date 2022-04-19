namespace AutoParts.Core.Contract
{
    public interface IPartModel
    {
        string CarBrand { get; }

        string CarModel { get; }

        int Year { get; }
    }
}