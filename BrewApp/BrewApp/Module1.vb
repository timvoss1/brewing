Imports System
Imports System.IO

Module Module1

    Sub Main()

        'Location of code
        Dim path = "C:\Users\Tim\Desktop\Local-Repo\brewing\Recipes\"

        'Folder/File names
        Dim name_data = "Data"
        Dim name_input = "16A_LeftHand.txt"

        ParseInput(name_input, path)

        Console.WriteLine("Hello World")
        Console.Read()


    End Sub

    Sub ParseInput(filename As String, path As String)

        Dim sr As StreamReader
        Dim read As String, id As String, myRecipe As New Recipe
        Dim readArr() As String

        sr = My.Computer.FileSystem.OpenTextFileReader(path + filename)

        'loop through text file
        For i = 1 To File.ReadLines(path + filename).Count()

            'read next line
            read = sr.ReadLine()

            'find header and id from header
            If InStr(read, "Begin") Then
                id = Replace(read, "Begin", "")
                id = Replace(id, " ", "")


            ElseIf Not String.IsNullOrEmpty(read) Then

                'parse by id
                If id = "Name" Then
                    myRecipe.Name = read
                ElseIf id = "Target" Then
                    If InStr(read, "OG") Then
                        read = Replace(read, "OG:", "")
                        read = Replace(read, " ", "")
                        myRecipe.OG = CDbl(read)
                    ElseIf InStr(read, "ABV") Then
                        read = Replace(read, "ABV:", "")
                        read = Replace(read, " ", "")
                        myRecipe.ABV = CDbl(read)
                    ElseIf InStr(read, "IBU") Then
                        read = Replace(read, "IBU:", "")
                        read = Replace(read, " ", "")
                        myRecipe.IBU = CDbl(read)
                    ElseIf InStr(read, "SRM") Then
                        read = Replace(read, "SRM:", "")
                        read = Replace(read, " ", "")
                        myRecipe.SRM = CDbl(read)
                    ElseIf InStr(read, "t_mash") Then
                        read = Replace(read, "t_mash:", "")
                        read = Replace(read, " ", "")
                        myRecipe.tmash = CDbl(read)
                    ElseIf InStr(read, "t_boil") Then
                        read = Replace(read, "t_boil:", "")
                        read = Replace(read, " ", "")
                        myRecipe.tboil = CDbl(read)
                    End If
                ElseIf id = "Grain" Then
                    readArr = Split(read, ":")

                    'add class code to make arrays for grain name and grain percentage

                End If



            End If


        Next




    End Sub

End Module
