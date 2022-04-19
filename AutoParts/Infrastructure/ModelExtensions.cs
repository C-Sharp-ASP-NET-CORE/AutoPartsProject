namespace AutoParts.Infrastructure
{
    using AutoParts.Core.Contract;

    public static class ModelExtensions
    {
        public static string GetInformation(this IPartModel part)
            => part.CarBrand + "-" + part.CarModel + "-" + part.Year;
    }
}