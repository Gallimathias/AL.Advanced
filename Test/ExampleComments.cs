
//var tab = con.GetTable<Object>().First(o => o.Type == 5 && o.ID == 93000);

//var str = GetStringFromBLOB(meta.User_AL_Code);

//using (var writer = new StreamWriter(File.OpenWrite(@"C:\Temp\unit_AL.txt")))
//{
//    writer.WriteLine(str);
//}

//var projI = ProjectInfo.Create(ProjectId.CreateNewId(), VersionStamp.Create(), "TestProject", "Test.dll", LanguageNames.CSharp, @"C:\Temp\wiese\TestProject\TestProject.csproj");

//var solutionI = SolutionInfo.Create(SolutionId.CreateNewId(), VersionStamp.Create(), @"C:\Temp\wiese\test.sln", new[] { projI });
//var ws = new AdhocWorkspace();
//var ele = ProjectRootElement.Create(@"C:\Temp\wiese\test.csproj");
//ele.ToolsVersion = "14.0";

//ele.Save();
//var solution = ws.AddSolution(solutionI);
//ws.TryApplyChanges(solution);

//var gen = SyntaxGenerator.GetGenerator(ws, LanguageNames.CSharp);
//var root = (CompilationUnitSyntax)CSharpSyntaxTree.ParseText(str).GetRoot();

//foreach (var namespaceDelcaration in root.Members)
//{
//    var tmpNamespace = (NamespaceDeclarationSyntax)namespaceDelcaration;
//    foreach (var classDeclaration in tmpNamespace.Members)
//    {
//        var tmpClass = (ClassDeclarationSyntax)classDeclaration;
//        var name = tmpClass.Identifier.Text;

//        foreach (var fieldDeclaration in tmpClass.Members.Where(m => m.Kind() == SyntaxKind.FieldDeclaration))
//        {
//            var n = (FieldDeclarationSyntax)fieldDeclaration;
//        }

//        foreach (var propertyDeclaration in tmpClass.Members.Where(m => m.Kind() == SyntaxKind.PropertyDeclaration))
//        {
//            var p = (PropertyDeclarationSyntax)propertyDeclaration;
//        }

//        foreach (var constructorDeclaration in tmpClass.Members.Where(m => m.Kind() == SyntaxKind.ConstructorDeclaration))
//        {
//            var c = (ConstructorDeclarationSyntax)constructorDeclaration;
//        }

//    }
//}

//return;

//using (var reader = new StreamReader(File.OpenRead(@"C:\Temp\unit_AL.txt")))
//{
//    tab.BLOB_Reference = ToBLOB(reader.ReadToEnd());
//}
//con.SubmitChanges();

//using (var reader = new StreamReader(@"C:\Temp\examples\test_2.cs"))//.OpenRead(args[0])))
//{
//    meta.User_Code = ToBLOB(reader.ReadToEnd());
//}

//con.SubmitChanges();

//tab.BLOB_Reference = null;
//using (var mdh = new MD5Cng())
//{
//    meta.Hash = BitConverter.ToString(mdh.ComputeHash(meta.User_Code.ToArray())).Replace(" - ", "").ToLower();
//}

//str = GetStringFromBLOB(meta.User_AL_Code);

//var obj = tab.BLOB_Reference.ToArray();
//byte[] test = new byte[obj.Length - 4];
//Array.Copy(obj, 4, test, 0, obj.Length - 4);

//var i = BitConverter.ToInt32(obj, 4);

//byte[] array2 = new byte[] { 2, 69, 125, 91 };
//var tmp = BitConverter.GetBytes(1617060235); 
//byte[] refer = new byte[tmp.Length + 4];
//Array.Copy(array2, 0, refer, 0, 4);
//Array.Copy(tmp, 0, refer, 4, tmp.Length);
//tab.BLOB_Reference = null;
////con.SubmitChanges();

//var bob = con.GetTable<Object>().First(s => s.ID == 92000).BLOB_Reference.ToArray();
//var alice = con.GetTable<Object_Metadata>().First(s => s.Object_ID == 92000).User_AL_Code.ToArray();
//var horst = con.GetTable<Object_Metadata>().First(s => s.Object_ID == 92000).User_Code.ToArray();
//var lo = BitConverter.ToInt64(bob, 4);
//var b = BitConverter.ToUInt16(bob, 4);
//byte[] tmp = new byte[bob.Length - 4];
//Array.Copy(bob, 4, tmp, 0, bob.Length - 4);
//var a = GetStringFromBLOB(bob);
//var co = Convert.ToUInt16(alice.Substring(0, 4), 16);
//var co = Convert.ToUInt16(alice);
//var cb = BitConverter.ToUInt16(bob, 4);
////con.SubmitChanges();
//var t = BitConverter.GetBytes(1617060339);
//byte[] array2 = new byte[] { 2, 69, 125, 91 };
//Array.Copy(bob, 4, tmp, 0, tmp.Length);
//Array.Copy(t, 0, tmp, 4, 2);
//var raw = tmp;
//tmp = new byte[1000];
//int take;

//var sourceArray = bob;

//using (var newStream = new MemoryStream())
//{
//    using (var stream = new MemoryStream(sourceArray))
//    {
//        byte[] sarray = new byte[4];
//        long num2 = stream.Read(sarray, 0, 4);

//        using (DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Decompress, false))
//            deflateStream.CopyTo(newStream);
//    }

//    raw = newStream.ToArray();
//}

//using (var reader = new StreamReader(new MemoryStream(raw)))
//{
//    var hope = reader.ReadToEnd();
//}

//using (var reader = new BinaryReader(File.OpenRead(@"C:\temp\examples\t_2.bk")))
//{
//    take = reader.BaseStream.Read(tmp, 0, tmp.Length);
//}

//tmp = tmp.Take(take).ToArray();

//con.GetTable<Object>().First(o => o.ID == 92000).BLOB_Reference = tmp;
//con.SubmitChanges();




//var encodings = Encoding.GetEncodings();
//for (int i = 0; i < encodings.Length; i++)
//{
//    var text = encodings[i].GetEncoding().GetString(raw);
//    using (var writer = new BinaryWriter(File.OpenWrite($@"C:\temp\encodings\t_{encodings[i].Name}.txt")))
//    {
//        writer.Write(text);
//    }
//}

//using (var stream = File.AppendText(@"C:\temp\examples\reference.txt"))
//{
//    stream.WriteLine(0);//b.ToString());
//}
