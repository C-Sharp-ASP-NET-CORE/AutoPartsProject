namespace AutoParts.Test.Mocks
{
    using AutoMapper;
    using Moq;

    public class MapperMock
    {
        public static IMapper Instance
        {
            get
            {
                var mapperMock = new Mock<IMapper>();

                mapperMock
                    .SetupGet(m=>m.ConfigurationProvider)
                    .Returns(Mock.Of<IConfigurationProvider>());

                return mapperMock.Object;
            }
        }
    }
}
