using Moq;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;

namespace TravelPlanner.Test
{
    public static class MockHelper
    {
        public static ITable<T> BuildMockTable<T>(List<T> data) where T : class
        {
            var queryable = data.AsQueryable();

            var mock = new Mock<ITable<T>>();
            mock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            return mock.Object;
        }
    }
}
