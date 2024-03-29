﻿using Domain.Const;
using Domain.Entities.Users;
using Domain.Extension;
using Domain.Primitives;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using System.Text;

namespace Shared.Infrastructure.Extension
{

    public static partial class DbContextExtension
    {
        public struct ColumnNameDefinitions
        {
            public const string UuidName = "uuid";
            public const string Name = "name";
            public const string Description = "description";

            public struct UserSpecific
            {
                public const string CreatedByUser = "created_by_uuid";
                public const string ModifiedByUser = "modified_by_uuid";
                public const string DeletedByUser = "deleted_by_uuid";
            }

            public struct TimeSpecific
            {
                public const string CreatedTime = "created_time";
                public const string ModifiedTime = "modified_time";
                public const string DeletedTime = "deleted_time";
            }
        }
        public struct ConstraintNameDefinitions
        {

        }
        public struct IndexNameDefinitions
        {
            public const string PrimaryKeyIndex = "PRIMARY";
        }

        public struct ColumnType
        {
            public const string Bool = "BOOLEAN";
        }
        public struct ColumnLength
        {
            public const int Ids = 36;
            public const int Names = 64;
            public const int PasswordLength = 32;
            public const int Descriptions = 255;
            public const int FileExtension = 5;
            public const int MimeTypes = 50;
            public const int PathDescriptors = 1024;
            public const int Base64 = 4096;
            public const int EmailSubject = 255;
            public const int EmailAddrLength = 255;
        }
        public struct Constraint
        {
            public const int MaxNameLen = 64;
        }

        public static Func<Type, string> GetTableName = new Func<Type, string>((type) =>
        {
            string typeName = type.Name;
            string tableName = null;
            StringBuilder result = new StringBuilder();

            foreach (char character in typeName)
            {
                if (char.IsUpper(character))
                {
                    result.Append('_');
                }
                result.Append(character);
            }
            tableName = result.ToString().TrimStart('_');

            return $"{tableName}".ToLower();
        });

        public static Func<string, string> GetIndexName = new Func<string, string>((name) =>
        {
            string val = $"IDX_{name}".ToUpper();
            return val;
        });

        public static Func<string, string, string, string> GetIndexForFkName = new Func<string, string, string, string>((fromEntity, fromEntityKeyName, foreignEntityName) =>
        {
            string val = GenerateName("IDX_FK_", fromEntity, fromEntityKeyName, foreignEntityName).ToUpper();
            return val;
        });

        public static Func<string, string, string, string> GetForeignKeyName = new Func<string, string, string, string>((fromEntity, fromEntityKeyName, to) =>
        {
            string val = GenerateName("FK_", fromEntity, fromEntityKeyName, to).ToUpper();
            return val;
        });
        private static Func<string, string, string, string, string> GenerateName = new Func<string, string, string, string, string>((preFix, fromEntity, fromEntityKeyName, to) =>
        {
            string shortenerFk = fromEntityKeyName.ToUpper().Replace("FOREIGNKEY", "FK");
            string val = $"{preFix}_{fromEntity}_{shortenerFk}_TO_{to}".ToUpper();
            if (val.Length > Constraint.MaxNameLen)
            {
                throw new InvalidOperationException($"Constraint Name: {val} is longer than {Constraint.MaxNameLen} chars");
            }
            return val;
        });

        public static List<User> GetTestSet()
        {
            List<User> testSet = new List<User>();
            for (int i = 0; i < 10; i++)
            {
                var userId = new UserId(Guid.NewGuid());
                var rootUserId = new UserId(UserConst.RootUserId);
                List<UserHasRelationToRole> roles = new List<UserHasRelationToRole>()
                {

                    UserHasRelationToRole.Create(
                        UserHasRelationToRole.NewId(),
                        userId,
                        new RoleId(RoleConst.UserRoleUuid),
                        DateTime.UtcNow.ToTypedDateTime(),
                        rootUserId,
                        null,
                        null,
                        null,
                        null)
                };
                var var1 = User.Create(
                    userId,
                    new UserTypeId(UserConst.UserType.Root),
                    $"Test{i}",
                    "abcd1234",
                    $"Test{i}",
                    $"Test{i}",
                    null,
                    null,
                    null,
                    null,
                    null,
                    Email.Parse($"test{i}@localhost.local"),
                    PhoneNumber.Parse("+49123456789"),
                    null,
                    roles,
                    DateTime.UtcNow.ToTypedDateOnly(),
                    DateTime.UtcNow.ToTypedDateTime(),
                    DateTime.UtcNow.ToTypedDateTime(),
                    rootUserId,
                    CustomDateTime.Now(),
                    null,
                    null,
                    null);

                testSet.Add(var1);
            }
            return testSet;
        }
    }
}
