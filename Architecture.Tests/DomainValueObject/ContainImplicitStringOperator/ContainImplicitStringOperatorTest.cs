using Domain.Primitives;
using System.Reflection;

namespace Architecture.Tests.DomainValueObject.ContainImplicitStringOperator
{
    public class ContainImplicitStringOperatorTest
    {
        private readonly Assembly assembly = Assembly.GetAssembly(typeof(IValueObjectNonBinary));

        [Fact]
        public void ValueObjects_ThatShouldImplementImplicitStringOperator_Check()
        {
            //Arrange
            var allTypes = FindValueObjectsNonBinaryThatImplement(assembly);
            List<Type> types = new List<Type>();
            //Act
            foreach (var valueType in allTypes)
            {
                // Hier prüfen, ob der implizite String-Operator vorhanden ist
                var implicitOperator = valueType.GetMethod("op_Implicit", new[] { valueType });
                bool implementsReturnTypeString = implicitOperator?.ReturnType == typeof(string);
                if (implementsReturnTypeString)
                {
                    types.Add(valueType);
                }
            }

            //Assert
            Assert.True(allTypes.Any());
            Assert.Equal(allTypes.Count(), types.Count());
        }
        private static IEnumerable<Type> FindValueObjectsNonBinaryThatImplement(Assembly assembly)
        {
            var foundTypes = assembly.GetTypes()
                .Where(type => typeof(IValueObjectNonBinary).IsAssignableFrom(type) && !type.IsAbstract);

            return foundTypes;
        }
        private static IEnumerable<Type> FindValueObjectsBinaryThatImplement(Assembly assembly)
        {
            var foundTypes = assembly.GetTypes()
                .Where(type => typeof(IValueObjectBinary).IsAssignableFrom(type) && !type.IsAbstract);

            return foundTypes;
        }
    }
}
