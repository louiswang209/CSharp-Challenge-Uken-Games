using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Read();

        }


        static void Read()
        {
            HashSet<string> strList = new HashSet<string>();

            allFileName("file", ref strList, new string[] { "txt" });//Get all file names under this file
            foreach (var str in strList)
            {
                new Thread(() => outPut(str)) { IsBackground = true }.Start();//Multi threads
            }

            Console.Read();
        }

        static void allFileName(string path, ref HashSet<string> strList, string[] str_Arrary_FileType)
        {
            if (!path.EndsWith(@"\"))
            {
                path += @"\";
            }

            DirectoryInfo theFolder = new DirectoryInfo(@path);
            foreach (string str in str_Arrary_FileType)
            {


                string str_FileType = str.Trim('.');

                //Go through file
                foreach (FileInfo NextFile in theFolder.GetFiles("*." + str_FileType))
                {
                    strList.Add(path + NextFile.Name);//File path
                }
            }
            
            foreach (DirectoryInfo NextFolder in theFolder.GetDirectories())
            {
                
                allFileName(path + NextFolder.Name, ref strList, str_Arrary_FileType);
            }

        }

        static void outPut(string fileName)
        {
            string[] str_No = File.ReadAllLines(fileName);//Get file content

            #region Get all numbers and the number of time they repeated
            Dictionary<int, int> dic_No_Time = new Dictionary<int, int>();
            foreach (string str in str_No)
            {
                int No;
                if (int.TryParse(str, out No) == false)
                {
                    
                    continue;
                }

                if (dic_No_Time.ContainsKey(No) == false)
                {
                    dic_No_Time.Add(No, 1);
                }
                else
                {
                    dic_No_Time[No]++;
                }
            }
            #endregion
            #region Get min
            
            int minTime = int.MaxValue;
            int minKey = int.MaxValue;
            foreach (var kv in dic_No_Time)//Get the number that repeated the fewest number of times
            {
                if (kv.Value < minTime)
                {
                    minTime = kv.Value;
                }
            }
            foreach (var kv in dic_No_Time)//Get the smaller of two numbers
            {
                if (kv.Value == minTime)
                {
                    if (kv.Key < minKey)
                    {
                        minKey = kv.Key;
                    }
                }
            }

            #endregion

            Console.WriteLine($"File：{fileName}，Number：{minKey}，Repetead：{minTime} time(s)");


        }


    }
}
