using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class DocuMaker : EditorWindow 
{
    
    [MenuItem("Window/DocuMaker")]
    public static void ShowWindow()
    {
        GetWindow(typeof(DocuMaker));
    }

    private void OnEnable()
    {
        if(classList.Length == 0)
            LoadClassList();

        warningIcon = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/DocuMaker/Icons/warning.png", typeof(Texture2D));

    }

    private Texture2D warningIcon;
    private Vector2 _classScrollVector = Vector2.zero;
    private Vector2 _methodScrollVector = Vector2.zero;
    private Vector2 _previewScrollVector = Vector2.zero;
    private int _menuModeIndex = 0;
    private readonly string[] _menuModeNames = new [] { "SelectClasses", "SelectMethods", "PreviewSave" };
    private GUIStyle greyBox;
    private GUIStyle greenBox;
    private void OnGUI()
    {
        _menuModeIndex = GUILayout.Toolbar(_menuModeIndex, _menuModeNames);

        greyBox = new GUIStyle(GUI.skin.label);
        greenBox = new GUIStyle(GUI.skin.label);
        greyBox.normal.textColor = Color.gray;
        greenBox.normal.textColor = Color.white;

        switch(_menuModeIndex)
        {
            case 0:
                GUILayout.Label("List of classes to document");

                _classScrollVector = GUILayout.BeginScrollView(_classScrollVector, false, true);
                int numberOfClasses = classList.Length;
                for(int i = 0; i < numberOfClasses; i++)
                {
                    GUIStyle useStyle = (includeClass[i]) ? greenBox : greyBox;
                    GUILayout.BeginHorizontal("box");
                    GUILayout.Label(classList[i].FullName, useStyle);
                    includeClass[i] = GUILayout.Toggle(includeClass[i], "", GUILayout.Width(20));
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndScrollView();

                GUILayout.BeginHorizontal("box");
                if (GUILayout.Button("Select All"))
                    for (int i = 0; i < numberOfClasses; i++)
                        includeClass[i] = true;
                if (GUILayout.Button("Select None"))
                    for (int i = 0; i < numberOfClasses; i++)
                        includeClass[i] = false;
                GUILayout.EndHorizontal();
                break;

            case 1:
                GUILayout.Label("List of methods to document");
                int numberOfMethods = methods.Length;
                int numberOfProperties = properties.Length;
                int numberOfEvents = events.Length;

                if(numberOfMethods == 0 && numberOfProperties == 0 && numberOfEvents == 0)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(warningIcon, GUILayout.Width(19), GUILayout.Height(20));
                    GUILayout.Label("You have not selected any classes");
                    GUILayout.EndHorizontal();
                }
                else
                {
                    _methodScrollVector = GUILayout.BeginScrollView(_methodScrollVector, false, true);

                    bool commentsIncluded = false;
                    GUILayout.Label("Methods");
                    for(int i = 0; i < numberOfMethods; i++)
                    {
                        GUIStyle useStyle = (includeMethod[i]) ? greenBox : greyBox;
                        GUILayout.BeginHorizontal("box");
                        GUILayout.Label(methods[i].ToString(), useStyle);
                        if(!methods[i].commented) GUILayout.Label(warningIcon, GUILayout.Width(19), GUILayout.Height(20));
                        includeMethod[i] = GUILayout.Toggle(includeMethod[i], "", GUILayout.Width(20));
                        GUILayout.EndHorizontal();

                        if(methods[i].commented) commentsIncluded = true;
                    }

                    GUILayout.Label("Properties");
                    for(int i = 0; i < numberOfProperties; i++)
                    {
                        GUIStyle useStyle = (includeProperties[i]) ? greenBox : greyBox;
                        GUILayout.BeginHorizontal("box");
                        GUILayout.Label(properties[i].type + ":" + properties[i].className + "." + properties[i].propertyName, useStyle);
                        if(!properties[i].commented) GUILayout.Label(warningIcon, GUILayout.Width(19), GUILayout.Height(20));
                        includeProperties[i] = GUILayout.Toggle(includeProperties[i], "", GUILayout.Width(20));
                        GUILayout.EndHorizontal();
                        if(properties[i].commented) commentsIncluded = true;
                    }

                    GUILayout.Label("Events");
                    for(int i = 0; i < numberOfEvents; i++)
                    {
                        GUIStyle useStyle = (includeEvents[i]) ? greenBox : greyBox;
                        GUILayout.BeginHorizontal("box");
                        GUILayout.Label(events[i].className + "." + events[i].eventName, useStyle);
                        if(!events[i].commented) GUILayout.Label(warningIcon, GUILayout.Width(19), GUILayout.Height(20));
                        includeEvents[i] = GUILayout.Toggle(includeEvents[i], "", GUILayout.Width(20));
                        GUILayout.EndHorizontal();
                        if(events[i].commented) commentsIncluded = true;
                    }

                    GUILayout.EndScrollView();

                    GUILayout.BeginHorizontal();
                    GUILayout.Label(warningIcon, GUILayout.Width(19), GUILayout.Height(20));
                    GUILayout.Label("This method lacks comments");
                    GUILayout.EndHorizontal();

                    if(!commentsIncluded)//there are no comments??!
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Label(warningIcon, GUILayout.Width(19), GUILayout.Height(20));
                        GUILayout.Label("There are no comments included in these entries!\nClick on the Import XML Comments to import\na generated comment file from Visual Studio");
                        GUILayout.EndHorizontal();
                    }

                    GUILayout.BeginHorizontal();
                    if(GUILayout.Button("Select All"))
                        for(int i = 0; i < numberOfMethods; i++)
                            includeMethod[i] = true;
                    if(GUILayout.Button("Select None"))
                        for(int i = 0; i < numberOfMethods; i++)
                            includeMethod[i] = false;
                    if(GUILayout.Button("Import XML Comments"))
                        AppendXMLComments();
                    GUILayout.EndHorizontal();
                }
                break;

            case 2:
                GUILayout.Label("Preview");
                _previewScrollVector = GUILayout.BeginScrollView(_previewScrollVector, false, true);
                GUILayout.Label(OutputToText());
                GUILayout.EndScrollView();

                ignoreUncommented = GUILayout.Toggle(ignoreUncommented, "Ignore Uncommmented Methods");

                if(GUILayout.Button("Export to HTML"))
                {
                    ExportHTML();
                }
                break;
        }

        if(GUI.changed && _menuModeIndex == 0)
            LoadMethods();

    }

    private System.Type[] classList = new System.Type[0];
    private bool[] includeClass = new bool[0];
    private MethodEntry[] methods = new MethodEntry[0];
    private bool[] includeMethod = new bool[0];
    private PropertyEntry[] properties = new PropertyEntry[0];
    private bool[] includeProperties = new bool[0];
    private EventEntry[] events = new EventEntry[0];
    private bool[] includeEvents = new bool[0];
    private List<string> includeAssembly = new List<string>() {"Assembly-CSharp"};
    private bool ignoreUncommented = false;

    private void LoadClassList()
    {
        var result = new List<System.Type>();
        var resultBool = new List<bool>();
        
        Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly in assemblies)
        {
            string assemblyName = assembly.FullName.Split(',')[0];
            if (!includeAssembly.Contains(assemblyName))
                continue;//skip assembly
            System.Type[] types = assembly.GetTypes();
            foreach(var T in types)
            {
                if (T.BaseType != null && T.BaseType.ToString() == "System.MulticastDelegate")//ignore delegates
                    continue;
                if(T.IsClass)
                {
                    
                    result.Add(T);
                    resultBool.Add(false);
                }
            }
        }
        classList = result.ToArray();
        includeClass = resultBool.ToArray();
    }

    private void LoadMethods()
    {
        int numberOfClasses = classList.Length;
        List<MethodEntry> methodList = new List<MethodEntry>();
        List<PropertyEntry> propertyList = new List<PropertyEntry>();
        List<EventEntry> eventList = new List<EventEntry>();
        var includeMethodList = new List<bool>();
        var includePropertyList = new List<bool>();
        var includeEntryList = new List<bool>();
        for(int i = 0; i < numberOfClasses; i++)
        {
            if(!includeClass[i])
                continue;//skip ignored classes
            System.Type type = classList[i];

            MethodInfo[] methodInfoList = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            int numberOfMethods = methodInfoList.Length;
            for(int m = 0; m < numberOfMethods; m++)
            {
                MethodInfo methodInfo = methodInfoList[m];

                if(!methodInfo.IsSpecialName && methodInfo.MemberType == MemberTypes.Method)
                {
                    MethodEntry methodEntry = new MethodEntry();
                    methodEntry.type = type;
                    methodEntry.className = type.Name;
                    methodEntry.methodName = methodInfo.Name;
                    methodEntry.methodType = TranslateString(methodInfo.MemberType.ToString());
                    methodEntry.methodReturn = TranslateString(methodInfo.ReturnType.Name);
                    methodEntry.isStatic = methodInfo.IsStatic;
                    ParameterInfo[] parameterList = methodInfo.GetParameters();
                    int numberOfParameters = parameterList.Length;
                    methodEntry.parameters = new ParameterEntry[numberOfParameters];
                    for(int p = 0; p < numberOfParameters; p++)
                    {
                        ParameterInfo parameterInfo = parameterList[p];
                        ParameterEntry parameterEntry = new ParameterEntry();
                        parameterEntry.name = parameterInfo.Name;
                        parameterEntry.type = TranslateString(parameterInfo.ParameterType.Name);
                        methodEntry.parameters[p] = parameterEntry;
                    }
                    methodList.Add(methodEntry);
                    includeMethodList.Add(true);
                }
            }


            PropertyInfo[] propertyInfoList = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
            int numberOfProperties = propertyInfoList.Length;
            for(int p = 0; p < numberOfProperties; p++)
            {
                PropertyInfo propertyInfo = propertyInfoList[p];
                PropertyEntry propertyEntry = new PropertyEntry();
                propertyEntry.className = type.Name;
                propertyEntry.propertyName = propertyInfo.Name;
                propertyEntry.type = propertyInfo.PropertyType.Name;
                propertyList.Add(propertyEntry);
                includePropertyList.Add(true);
            }

            //Events
            EventInfo[] eventInfoList = type.GetEvents(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
            int numberOfEvents = eventInfoList.Length;
            for (int e = 0; e < numberOfEvents; e++)
            {
                EventInfo eventInfo = eventInfoList[e];
                EventEntry eventEntry = new EventEntry();
                eventEntry.className = type.Name;
                eventEntry.eventName = eventInfo.Name;
                eventList.Add(eventEntry);

                ParameterInfo[] parameterList = eventInfo.EventHandlerType.GetMethod("Invoke").GetParameters();
                int numberOfParameters = parameterList.Length;
                eventEntry.parameters = new ParameterEntry[numberOfParameters];
                for (int p = 0; p < numberOfParameters; p++)
                {
                    ParameterInfo parameterInfo = parameterList[p];
                    ParameterEntry parameterEntry = new ParameterEntry();
                    parameterEntry.name = parameterInfo.Name;
                    parameterEntry.type = TranslateString(parameterInfo.ParameterType.Name);
                    eventEntry.parameters[p] = parameterEntry;
                }

                includeEntryList.Add(true);
            }
        }

        methods = methodList.ToArray();
        includeMethod = includeMethodList.ToArray();

        properties = propertyList.ToArray();
        includeProperties = includePropertyList.ToArray();

        events = eventList.ToArray();
        includeEvents = includeEntryList.ToArray();
    }

    private void AppendXMLComments()
    {
        string xmlpath = EditorUtility.OpenFilePanel("Import XML Comments", "", "xml");
        XmlDocument xml = new XmlDocument();
        if(xmlpath == "")
            return;
        using (StreamReader sr = new StreamReader(xmlpath))
        {
            xml.LoadXml(sr.ReadToEnd());
        }

        XmlNodeList nodes = xml.SelectNodes("doc/members/member");
        int numberOfNodes = nodes.Count;
        for(int n = 0; n < numberOfNodes; n++)
        {
            float percentage = n / (float)numberOfNodes;
            if(EditorUtility.DisplayCancelableProgressBar("Attching XML Comments", "", percentage))
            {
                EditorUtility.ClearProgressBar();
                return;//CANCEL NOW. NOW GODDAMNIT NOW!
            }
            XmlNode node = nodes[n];

            string member = node.Attributes[0].Value;
            string[] memberParts = member.Split(':');
            string content = memberParts[1];
            string[] contentParts = content.Split('.');
            if(contentParts.Length == 1)//no class? leave it for now...
                continue;
            string className = contentParts[0];
            string methodSignature = contentParts[1];
            string[] methodParts = methodSignature.Split('(',')');
            string methodName = methodParts[0];
            string summary = "";
            if(node["summary"] != null)
                summary = (node["summary"].FirstChild.Value);
            List<string> paramSummaries = new List<string>();
            foreach (XmlNode psNode in node.SelectNodes("param"))
                if(psNode.HasChildNodes)
                    paramSummaries.Add(psNode.FirstChild.Value);
            string returnSummary = "";
            if(node["returns"] != null && node["returns"].HasChildNodes)
                returnSummary = (node["returns"].FirstChild.Value);

            foreach (MethodEntry methodEntry in methods)
            {
                if(methodEntry.className == className && methodEntry.methodName == methodName)
                {
                    methodEntry.description = summary;
                    methodEntry.returndescription = returnSummary;
                    int numberOfCommentedSummaries = paramSummaries.Count;
                    methodEntry.paramCommentMismatch = methodEntry.parameters.Length != numberOfCommentedSummaries;
                    if (methodEntry.paramCommentMismatch)
                        continue;//here lies dragons!
                    for(int p = 0; p < numberOfCommentedSummaries; p++)
                    {
                        methodEntry.parameters[p].description = paramSummaries[p];
                    }
                    break;
                }
            }

            foreach (PropertyEntry propertyEntry in properties)
            {
                if (propertyEntry.className == className && propertyEntry.propertyName == methodName)
                {
                    propertyEntry.description = summary;
                    break;
                }
            }

            foreach (EventEntry eventEntry in events)
            {
                if (eventEntry.className == className && eventEntry.eventName == methodName)
                {
                    eventEntry.description = summary;
                    break;
                }
            }
        }
        EditorUtility.ClearProgressBar();
    }

    private string OutputToHTML()
    {
        StringBuilder sb = new StringBuilder();

        //head
         sb.AppendLine("<html>");
         sb.AppendLine("<head>");

         sb.AppendLine("<style>");

         sb.AppendLine("body, p ");
         sb.AppendLine("{");
         sb.AppendLine("	color: #444;");
         sb.AppendLine("	line-height: 17px;");
         sb.AppendLine("}");

         sb.AppendLine("body, p, ul, ol, td, li ");
         sb.AppendLine("{");
         sb.AppendLine("	font-family: Helvetica, Arial, sans-serif;");
         sb.AppendLine("	font-size: 12px;");
         sb.AppendLine("}");

         sb.AppendLine(".mainContainer");
         sb.AppendLine("{");
         sb.AppendLine("	text-align:left;");
         sb.AppendLine("}");

         sb.AppendLine(".methodTitle");
         sb.AppendLine("{");
         sb.AppendLine("	clear: both;");
         sb.AppendLine("	font-size: 18px;");
         sb.AppendLine("	font-weight: bold;");
         sb.AppendLine("	color: black;");
         sb.AppendLine("	margin: .9em 0px 0px 0px;");
         sb.AppendLine("	display: block;");
         sb.AppendLine("	line-height: 25px;");
         sb.AppendLine("}");

         sb.AppendLine(".MethodName");
         sb.AppendLine("{");
         sb.AppendLine("	font-weight: bold;");
         sb.AppendLine("}");

         sb.AppendLine(".descriptionTitle");
         sb.AppendLine("{");
         sb.AppendLine("font-size: 12px;");
         sb.AppendLine("font-weight: bold;");
         sb.AppendLine("margin: 1em 0px 0px 0px;");
         sb.AppendLine("}");

         sb.AppendLine("</style>");

         sb.AppendLine("</head>");
         sb.AppendLine("<body>");

        foreach (MethodEntry entry in methods)
        {
            if (!entry.commented && ignoreUncommented)
                continue;
            int numberOfParameters = entry.parameters.Length;
            sb.AppendLine("<div id=\"mainContainer\">");
            string staticString = entry.isStatic ? "Static " : "";
            sb.AppendLine("    <span class=\"methodTitle\">" + staticString + entry.className + "." + entry.methodName + "</span><br>");
            sb.AppendLine("        <div class=\"methodSignature\">");
            sb.Append("            " + entry.methodType);
            sb.Append(" <span class=\"MethodName\">" + entry.methodReturn + " "+ entry.methodName + "</span>");
            sb.Append(" (");
            for(int p = 0; p < numberOfParameters; p++)
            {
                ParameterEntry parameter = entry.parameters[p];
                sb.Append(" " + parameter.type + " " + parameter.name);
                if (entry.parameters.Length > 1 && p < numberOfParameters - 1)
                    sb.Append(", ");
            }
            sb.Append(" )");
            sb.AppendLine("</div>");
            if(entry.description != "")
            {
                sb.AppendLine("        <div class=\"descriptionTitle\">");
                sb.AppendLine("            Description");
                sb.AppendLine("       </div>");
                sb.AppendLine("        <div class=\"descriptionContent\">");
                sb.AppendLine("            " + entry.description);
                sb.AppendLine("        </div>");
            }
            if (numberOfParameters > 0)
            {
                sb.AppendLine("        <div class=\"parameters\">");
                foreach (ParameterEntry parameter in entry.parameters)
                    if (parameter.description != "")
                        sb.AppendLine("	            " + parameter.name + " : " + parameter.description);
                sb.AppendLine("        </div>");
            }
            if(entry.returndescription != "")
            {
                sb.AppendLine("        <div class=\"returns\">");
                sb.AppendLine("            returns: " + entry.returndescription);
                sb.AppendLine("        </div>");
            }
            sb.AppendLine("</div>");
            sb.AppendLine();
        }


        foreach (PropertyEntry entry in properties)
        {
            if (!entry.commented && ignoreUncommented)
                continue;
            sb.AppendLine("<div id=\"mainContainer\">");
            sb.AppendLine("    <span class=\"methodTitle\">" + entry.className + "." + entry.propertyName + "</span><br>");
            sb.AppendLine("        <div class=\"methodSignature\">");
            sb.Append("            Property");
            sb.Append(" <span class=\"MethodName\">" + entry.type + " " + entry.propertyName + "</span>");

            sb.AppendLine("</div>");
            if (entry.description != "")
            {
                sb.AppendLine("        <div class=\"descriptionTitle\">");
                sb.AppendLine("            Description");
                sb.AppendLine("       </div>");
                sb.AppendLine("        <div class=\"descriptionContent\">");
                sb.AppendLine("            " + entry.description);
                sb.AppendLine("        </div>");
            }
            sb.AppendLine("</div>");
            sb.AppendLine();
        }

        foreach (EventEntry entry in events)
        {
            if (!entry.commented && ignoreUncommented)
                continue;
            sb.AppendLine("<div id=\"mainContainer\">");
            sb.AppendLine("    <span class=\"methodTitle\">" + entry.className + "." + entry.eventName + "</span><br>");
            sb.AppendLine("        <div class=\"methodSignature\">");
            sb.Append("            Event");
            sb.Append(" <span class=\"MethodName\">" + entry.eventName + "</span>");
            int numberOfParameters = entry.parameters.Length;
            sb.Append(" (");
            for (int p = 0; p < numberOfParameters; p++)
            {
                ParameterEntry parameter = entry.parameters[p];
                sb.Append(" " + parameter.type + " " + parameter.name);
                if (entry.parameters.Length > 1 && p < numberOfParameters - 1)
                    sb.Append(", ");
            }
            sb.Append(" )");
            sb.AppendLine("</div>");
            if (entry.description != "")
            {
                sb.AppendLine("        <div class=\"descriptionTitle\">");
                sb.AppendLine("            Description");
                sb.AppendLine("       </div>");
                sb.AppendLine("        <div class=\"descriptionContent\">");
                sb.AppendLine("            " + entry.description);
                sb.AppendLine("        </div>");
            }
            if (numberOfParameters > 0)
            {
                sb.AppendLine("        <div class=\"parameters\">");
                foreach (ParameterEntry parameter in entry.parameters)
                    if (parameter.description != "")
                        sb.AppendLine("	            " + parameter.name + " : " + parameter.description);
                sb.AppendLine("        </div>");
            }
            sb.AppendLine("</div>");
            sb.AppendLine();
        }

        sb.AppendLine("</body>");
        sb.AppendLine("</html>");

        return sb.ToString();
    }

    private void ExportHTML()
    {
        string defaultName = GetProjectName();
        string filepath = EditorUtility.SaveFilePanel("Export Documentation to HTML", "/", defaultName, "html");

        if (filepath != "")
        {
            using (StreamWriter sw = new StreamWriter(filepath))
            {
                sw.Write(OutputToHTML());//write out contents of data to HTML
            }
        }
    }

    private string GetProjectName()
    {
        string[] s = Application.dataPath.Split('/');
        string projectName = s[s.Length - 2];
        return projectName;
    }

    private string OutputToText()
    {
        StringBuilder sb = new StringBuilder();
        foreach (MethodEntry entry in methods)
        {
            if(!entry.commented && ignoreUncommented)
                continue;

            int numberOfParameters = entry.parameters.Length;
            string staticString = entry.isStatic ? "Static " : "";
            sb.AppendLine(staticString + entry.className + "." + entry.methodName);
            sb.Append(entry.methodType+" ");
            sb.Append(entry.methodName);
            sb.Append("(");
            for (int p = 0; p < numberOfParameters; p++)
            {
                ParameterEntry parameter = entry.parameters[p];
                sb.Append(parameter.type + " " + parameter.name);
                if(entry.parameters.Length > 1 && p < numberOfParameters-1)
                    sb.Append(", ");
            }
            sb.Append("):");
            sb.Append(entry.methodReturn);
            sb.AppendLine(entry.description);
            if (entry.parameters.Length > 0)
                foreach(ParameterEntry parameter in entry.parameters)
                    if (parameter.description != "")
                        sb.AppendLine(parameter.name + " : " + parameter.description);
            if (entry.returndescription != "")
                sb.AppendLine("returns: " + entry.returndescription);
            sb.AppendLine();
        }

        foreach (PropertyEntry entry in properties)
        {
            if (!entry.commented && ignoreUncommented)
                continue;
            sb.AppendLine(entry.className + "." + entry.propertyName);
            sb.Append("Property: ");
            sb.Append(entry.type + " ");
            sb.Append(entry.propertyName);
            if (entry.description != "")
                sb.AppendLine(entry.description);
            sb.AppendLine();
            sb.AppendLine();
        }

        foreach (EventEntry entry in events)
        {
            if (!entry.commented && ignoreUncommented)
                continue;
            sb.AppendLine(entry.className + "." + entry.eventName);
            sb.Append("Event: ");
            sb.Append(entry.eventName + " ");
            if (entry.description != "")
                sb.AppendLine(entry.description);
            sb.AppendLine();
            sb.AppendLine();
        }

        return sb.ToString();
    }

    private string TranslateString(string input)
    {
        switch(input)
        {
            default:
                return input;

            case "Single":
                return "float";

            case "Int32":
                return "int";

            case "String":
                return "string";

            case "Boolean":
                return "bool";
        }
    }

//    private void GotoMethodLine(System.Type type, string method)
//    {
//        AssetDatabase.
//        MonoScript script = MonoScript.
//        StreamReader sr = new StreamReader();
//
//
//        //Not importing System.Diagnostics as it conflicts with Debug
//        System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
//        System.Diagnostics.StackFrame[] stackFrames = st.GetFrames();
//        int lineNumber = -1;
//        string fileName = "/";
//        foreach (System.Diagnostics.StackFrame stackFrame in stackFrames)
//        {
//            Debug.Log((type.GetMethod(method).ToString()+" "+stackFrame.GetMethod().ToString()));
//            if(type.GetMethod(method) == stackFrame.GetMethod())
//            {
//                lineNumber = stackFrame.GetFileLineNumber();
//                fileName = stackFrame.GetFileName();
//            }
//        }
//
//        if(lineNumber != -1)
//        {
//            MonoScript script = (MonoScript)AssetDatabase.LoadAssetAtPath(fileName, typeof(MonoScript));
//            AssetDatabase.OpenAsset(script, lineNumber);
//        }
//        else
//        {
//            Debug.Log("GotoMethodLine wrong");
//        }
//    }

    public class MethodEntry
    {
        public System.Type type = null;
        public string className = "Class";
        public string methodName = "Method";
        public string methodType = "function";
        public string methodReturn = "Void";
        public string description = "";
        public string returndescription = "";
        public ParameterEntry[] parameters = new ParameterEntry[0];
        public bool paramCommentMismatch = false;
        public bool isStatic = false;

        public bool commented
        {
            get
            {
                if (description == "") return false;
                if (methodReturn != "Void" && returndescription == "") return false;
                foreach (ParameterEntry parameter in parameters)
                    if (parameter.description == "") return false;
                return true;
            }
        }

        public bool partialCommented
        {
            get
            {
                if (description != "") return true;
                if (methodReturn != "Void" && returndescription != "") return true;
                foreach (ParameterEntry parameter in parameters)
                    if (parameter.description != "") return true;
                return false;
            }
        }

        public new string ToString()
        {
            int numberOfParameters = parameters.Length;
            string output = methodType + " : " + className + "." + methodName + "(";
            for(int p = 0; p < numberOfParameters; p++)
            {
                ParameterEntry parameter = parameters[p];
                if(parameter.type != "")
                    output += parameter.type;
                if(numberOfParameters > 1 && p < numberOfParameters - 1)
                    output += ", ";
            }
            output += ") : " + methodReturn;
            return output;
        }
    }

    public class ParameterEntry
    {
        public string name = "Parameter";
        public string type = "Type";
        public string description = "";
    }

    public class PropertyEntry
    {
        public string className = "Class";
        public string propertyName = "Property";
        public string type = "";
        public string description = "";

        public bool commented
        {
            get{return description != "";}
        }
    }

    public class EventEntry
    {
        public string className = "Class";
        public string eventName = "Event";
        public ParameterEntry[] parameters = new ParameterEntry[0];
        public string description = "";
        public bool commented
        {
            get { return description != ""; }
        }
    }
}

