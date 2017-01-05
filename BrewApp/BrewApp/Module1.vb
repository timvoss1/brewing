Imports System
Imports System.IO

Module Module1

    Sub Main()

        'Equipment Profile <--- Write input deck and reader
        Dim eta As Double
        Dim dMash As Double
        eta = 0.7
        dMash = 13

        'Location of code
        Dim inp_path = "C:\Users\Tim\Desktop\Local-Repo\brewing\Recipes\"
        Dim data_path = "C:\Users\Tim\Desktop\Local-Repo\brewing\Data\"

        'Folder/File names
        Dim name_data = "GrainPotential.csv"
        Dim name_input = "16A_LeftHand.txt"

        'Data objects
        Dim myRecipe As Recipe
        Dim myData(,) As String

        myRecipe = ParseInput(name_input, inp_path)
        myData = oneDArrayParse(name_data, data_path)

        'Dependent Values
        Dim GU As Double 'gravity units
        Dim TGU As Double 'total gravity units
        Dim grainName() As String
        Dim grainPote() As Double
        Dim grainPerc() As Double
        Dim grainWeig() As Double
        Dim totWeight As Double
        Dim WaterNeed As Double
        Dim WaterMash As Double
        Dim spargeHeight As Double

        'Calculations
        GU = (myRecipe.OG - 1) * 1000
        TGU = GU * myRecipe.vol

        grainName = myRecipe.getGrainName
        grainPerc = myRecipe.getGrainPerc

        ReDim grainPote(grainName.Length - 1)
        ReDim grainWeig(grainName.Length - 1)

        Console.WriteLine(myRecipe.Name)
        Console.WriteLine()
        Console.WriteLine("Target gravity: " + CStr(myRecipe.OG))
        Console.WriteLine("Final volume: " + CStr(myRecipe.vol))
        Console.WriteLine()

        'get potential gravity per grain and weight
        For i = 0 To grainName.Length - 1
            If Not (IsNothing(grainName(i))) Then

                For j = 0 To myData.Length / 7 - 1
                    If InStr(myData(0, j), grainName(i), CompareMethod.Text) > 0 Then

                        grainPote(i) = (CDbl(myData(5, j)) - 1) * 1000
                        grainWeig(i) = grainPerc(i) / 100 * TGU / (grainPote(i) * eta)
                        totWeight = totWeight + grainWeig(i)

                        Console.WriteLine(CStr(Math.Round(grainWeig(i), 2)) + " lb." + vbTab + grainName(i))

                    End If
                Next
            End If
        Next

        Console.WriteLine("--------")
        Console.WriteLine(CStr(Math.Round(totWeight, 2)))

        WaterNeed = (myRecipe.vol + 0.125 * totWeight + 1 + 1) * 1.04
        WaterMash = 0.375 * totWeight
        spargeHeight = WaterMash * 231 / (Math.PI * dMash ^ 2 / 4)

        Console.WriteLine()
        Console.WriteLine(CStr(Math.Round(WaterMash, 2)) + vbTab + vbTab + "Water in mash tun, set sparge arm to " + CStr(Math.Round(spargeHeight, 2)) + " inches")
        Console.WriteLine(CStr(Math.Round(WaterNeed - WaterMash, 2)) + vbTab + vbTab + "Water in HLT")
        Console.WriteLine("--------")
        Console.WriteLine(CStr(Math.Round(WaterNeed, 2)))


        Console.ReadKey()


    End Sub

    Function ParseInput(filename As String, path As String) As Recipe

        Dim sr As StreamReader
        Dim read As String, id As String, myRecipe As New Recipe
        Dim readArr() As String, counter As Integer

        counter = -1
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
                        read = Replace(read, "OG: ", "")
                        myRecipe.OG = CDbl(read)
                    ElseIf InStr(read, "ABV") Then
                        read = Replace(read, "ABV: ", "")
                        myRecipe.ABV = CDbl(read)
                    ElseIf InStr(read, "IBU") Then
                        read = Replace(read, "IBU: ", "")
                        myRecipe.IBU = CDbl(read)
                    ElseIf InStr(read, "SRM") Then
                        read = Replace(read, "SRM: ", "")
                        myRecipe.SRM = CDbl(read)
                    ElseIf InStr(read, "t_mash") Then
                        read = Replace(read, "t_mash: ", "")
                        myRecipe.tmash = CDbl(read)
                    ElseIf InStr(read, "t_boil") Then
                        read = Replace(read, "t_boil: ", "")
                        myRecipe.tboil = CDbl(read)
                    ElseIf InStr(read, "vol:") Then
                        read = Replace(read, "vol: ", "")
                        myRecipe.vol = CDbl(read)
                    End If
                ElseIf id = "Grain" Then
                    counter = counter + 1
                    readArr = Split(read, ":")
                    myRecipe.GrainName(counter) = readArr(0)
                    read = Replace(readArr(1), " ", "")
                    read = CDbl(read)
                    myRecipe.GrainPerc(counter) = read
                ElseIf id = "End" Then
                    GoTo ExitSub


                End If



            End If


        Next

ExitSub:


        Return myRecipe

    End Function


    Function oneDArrayParse(filename As String, path As String) As String(,)

        Dim sr As StreamReader
        Dim read As String, readArr() As String, returnArr(,) As String

        sr = My.Computer.FileSystem.OpenTextFileReader(path + filename)

        read = sr.ReadLine()
        readArr = Split(read, ",")
        ReDim returnArr(readArr.Length - 1, File.ReadAllLines(path + filename).Length - 1)

        For i = 0 To readArr.Length - 1
            returnArr(i, 0) = readArr(i)
        Next

        For i = 1 To File.ReadLines(path + filename).Count() - 2

            read = sr.ReadLine()
            readArr = Split(read, ",")

            For j = 0 To readArr.Length - 1
                returnArr(j, i) = readArr(j)
            Next


        Next

        Return returnArr

    End Function

    Function twoDArrayParse(filename As String, path As String) As VariantType

        'write generic 2d csv array parser

        'Dim returnArr() As VariantType

        'Return returnArr

    End Function

End Module
