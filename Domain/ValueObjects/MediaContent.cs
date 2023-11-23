﻿namespace Domain.ValueObjects
{
    public class MediaContent
    {
        public byte[]? Data { get; private set; }
        public string FilePath { get; private set; }
        /// <summary>
        /// Filesystem Fileextension or MimeType
        /// </summary>
        public string FileExtension { get; private set; }
        public MediaContent(byte[] data, string filePath, string fileExtension)
        {
            Data = data;
            FilePath = filePath;
            FileExtension = fileExtension;
        }
        public MediaContent(string filePath, string fileExtension):this(null,filePath,fileExtension)
        {
        }
        public static MediaContent Parse(byte[] data, string filePath, string fileExtension)
        {
            return new MediaContent(data, filePath, fileExtension);
        }
        public static MediaContent Parse(string filePath, string fileExtension)
        {
            return new MediaContent(filePath, fileExtension);
        }
        public void SetBinary(byte[] data)=> Data= data;    
        public override string ToString()
        {
            return Data == null ? null: Convert.ToBase64String(Data);
        }
    }
}
