using System;
using System.IO;
using Google.Apis.Drive.v3;
using GFile = Google.Apis.Drive.v3.Data.File;
using Google.Apis.Auth.OAuth2;
using System.Collections.Generic;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using System.Linq;
using Google.Apis.Download;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Upload;

namespace GDrive
{
    class Program
    {
        private static List<String> Scopes = new List<String> { DriveService.Scope.Drive };
        private static String AppName = "LangLearn";
        private static String filePath_uploadedFileV1 = @"F:\Eng learning v01.rar";
        private static String filePath_uploadedFileV2 = @"F:\Eng learning v02.rar";

        static void Main(string[] args)
        {
            DriveService driveService = GetDriveService(GetUserCredential());

            ShowFolderFiles(driveService, "1qSRmdpsjZwv6w8k3sCmU9fo7dC2KmCOO");

            //List<GoogleFile> googleFiles = driveService.Files.List().Execute().Files.ToList();
            //googleFiles.ForEach(x => Console.WriteLine($"{x.Name} (id:{x.Id})"));

            //GFile file = UploadFileToGDrive(driveService,  Path.GetFileName(filePath_uploadedFileV1), filePath_uploadedFileV1, "application/zip");
            //Console.WriteLine($"File uploaded: {file.Name} (id:{file.Id})");

            //RevisionList revisions = RetriveRevision(driveService, file.Id);
            //revisions.Revisions.ToList()?.ForEach(x => Console.WriteLine($"{x.ModifiedTime} ({x.Id})"));

            //file = UpdateFileInGDrive(driveService, file.Id, "new version", "updated version", "application/zip", filePath_uploadedFileV2, true);
            //Console.WriteLine($"File updated: {file.Name} (id:{file.Id})");
            //revisions = RetriveRevision(driveService, file.Id);
            //revisions.Revisions.ToList().ForEach(x => Console.WriteLine($"{x.ModifiedTime} ({x.Id})"));

            //DownloadFileFromGDrive(driveService, file.Id, Path.Combine(Path.GetDirectoryName(filePath_uploadedFileV1), Path.GetFileName(filePath_uploadedFileV1) + "(downloaded).rar"));
            //Console.WriteLine($"File downloaded: {file.Id}");

            Console.WriteLine("Program finished");
            Console.ReadLine();
        }

        private static void ShowFolderFiles(DriveService service, String folderId)
        {
            GFile file = service.Files.Get(folderId).Execute();
            if (file == null) return;
            Console.WriteLine(file.Name);
            FilesResource.ListRequest request = service.Files.List();
            //request.Q = $"{folderId} in parents";
            //file.Trashed
            request.Fields = "files(id, name, parents, trashed)";
            FileList fileList = request.Execute();
            if (fileList == null) return;
            fileList.Files.Where(x => x.Trashed != true && x.Parents.Contains(folderId)).ToList().ForEach(x => Console.WriteLine($"{x.Name} ({x.Id})"));

        }

        private static RevisionList RetriveRevision(DriveService service, String fileId)
        {
            try
            {
                RevisionList revisions = service.Revisions.List(fileId).Execute();
                Revision rev = new Revision();
                return revisions;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
            }
            return null;
        }

        private static void DownloadFileFromGDrive(DriveService service, String fileId, String filePath)
        {
            var request = service.Files.Get(fileId);

            using (var memStream = new MemoryStream())
            {
                request.MediaDownloader.ProgressChanged += (IDownloadProgress progress) =>
                {
                    switch (progress.Status)
                    {
                        case DownloadStatus.Downloading:
                            Console.WriteLine(progress.BytesDownloaded);
                            break;
                        case DownloadStatus.Completed:
                            Console.WriteLine("Download complete");
                            break;
                        case DownloadStatus.Failed:
                            Console.WriteLine("DownloadFailed");
                            break;
                    }
                };
                request.Download(memStream);

                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    Byte[] memStreamBuffer = memStream.GetBuffer();
                    fileStream.Write(memStreamBuffer, 0, memStreamBuffer.Length);
                }
            }
        }

        public static GFile UpdateFileInGDrive(DriveService service, GFile gFile, String filePath, String contentType)
        {
            FilesResource.UpdateMediaUpload updateRequest;
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                updateRequest = service.Files.Update(gFile, gFile.Id, stream, contentType);
                updateRequest.KeepRevisionForever = true;
                updateRequest.Upload();
            }
            return updateRequest.ResponseBody;
        }

        public static GFile UpdateFileInGDrive(DriveService service, String fileId, String newTitle, String newDescription, String newMimeType, String newFileName, bool newRevision)
        {
            try
            {
                GFile file = service.Files.Get(fileId).Execute();
                file.Id = null;
                file.Name = newTitle;
                file.Description = newDescription;
                file.MimeType = newMimeType;

                byte[] byteArray = System.IO.File.ReadAllBytes(newFileName);
                System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);
                FilesResource.UpdateMediaUpload request = service.Files.Update(file, fileId, stream, newMimeType);
                //request.KeepRevisionForever = true;

                request.ProgressChanged += Request_ProgressChanged;

                request.Upload();

                GFile updatedFile = request.ResponseBody;
                return updatedFile;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured: {e.Message}");
                return null;
            }
        }

        private static void Request_ProgressChanged(IUploadProgress progress)
        {
            switch (progress.Status)
            {
                case UploadStatus.Completed:
                    Console.WriteLine("Uploading completed");
                    break;
                case UploadStatus.Failed:
                    {
                        Console.WriteLine("Uploading failed");
                        if (progress.Exception != null)
                        {
                            Console.WriteLine($"{progress.Exception.Message}");
                        }
                        break;
                    }
                case UploadStatus.Starting:
                    Console.WriteLine("Uploading starting");
                    break;
                case UploadStatus.NotStarted:
                    Console.WriteLine("Uploading not started");
                    break;
                case UploadStatus.Uploading:
                    Console.WriteLine("Uploading in progress");
                    break;

            }
        }

        private static GFile UploadFileToGDrive(DriveService service, String fileName, String filePath, String contentType)
        {
            GFile fileMetadata = new GFile
            {
                Name = fileName,
                Parents = new List<String> { "1qSRmdpsjZwv6w8k3sCmU9fo7dC2KmCOO" }
            };
            FilesResource.CreateMediaUpload request;
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                request = service.Files.Create(fileMetadata, stream, contentType);
                request.ProgressChanged += Request_ProgressChanged;
                request.Upload();
            }

            return request.ResponseBody;
        }

        private static DriveService GetDriveService(UserCredential credential)
        {
            BaseClientService.Initializer serviceInitializer = new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = AppName
            };
            return new DriveService(serviceInitializer);
        }

        private static UserCredential GetUserCredential()
        {
            UserCredential userCredential = null;
            using (var stream = new FileStream("L2 ClientSecret.json", FileMode.Open, FileAccess.Read))
            {
                String credPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DriveApiCredentials", "drive-credentials.json");
                userCredential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "User",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
            return userCredential;
        }

    }


    public class AppConfig
    {
        public List<IConfigItem> items { get; set; }
    }

    public interface IConfigItem
    {
        Type Type { get; set; }

        String Name { get; set; }

        String FriendlyName { get; set; }

        String Remarks { get; set; }
    }

    public class ConfigItem<T> : IConfigItem
    {
        public Type Type { get; set; }

        public String Name { get; set; }

        public String FriendlyName { get; set; }

        public String Remarks { get; set; }

        public T DefaultValue { get; set; }

        public T Value { get; set; }
    }

    // Application.Files.AutosaveInterval (d:300, u1:600) Int32;
    //
    // rus("Интервал автосохранения", "в мс", {"Приложение", "Файлы", "Интервал автосохранения"});
    // eng("Autosave interval", "in ms", {"Application", "Files", "Autosave interval"});
}
