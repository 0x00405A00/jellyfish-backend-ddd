#define HOST_IS_WINDOWS
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using System.Security.AccessControl;

namespace Infrastructure.FileSys
{
    public class FileHandler : IFileHandler
    {
        private readonly ILogger<FileHandler> logger;
        private readonly IAntiVirus antiVirus;
        private readonly IAzureAdultContentDetection azureAdultContentDetection;

        public static string EnvironmentCurrentDirectoy => Environment.CurrentDirectory;
        public static string WebRoot => Path.Combine(EnvironmentCurrentDirectoy, "wwwroot");

        /// <summary>
        /// General User Content Folder
        /// </summary>
        public static string UsersMediaFolder => Path.Combine(WebRoot, "users");
        /// <summary>
        /// User Profile Picture will be stored there
        /// </summary>
        public static string UserProfilePictures => Path.Combine(UsersMediaFolder, "profile-img");

        /// <summary>
        /// General Chat Content Folder
        /// </summary>
        public static string ChatsMediaFolder => Path.Combine(WebRoot, "chats");
        /// <summary>
        /// Chat Images for Groupchats, when Chat is no Group the Profile Picture of Chatuser will be shown
        /// </summary>
        public static string ChatPictures => Path.Combine(ChatsMediaFolder, "chat-img");
        /// <summary>
        /// Media that is shared over the chat
        /// </summary>
        public static string ChatMediaContent => Path.Combine(ChatsMediaFolder, "chat-media-content");

        private static readonly IDictionary<string, DirectoryExtension.FileSystemRightsExt> _createDirectories = new Dictionary<string, DirectoryExtension.FileSystemRightsExt>
    {
        { UsersMediaFolder, DirectoryExtension.FileSystemRightsExt.Read | DirectoryExtension.FileSystemRightsExt.Write },
        { UserProfilePictures, DirectoryExtension.FileSystemRightsExt.Read|DirectoryExtension.FileSystemRightsExt.Write },
        { ChatsMediaFolder, DirectoryExtension.FileSystemRightsExt.Read | DirectoryExtension.FileSystemRightsExt.Write },
        { ChatPictures, DirectoryExtension.FileSystemRightsExt.Read | DirectoryExtension.FileSystemRightsExt.Write },
        { ChatMediaContent, DirectoryExtension.FileSystemRightsExt.Read | DirectoryExtension.FileSystemRightsExt.Write }
    };
        public FileHandler(
            ILogger<FileHandler> logger,
            IAntiVirus antiVirus,
            IAzureAdultContentDetection azureAdultContentDetection)
        {
            this.logger = logger;
            this.antiVirus = antiVirus;
            this.azureAdultContentDetection = azureAdultContentDetection;
#if HOST_IS_WINDOWS
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                throw new Exception($"try to use {nameof(FileHandler)} with HOST_IS_WINDOWS==true, but Host is an unix derivat (Host should be Windows)");
            }
#else
            if(Environment.OSVersion.Platform != PlatformID.Unix)
            {
                throw new Exception($"try to use {nameof(FileHandler)} with HOST_IS_WINDOWS==false, but Hosts is anything other than Linux (Host should be Linux)");
            }
#endif
        }

        void IFileHandler.CreateApplicationFolders()
        {
            foreach(var folder in _createDirectories)
            {
                string folderPath = folder.Key;
                DirectoryExtension.FileSystemRightsExt folderPathRightsExt = folder.Value;
                var folderInfo = DirectoryExtension.CreateIfNotExistsDirectory(folderPath);
                if (folderInfo is null)
                {
                    logger.LogError($"{nameof(folderInfo)} is null; folder {folderPath} does not exist or cannot be created.");
                    throw new DirectoryNotFoundException($"{nameof(folderInfo)} is null; folder {folderPath} does not exist or cannot be created.");
                }

                logger.LogInformation($"Folder {folderPath} exists or has been created successfully.");

                try
                {
/*#if HOST_IS_WINDOWS

                    folderInfo.SetFolderPermissions("jellyfish-ACL",
                        folderPathRightsExt);
#else

                    folderInfo.SetFolderPermissions(
                        folderPathRightsExt);
#endif*/

                    logger.LogInformation($"Permissions set for folder {folderPath}.");
                }
                catch (Exception ex)
                {
                    logger.LogError($"Failed to set permissions for folder {folderPath}. Error: {ex.Message}");
                    throw;
                }
            }
        }

        async Task<MediaContentDTO> IFileHandler.CreateOrUpdateFile(
            string path,
            byte[] content,
            CancellationToken cancellationToken)
        {
            MediaContentDTO mediaContentDTO =null;
            try
            {
                var mediaContent = MediaContentDTO.Parse(content,antiVirus,azureAdultContentDetection);
                mediaContentDTO = await mediaContent.CheckInadmissibleContent(antiVirus, azureAdultContentDetection, cancellationToken);
                await File.WriteAllBytesAsync(path, content, cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
            return mediaContentDTO; 
        }
        void IFileHandler.DeleteFile(string path, CancellationToken cancellationToken)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        async Task<byte[]> IFileHandler.ReadFile(string path, CancellationToken cancellationToken)
        {
            try
            {
                return await File.ReadAllBytesAsync(path);
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }
    }

    public static class DirectoryExtension
    {
        [Flags]
        public enum FileSystemRightsExt
        {
            ReadData = 1,
            WriteData = 2,
            ExecuteFile = 32,
            Write = 278,
            Read = 131209,
            ReadAndExecute = 131241,
            FullControl = 2032127
        }

        public static async Task<byte[]?> LoadMediaContent(this MediaContent mediaContent, CancellationToken cancellationToken)
        {
            if(mediaContent is null && mediaContent.FilePath is null)
                return null;    
            if (!File.Exists(mediaContent.FilePath))
                return null;
            return await File.ReadAllBytesAsync(mediaContent.FilePath, cancellationToken);
        }

#if HOST_IS_WINDOWS
        public static Dictionary<FileSystemRightsExt, FileSystemRights> FileSystemRightsToWindowsPermissions = new Dictionary<FileSystemRightsExt, FileSystemRights>
            {
            { FileSystemRightsExt.Read, FileSystemRights.Read },
            { FileSystemRightsExt.Write, FileSystemRights.Write},
            { FileSystemRightsExt.ExecuteFile, FileSystemRights.ExecuteFile },
            { FileSystemRightsExt.ReadAndExecute, FileSystemRights.ReadAndExecute },
            { FileSystemRightsExt.Read | FileSystemRightsExt.Write, FileSystemRights.Read | FileSystemRights.Write },
            { FileSystemRightsExt.FullControl, FileSystemRights.FullControl },
        };
#else
        public static Dictionary<FileSystemRightsExt, string> FileSystemRightsToLinuxPermissions = new Dictionary<FileSystemRightsExt, string>
        {
            { FileSystemRightsExt.Read, "400" },
            { FileSystemRightsExt.Write, "200" },
            { FileSystemRightsExt.ExecuteFile, "100" },
            { FileSystemRightsExt.ReadAndExecute, "500" },
            { FileSystemRightsExt.Read | FileSystemRightsExt.Write, "600" },
            { FileSystemRightsExt.FullControl, "777" },
        };
#endif
        public static DirectoryInfo CreateIfNotExistsDirectory(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            try
            {
                if (!Directory.Exists(path))
                {
                    directoryInfo = Directory.CreateDirectory(path);
                }
            }
            catch (Exception e)
            {
                throw;
                return null;
            }
            return directoryInfo;
        }

#if HOST_IS_WINDOWS
        public static void SetFolderPermissions(this DirectoryInfo directoryInfo, string identityName, FileSystemRightsExt fileSystemRights)
        {
            try
            {
                DirectorySecurity directorySecurity = directoryInfo.GetAccessControl();

                directorySecurity.SetOwner(System.Security.Principal.WindowsIdentity.GetCurrent().User);
                FileSystemRights fileSystemRightsWindows = GetSystemRight(fileSystemRights);
                // Berechtigungen für "Jeder" (Everyone) setzen (Lesen, Schreiben)
                FileSystemAccessRule rule = new FileSystemAccessRule(
                    "Jeder",
                    fileSystemRightsWindows,
                    InheritanceFlags.None,
                    PropagationFlags.None,
                    AccessControlType.Allow);

                directorySecurity.AddAccessRule(rule);
                directoryInfo.SetAccessControl(directorySecurity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static FileSystemRights GetSystemRight(FileSystemRightsExt fileSystemRightsExt)
        {
            return FileSystemRightsToWindowsPermissions.TryGetValue(fileSystemRightsExt, out var result)? result: FileSystemRights.Read;
        }
#else
        public static void SetFolderPermissions(this DirectoryInfo directoryInfo, FileSystemRightsExt fileSystemRights)
        {
            try
            {
                string fileSystemRightsLinux = GetSystemRight(fileSystemRights);
                System.Diagnostics.Debug.WriteLine($"set file acls: {fileSystemRightsLinux} {directoryInfo.FullName}");
                var process = new System.Diagnostics.Process();
                var startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "chmod",
                    Arguments = $"{fileSystemRightsLinux} {directoryInfo.FullName}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = Environment.CurrentDirectory,

                };
                process.StartInfo = startInfo;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                if (process.ExitCode == 0)
                {
                    throw new Exception($"Permissions set for directory: {directoryInfo.FullName}");
                }
                else
                {
                    throw new Exception($"Error setting permissions for directory {directoryInfo.FullName}: {error}");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static string GetSystemRight(FileSystemRightsExt fileSystemRightsExt)
        {
            return FileSystemRightsToLinuxPermissions.TryGetValue(fileSystemRightsExt, out var result) ? result : "r";
        }
#endif
    }
}
