using System;
using System.Collections;
using System.IO;
using System.Net;
using UnityEngine;

public class FtpConnect
{
    private static readonly int BufferSize = 2048;

    public struct FileInfo
    {
        public bool IsDirectory;
        public string Name;
        public long Size;
    }

    public static IEnumerator Upload (Ftp ftp, string localFilePath, string remoteFilePath)
    {
        ftp.Connect(remoteFilePath, WebRequestMethods.Ftp.UploadFile);

        FileStream localFileStream = new FileStream(localFilePath, FileMode.Open);
        byte[] fileBuffer = new byte[BufferSize];
        int fileSize = localFileStream.Read(fileBuffer, 0, BufferSize);

        while (fileSize != 0)
        {
            ftp.Stream.Write(fileBuffer, 0, fileSize);
            fileSize = localFileStream.Read(fileBuffer, 0, BufferSize);
        }

        localFileStream.Close();
        ftp.Dispose();

        yield return null;
    }

    public static IEnumerator Download (Ftp ftp, string localFilePath, string remoteFilePath)
    {
        ftp.Connect(remoteFilePath, WebRequestMethods.Ftp.DownloadFile);

        FileStream localFileStream = new FileStream(localFilePath, FileMode.Create);
        byte[] fileBuffer = new byte[BufferSize];
        int fileSize = ftp.Stream.Read(fileBuffer, 0, BufferSize);

        while (fileSize > 0)
        {
            localFileStream.Write(fileBuffer, 0, fileSize);
            fileSize = ftp.Stream.Read(fileBuffer, 0, BufferSize);
        }

        localFileStream.Close();
        ftp.Dispose();

        yield return null;
    }

    public static void Delete (Ftp ftp, string remoteFilePath)
    {
        ftp.Connect(remoteFilePath, WebRequestMethods.Ftp.DeleteFile);
        ftp.Dispose();
    }

    public static void Rename (Ftp ftp, string remoteFilePath, string newFileName)
    {
        ftp.Connect(remoteFilePath, WebRequestMethods.Ftp.Rename);
        {
            ftp.Request.RenameTo = newFileName;
            ftp.Response = (FtpWebResponse)ftp.Request.GetResponse();
        }
        ftp.Dispose();
    }

    public static DateTime GetFileCreatedDateTime (Ftp ftp, string remoteFilePath, bool isKorean = true)
    {
        DateTime dateTime;
        ftp.Connect(remoteFilePath, WebRequestMethods.Ftp.GetDateTimestamp);
        {
            dateTime = ftp.Response.LastModified;
        }
        ftp.Dispose();

        dateTime = isKorean ? dateTime.AddHours(9) : dateTime;
        return dateTime;
    }

    public static long GetFileSize (Ftp ftp, string remoteFilePath)
    {
        long size = 0;
        ftp.Connect(remoteFilePath, WebRequestMethods.Ftp.GetFileSize);
        {
            size = (int)ftp.Response.ContentLength;
        }
        ftp.Dispose();

        Debug.Log(size);
        return size;
    }

    public static string[] GetListDirectory (Ftp ftp, string remoteDirectory)
    {
        string directoryRaw = "";
        ftp.Connect(remoteDirectory, WebRequestMethods.Ftp.ListDirectory);
        {
            StreamReader reader = new StreamReader(ftp.Stream);

            while (reader.Peek() != -1)
            {
                if (directoryRaw.Length > 0)
                    directoryRaw += "|";

                directoryRaw += reader.ReadLine();
            }

            reader.Close();
        }
        ftp.Dispose();

        return directoryRaw.Split("|".ToCharArray());
    }

    public static FileInfo[] GetListDirectoryDetails (Ftp ftp, string remoteDirectory)
    {
        string directoryRaw = "";
        ftp.Connect(remoteDirectory, WebRequestMethods.Ftp.ListDirectoryDetails);
        {
            StreamReader reader = new StreamReader(ftp.Stream);

            while (reader.Peek() != -1)
            {
                if (directoryRaw.Length > 0)
                    directoryRaw += "|";

                directoryRaw += reader.ReadLine();
            }

            reader.Close();
        }
        ftp.Dispose();


        string[] directories = directoryRaw.Split("|".ToCharArray());
        FileInfo[] fileInfo = new FileInfo[directories.Length];
        for (int i = 0; i < directories.Length; i++)
        {
            string[] tokens = directories[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            fileInfo[i] = new FileInfo();
            fileInfo[i].IsDirectory = tokens[0].StartsWith("d") ? true : false;
            fileInfo[i].Size = long.Parse(tokens[4]);
            fileInfo[i].Name = tokens[8];
        }

        return fileInfo;
    }

    public static bool IsExistDirectoryOrFile (Ftp ftp, string checkDirectoryName, string checkName)
    {
        string[] arr = GetListDirectory(ftp, checkDirectoryName);

        foreach (string temp in arr)
        {
            if (temp == checkName)
                return true;
        }

        return false;
    }

    public static void CreateDirectory (Ftp ftp, string newDirectoryName)
    {
        bool isCheck = IsExistDirectoryOrFile(ftp, newDirectoryName, newDirectoryName);
        if (isCheck == true)
            return;

        ftp.Connect(newDirectoryName, WebRequestMethods.Ftp.MakeDirectory);
        ftp.Dispose();
    }
}

public class Ftp
{
    private string _address;
    public string Address
    {
        get { return _address; }
    }

    private string _id;
    private string _pw;

    private FtpWebRequest _request = null;
    public FtpWebRequest Request
    {
        get { return _request; }
    }

    private FtpWebResponse _response = null;
    public FtpWebResponse Response
    {
        get { return _response; }
        set { _response = value; }
    }

    private Stream _stream = null;
    public Stream Stream
    {
        get { return _stream; }
    }

    public Ftp (string address, string id, string pw)
    {
        _address = address;
        if (_address.Substring(_address.Length - 1, 1) != "/")
            _address += "/";

        _id = id;
        _pw = pw;
    }

    public void Connect (string filePath, string method)
    {
        try
        {
            string path = _address + filePath;

            _request = (FtpWebRequest)FtpWebRequest.Create(path);
            _request.Credentials = new NetworkCredential(_id, _pw);
            _request.UseBinary = true;
            _request.UsePassive = true;
            _request.KeepAlive = true;
            _request.Method = method;

            switch (method)
            {
                case WebRequestMethods.Ftp.UploadFile:
                    _stream = _request.GetRequestStream();
                    break;

                case WebRequestMethods.Ftp.DeleteFile:
                case WebRequestMethods.Ftp.GetDateTimestamp:
                case WebRequestMethods.Ftp.GetFileSize:
                case WebRequestMethods.Ftp.MakeDirectory:
                    _response = (FtpWebResponse)_request.GetResponse();
                    break;

                case WebRequestMethods.Ftp.DownloadFile:
                case WebRequestMethods.Ftp.ListDirectoryDetails:
                case WebRequestMethods.Ftp.ListDirectory:
                    _response = (FtpWebResponse)_request.GetResponse();
                    _stream = _response.GetResponseStream();
                    break;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void Dispose ()
    {
        if (_stream != null)
            _stream.Close();

        if (_response != null)
            _response.Close();

        _request = null;
    }
}