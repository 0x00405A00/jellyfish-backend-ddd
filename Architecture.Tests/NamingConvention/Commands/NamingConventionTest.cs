using Application.Abstractions.Messaging;
using FluentAssertions;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace Architecture.Tests.NamingConvention.Commands
{
    public class NamingConventionTest
    {
        private readonly Assembly assembly = Assembly.GetAssembly(typeof(Application.Abstractions.Messaging.ICommandHandler<>));

        public NamingConventionTest()
        {
            

        }

        [Fact]
        public void Commands_NameShouldEndWithCommand_Check()
        {
            //Arrange
            var allTypes = FindCommandTypes(assembly);

            //Act
            var allTypesWithCorrectNamingConvention = allTypes.Where(x => x.Name.EndsWith("Command"))
                .ToList();

            //Assert
            Assert.True(allTypes.Any());
            Assert.Equal(allTypes.Count(), allTypesWithCorrectNamingConvention.Count());
        }

        [Fact]
        public void Queries_NameShouldEndWithQuery_Check()
        {
            //Arrange
            var allTypes = FindQueryTypes(assembly);

            //Act
            var allTypesWithCorrectNamingConvention = allTypes.Where(x => x.Name.EndsWith("Query"))
                .ToList();

            //Assert
            Assert.True(allTypes.Any());
            Assert.Equal(allTypes.Count(), allTypesWithCorrectNamingConvention.Count());
        }

        [Fact]
        public void CqrsHandlers_NameShouldEndWithHandler_Check()
        {
            //Arrange
            var allTypes = FindAllHandler(assembly);

            //Act
            var allTypesWithCorrectNamingConvention = allTypes.Where(x => x.Name.EndsWith("Handler"))
                .ToList();

            //Assert
            Assert.True(allTypes.Any());
            Assert.Equal(allTypes.Count(), allTypesWithCorrectNamingConvention.Count());
        }

        [Fact]
        public void CqrsCommandValidators_NameShouldEndWithValidator_Check()
        {
            //Arrange
            var allTypes = FindAllValidators(assembly);
            
            //Act
            var allQueriesWithCorrectNaming = allTypes.Where(x => x.Name.EndsWith("Validator"))
                .ToList();

            //Assert
            Assert.True(allTypes.Any());
            Assert.Equal(allTypes.Count(), allQueriesWithCorrectNaming.Count());
        }

        private static IEnumerable<Type> FindAllHandler(Assembly assembly)
        {
            var allTypes = assembly.GetTypes();

            var foundTypes = allTypes.Where(type =>
            !type.IsGenericType &&
            type.GetInterfaces().Any(i =>
                i.IsGenericType &&
                (i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) ||
                 i.GetGenericTypeDefinition() == typeof(IRequestHandler<>)) 
            )
        );

            return foundTypes;
        }
        private static IEnumerable<Type> FindAllValidators(Assembly assembly)
        {
            var allTypes = assembly.GetTypes();

            // Filter types that derive from AbstractValidator<>
            var foundTypes = allTypes.Where(type =>
                type.BaseType != null &&
                type.BaseType.IsGenericType &&
                type.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>)
            );
            return foundTypes;
        }
        private static IEnumerable<Type> FindCommandTypes(Assembly assembly)
        {
            var allTypes = assembly.GetTypes();

            var foundTypes = allTypes.Where(type =>
                type.GetInterfaces().Any(interfaceType =>
                    interfaceType.IsGenericType &&
                    (interfaceType.GetGenericTypeDefinition() == typeof(ICommand<>) ||
                     interfaceType.GetGenericTypeDefinition() == typeof(Application.Abstractions.Messaging.ICommand))
                )
            );

            return foundTypes;
        }
        private static IEnumerable<Type> FindQueryTypes(Assembly assembly)
        {
            var allTypes = assembly.GetTypes();

            var foundTypes = allTypes.Where(type =>
                !type.IsGenericType &&
                type.GetInterfaces().Any(interfaceType =>
                    interfaceType.IsGenericType &&
                    (interfaceType.GetGenericTypeDefinition() == typeof(IQuery<>))
                )
            );

            return foundTypes;
        }
    }
}
