using Domain.Entities.Users;
using Shared.DataTransferObject;
using Shared.DataTransferObject.Abstraction;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Architecture.Tests.ApplicationDTO.DTOPropertyNamesAreEqualToDomainObjectProps
{
    public class DTOPropertyNamesAreEqualToDomainObjectPropsTest
    {
        /*[Theory]
        [InlineData((typeof(UserDTO),typeof(User)))]*/
        [Fact]
        public void UserDTOPropertiesNames_ShouldBeEqualTo_DomainProperties()
        {
            //Arrange
            List<(PropertyInfo, string)> values = new List<(PropertyInfo, string)>(); 

            var dto = GetDTO(typeof(UserDTO));
            var props = GetProps(dto);
            var domainObjectProps = typeof(User).GetProperties().Where(x => x.CanWrite && x.CanRead).ToList();
            //Act
            foreach (var prop in props)
            {
                var foundDomainObject = domainObjectProps.Find(x=>x.Name == prop.Name);
                if(foundDomainObject == null)
                {
                    values.Add((prop,"prop not found in domainobject"));
                    continue;
                }

            }

            //Assert
            Assert.Equal(values.Count, 0);
        }

        private Type GetDTO(Type dtoType)
        {

            Assembly assembly = typeof(AbstractDTO).Assembly;
            var allDtos = assembly.GetTypes().Where(x => typeof(IDataTransferObject).IsAssignableFrom(x) && x == dtoType);
            if (allDtos.Count() > 1)
            {
                throw new NotSupportedException();
            }
            return allDtos.First();
        }
        private ICollection<PropertyInfo> GetProps(Type dtoType)
        {

            return dtoType.GetProperties()
                .Where(x => x.CanWrite && x.CanRead && x.GetCustomAttribute<JsonPropertyNameAttribute>() != null && (!x.PropertyType.IsGenericType || x.PropertyType.GetGenericTypeDefinition() != typeof(ICollection<>)))
                .ToList();
        }
    }
}
