Imports System
Imports System.IO

Module Module1

    Sub Main()

        'Location of code
        Dim path = "C:\Users\Tim\Desktop\Local-Repo\brewing\"

        'Folder/File names
        Dim name_data = "Data"
        Dim name_input = "InputDeck.txt"

        ParseInput(name_input, path)

        Console.WriteLine("Hello World")
        Console.Read()


    End Sub

    Sub ParseInput(filename As String, path As String)

        Dim sr As StreamReader
        Dim read As String

        sr = My.Computer.FileSystem.OpenTextFileReader(path + filename)

        For i = 1 To File.ReadLines(path + filename).Count()
            read = sr.ReadLine()
        Next




    End Sub

End Module
